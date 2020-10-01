﻿using System;
using Elk.Core;
using Elk.Cache;
using System.Linq;
using GolPooch.Domain.Enum;
using GolPooch.CrossCutting;
using GolPooch.Domain.Entity;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using System.Linq.Expressions;
using GolPooch.Service.Resourses;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class ChestService : IChestService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _chestCacheKey = GlobalVariables.CacheSettings.ChestCacheKey();

        public ChestService(AppUnitOfWork appUnitOfWork, IMemoryCacheProvider cacheProvider,
            IConfiguration configuration)
        {
            _appUow = appUnitOfWork;
            _cacheProvider = cacheProvider;
            _configuration = configuration;
        }

        public IResponse<List<Chest>> GetAllAvailable()
        {
            var response = new Response<List<Chest>>();
            try
            {
                var chests = (List<Chest>)_cacheProvider.Get(_chestCacheKey);
                if (chests == null)
                {
                    chests = _appUow.ChestRepo.Get(
                        new QueryFilter<Chest>
                        {
                            Conditions = x => x.IsActive,
                            OrderBy = x => x.OrderByDescending(x => x.ParticipantCount)
                        }).ToList();

                    foreach (var chest in chests)
                        chest.ImageUrl = chest.ImageUrl != null
                            ? _configuration["CustomSettings:CdnAddress"] + chest.ImageUrl
                            : null;

                    _cacheProvider.Add(_chestCacheKey, chests, DateTime.Now.AddHours(GlobalVariables.CacheSettings.ChestCacheTimeout()));
                }

                response.Result = chests;
                response.IsSuccessful = true;
                response.Message = ServiceMessage.Success;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public IResponse<int> MyChanceAsync(int userId, int ChestId)
        {
            var response = new Response<int>();
            try
            {
                var drawChance = _appUow.DrawChanceRepo.Get(
                    new QueryFilterWithSelector<DrawChance, object>
                    {
                        Conditions = x => x.UserId == userId && x.Round.Chest.ChestId == ChestId,
                        IncludeProperties = new List<Expression<Func<DrawChance, object>>>
                        {
                            x=> x.Round,
                            x=> x.Round.Chest
                        },
                        Selector = x => new
                        {
                            x.UserId,
                            x.RoundId,
                            x.Round.ChestId
                        }
                    });

                response.IsSuccessful = true;
                response.Result = drawChance.Count();
                response.Message = ServiceMessage.Success;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<string>> SpendChanceAsync(int userId, int purchaseId, int chestId, byte chanceCount)
        {
            var response = new Response<string>();
            using (var trans = await _appUow.Database.BeginTransactionAsync())
            {
                var now = DateTime.Now;
                try
                {
                    #region Get Purchase
                    var purchase = await _appUow.PurchaseRepo.FirstOrDefaultAsync(
                        new QueryFilter<Purchase>
                        {
                            AsNoTracking = false,
                            Conditions = x => x.PurchaseId == purchaseId
                        });
                    if (purchase.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidPurchase };
                    if (purchase.IsFinished) return new Response<string> { Message = ServiceMessage.PurchaseHasFinished };
                    if (purchase.UserId != userId) return new Response<string> { Message = ServiceMessage.PuchaseNotForCurrentUser };
                    if (purchase.UsedChance >= purchase.Chance) return new Response<string> { Message = ServiceMessage.PurchaseNotAnyChanse };

                    purchase.IsReFoundable = false;
                    purchase.UsedChance += chanceCount;
                    if (purchase.UsedChance == purchase.Chance) purchase.IsFinished = true;
                    else if (purchase.UsedChance >= purchase.Chance) return new Response<string> { Message = ServiceMessage.InvalidChanceCount };
                    #endregion

                    #region Get Chest & Round
                    var chest = await _appUow.ChestRepo.FirstOrDefaultAsync(
                        new QueryFilter<Chest>
                        {
                            Conditions = x => x.ChestId == chestId
                        });
                    if (chest.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidChest };
                    if (!chest.IsActive) return new Response<string> { Message = ServiceMessage.ChestNotActive };

                    var rounds = _appUow.RoundRepo.Get(
                        new QueryFilter<Round>
                        {
                            Conditions = x => x.ChestId == chestId
                        }).ToList();

                    Round newRound, currentRound;
                    var roundCounter = 0;
                    if (!rounds.Any())
                    {
                        newRound = new Round
                        {
                            ChestId = chestId,
                            ParticipantCount = chest.ParticipantCount,
                            State = RoundState.Open,
                            OpenDateMi = now,
                            OpenDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date),
                            CloseDateMi = null,
                            CloseDateSh = null,
                            Description = ServiceMessage.CreateFirstRoundBySystem
                        };
                        await _appUow.RoundRepo.AddAsync(newRound);
                        var roundSaveResult = await _appUow.ElkSaveChangesAsync();
                        if (!roundSaveResult.IsSuccessful) return new Response<string> { Message = ServiceMessage.InvalidRound };
                        currentRound = newRound;
                    }
                    else
                    {
                        currentRound = rounds.FirstOrDefault(x => x.State == RoundState.Open);
                        if (currentRound.IsNull() && rounds.Count() == chest.RoundCount)
                        {
                            //await trans.RollbackAsync();
                            return new Response<string> { Message = ServiceMessage.AllChestRoundHasFinished };
                        }
                        else if (currentRound.IsNull() && rounds.Count() < chest.RoundCount)
                        {
                            newRound = new Round
                            {
                                ChestId = chestId,
                                ParticipantCount = chest.ParticipantCount,
                                State = RoundState.Open,
                                OpenDateMi = now,
                                OpenDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date),
                                CloseDateMi = null,
                                CloseDateSh = null,
                                Description = ServiceMessage.CreateNewRoundBySystem
                            };
                            await _appUow.RoundRepo.AddAsync(newRound);
                            var roundSaveResult = await _appUow.ElkSaveChangesAsync();
                            if (!roundSaveResult.IsSuccessful) return new Response<string> { Message = ServiceMessage.InvalidRound };
                            currentRound = newRound;
                        }
                    }
                    #endregion

                    #region Save DrawChest
                    var lastDrawChance = await _appUow.DrawChanceRepo.FirstOrDefaultAsync(
                        new QueryFilter<DrawChance>
                        {
                            Conditions = x => x.RoundId == currentRound.RoundId,
                            OrderBy = x => x.OrderByDescending(x => x.DrawChanceId)
                        });

                    if (lastDrawChance.IsNotNull()) roundCounter = lastDrawChance.Counter;
                    if ((roundCounter + chanceCount) > currentRound.ParticipantCount) return new Response<string> { Message = ServiceMessage.ParticipantCountOverflow.Fill($"{(roundCounter + chanceCount) - currentRound.ParticipantCount}") };

                    var drawChestCode = string.Empty;
                    var drawChestList = new List<DrawChance>();
                    for (int i = 0; i < chanceCount; i++)
                    {
                        roundCounter += 1;
                        var drawChest = new DrawChance
                        {
                            UserId = userId,
                            RoundId = currentRound.RoundId,
                            PurchaseId = purchaseId,
                            Counter = roundCounter,
                            Code = Randomizer.GetRandomString(8)
                        };

                        drawChestList.Add(drawChest);
                        drawChestCode += $"{ServiceMessage.DrawChestCode} = {drawChest.Code}" + Environment.NewLine;
                    }
                    await _appUow.DrawChanceRepo.AddRangeAsync(drawChestList);

                    if (currentRound.ParticipantCount == roundCounter)
                    {
                        currentRound.State = RoundState.Close;
                        currentRound.CloseDateMi = now;
                        currentRound.CloseDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                        currentRound.Description += $" | {ServiceMessage.ClosedRoundBySystem}";

                        _appUow.RoundRepo.UpdateUnAttached(currentRound);
                        await _appUow.SaveChangesAsync();

                        await DoDrawAsync(chest, currentRound);
                    }

                    var saveResult = await _appUow.ElkSaveChangesAsync();
                    #endregion

                    if (saveResult.IsSuccessful)
                    {
                        #region Add Spend Chance Notif
                        var spendChanceNotif = new Notification
                        {
                            UserId = userId,
                            Type = NotificationType.PushNotification,
                            Action = NotificationAction.SpendChance,
                            Priority = Priority.Low,
                            IsActive = true,
                            IsSent = false,
                            IsSuccess = false,
                            IsRead = false,
                            Subject = ServiceMessage.SpendChanceSubject,
                            Text = ServiceMessage.SpendChanceText.Fill(chest.Title, Environment.NewLine + drawChestCode),
                            IconUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.NotifyWinnerIconUrl,
                            ImageUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.NotifyWinnerIconUrl
                        };
                        await _appUow.NotificationRepo.AddAsync(spendChanceNotif);
                        await _appUow.SaveChangesAsync();
                        #endregion

                        await trans.CommitAsync();
                    }
                    else
                        await trans.RollbackAsync();

                    response.IsSuccessful = saveResult.IsSuccessful;
                    response.Result = saveResult.IsSuccessful ? drawChestCode : string.Empty;
                    response.Message = saveResult.IsSuccessful ? ServiceMessage.Success : ServiceMessage.Error;
                    return response;
                }
                catch (Exception e)
                {
                    FileLoger.Error(e);
                    await trans.RollbackAsync();
                    response.Message = ServiceMessage.Exception;
                    return response;
                }
            }
        }

        private async Task DoDrawAsync(Chest chest, Round closedRound)
        {
            #region Do Draw & Update Closed Round
            var now = DateTime.Now;
            var participants = _appUow.DrawChanceRepo.Get(
                new QueryFilter<DrawChance>
                {
                    Conditions = x => x.RoundId == closedRound.RoundId,
                    OrderBy = x => x.OrderBy(x => Guid.NewGuid())
                });

            var winnerList = new List<RoundWinner>();
            for (int i = 0; i < chest.WinnerCount; i++)
            {
                var skip = new Random().Next(0, participants.Count() - 1);
                var drawWinner = participants.Skip(skip).Take(1).FirstOrDefault();

                var newWinner = new RoundWinner
                {
                    RoundId = drawWinner.RoundId,
                    UserId = drawWinner.UserId
                };
                winnerList.Add(newWinner);
            }
            await _appUow.RoundWinnerRepo.AddRangeAsync(winnerList);

            closedRound.State = RoundState.waitForPay;
            closedRound.DrawDateMi = now;
            closedRound.DrawDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
            closedRound.Description += " | " + ServiceMessage.EndDrawBySystem;

            _appUow.RoundRepo.UpdateUnAttached(closedRound);
            #endregion

            #region Insert Draw Result Notification
            foreach (var user in winnerList)
            {
                var winnerNotifList = new List<Notification>();
                var winner = await _appUow.UserRepo.FindAsync(user.UserId);
                foreach (var participant in participants)
                {
                    var winnerNotif = new Notification
                    {
                        UserId = participant.UserId,
                        Type = NotificationType.PushNotification,
                        Action = NotificationAction.NotifyWinners,
                        Priority = Priority.Low,
                        IsActive = true,
                        IsSent = false,
                        IsSuccess = false,
                        IsRead = false,
                        Subject = ServiceMessage.NotifyWinnerSubject,
                        Text = ServiceMessage.NotifyWinnerText.Fill(winner.FirstName + winner.LastName).Fill(chest.Title),
                        IconUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.NotifyWinnerIconUrl,
                        ImageUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.NotifyWinnerIconUrl
                    };
                    winnerNotifList.Add(winnerNotif);
                }
                await _appUow.NotificationRepo.AddRangeAsync(winnerNotifList);

                var userWinnerNotif = new Notification
                {
                    UserId = winner.UserId,
                    Type = NotificationType.Sms,
                    Action = NotificationAction.NotifyWinners,
                    Priority = Priority.High,
                    IsActive = true,
                    IsSent = false,
                    IsSuccess = false,
                    IsRead = false,
                    Subject = ServiceMessage.NotifyWinnerSubject,
                    Text = ServiceMessage.NotifyWinnerText.Fill(winner.FirstName + winner.LastName).Fill(chest.Title),
                    IconUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.NotifyWinnerIconUrl,
                    ImageUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.NotifyWinnerIconUrl
                };
                await _appUow.NotificationRepo.AddAsync(userWinnerNotif);
            }
            #endregion

            await _appUow.ElkSaveChangesAsync();
        }
    }
}