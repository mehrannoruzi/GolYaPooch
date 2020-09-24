using System;
using Elk.Core;
using Elk.Cache;
using System.Linq;
using GolPooch.CrossCutting;
using GolPooch.Domain.Entity;
using GolPooch.DataAccess.Ef;
using GolPooch.Service.Resourses;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using GolPooch.Domain.Enum;

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

        public async Task<IResponse<string>> SpendChanceAsync(int userId, int purchaseId, int ChestId)
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
                            Conditions = x => x.PurchaseId == purchaseId,
                            AsNoTracking = false
                        });
                    if (purchase.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidPurchase };
                    if (purchase.IsFinished) return new Response<string> { Message = ServiceMessage.PurchaseHasFinished };
                    if (purchase.UserId != userId) return new Response<string> { Message = ServiceMessage.PuchaseNotForCurrentUser };
                    if (purchase.UsedChance >= purchase.Chance) return new Response<string> { Message = ServiceMessage.PurchaseNotAnyChanse };

                    purchase.UsedChance += 1;
                    #endregion

                    #region Get Chest
                    var chest = await _appUow.ChestRepo.FirstOrDefaultAsync(
                        new QueryFilter<Chest>
                        {
                            Conditions = x => x.ChestId == ChestId
                        });
                    if (chest.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidChest };
                    if (!chest.IsActive) return new Response<string> { Message = ServiceMessage.ChestNotActive };
                    #endregion

                    #region Get Round
                    var rounds = _appUow.RoundRepo.Get(
                        new QueryFilter<Round>
                        {
                            Conditions = x => x.ChestId == ChestId
                        }).ToList();

                    Round newRound, currentRound;
                    var roundCounter = 0;
                    var participantCount = chest.ParticipantCount;
                    if (rounds.IsNull() || rounds.Count() == 0)
                    {
                        newRound = new Round
                        {
                            ChestId = ChestId,
                            WinnerUserId = null,
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
                            await trans.RollbackAsync();
                            return new Response<string> { Message = ServiceMessage.AllChestRoundHasFinished };
                        }
                        else if (currentRound.IsNull() && rounds.Count() < chest.RoundCount)
                        {
                            newRound = new Round
                            {
                                ChestId = ChestId,
                                WinnerUserId = null,
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

                    #region Save Draw Chest
                    var lastDrawChance = await _appUow.DrawChanceRepo.FirstOrDefaultAsync(
                        new QueryFilter<DrawChance>
                        {
                            Conditions = x => x.RoundId == currentRound.RoundId,
                            OrderBy = x => x.OrderByDescending(x => x.DrawChanceId)
                        });
                    if (lastDrawChance.IsNotNull()) roundCounter = lastDrawChance.Counter;

                    var drawChest = new DrawChance
                    {
                        UserId = userId,
                        RoundId = currentRound.RoundId,
                        PurchaseId = purchaseId,
                        Counter = roundCounter + 1,
                        Code = Randomizer.GetRandomString(16)
                    };
                    await _appUow.DrawChanceRepo.AddAsync(drawChest);

                    if (currentRound.ParticipantCount == drawChest.Counter)
                    {
                        currentRound.State = RoundState.End;
                        currentRound.CloseDateMi = now;
                        currentRound.CloseDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                        currentRound.Description += $" | {ServiceMessage.ClosedRoundBySystem}";

                        _appUow.RoundRepo.UpdateUnAttached(currentRound);
                    }

                    var saveResult = await _appUow.ElkSaveChangesAsync();
                    #endregion

                    response.IsSuccessful = saveResult.IsSuccessful;
                    response.Result = saveResult.IsSuccessful ? drawChest.Code : string.Empty;
                    response.Message = saveResult.IsSuccessful ? ServiceMessage.Success : ServiceMessage.Error;
                    await trans.CommitAsync();
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
    }
}