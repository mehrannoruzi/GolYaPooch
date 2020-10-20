using System;
using Elk.Core;
using System.Linq;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class PurchaseService : IPurchaseService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;
        private static object lockObject = new object();

        public PurchaseService(AppUnitOfWork appUnitOfWork, IConfiguration configuration)
        {
            _appUow = appUnitOfWork;
            _configuration = configuration;
        }


        public async Task<IResponse<PagingListDetails<PurchaseDto>>> GetActivePurchasesAsync(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<PurchaseDto>>();
            try
            {
                var now = DateTime.Now;
                var purchases = await _appUow.PurchaseRepo.GetPagingAsync(
                    new PagingQueryFilterWithSelector<Purchase, PurchaseDto>
                    {
                        Conditions = x => x.UserId == userId && !x.IsFinished && x.UsedChance < x.Chance && x.ExpireDateMi > now,
                        IncludeProperties = new List<Expression<Func<Purchase, object>>> {
                            x => x.ProductOffer,
                            x => x.ProductOffer.Product,
                            x => x.PaymentTransaction
                        },
                        PagingParameter = pagingParameter,
                        OrderBy = x => x.OrderByDescending(x => x.PurchaseId),
                        Selector = x => new PurchaseDto
                        {
                            #region Set Purchase Property
                            PurchaseId = x.PurchaseId,
                            ProductOffer = new
                            {
                                Product = new
                                {
                                    x.ProductOffer.Product.Subject,
                                    x.ProductOffer.Product.Text,
                                    x.ProductOffer.Product.Type
                                },
                                ImageUrl = _configuration["CustomSettings:CdnAddress"] + x.ProductOffer.ImageUrl,
                                x.ProductOffer.Price,
                                x.ProductOffer.Discount,
                                x.ProductOffer.Profit,
                                x.ProductOffer.TotalPrice
                            },
                            PaymentTransaction = new
                            {
                                x.PaymentTransaction.Type,
                                x.PaymentTransaction.Description
                            },
                            Chance = x.Chance,
                            UsedChance = x.UsedChance,
                            IsReFoundable = x.IsReFoundable,
                            ExpireDateSh = x.ExpireDateSh,
                            InsertDateSh = x.InsertDateSh,
                            ModifyDateSh = x.ModifyDateSh
                            #endregion
                        }
                    });

                response.Message = ServiceMessage.Success;
                response.Result = purchases;
                response.IsSuccessful = true;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<PagingListDetails<PurchaseDto>>> GetAllPurchasesAsync(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<PurchaseDto>>();
            try
            {
                var purchases = await _appUow.PurchaseRepo.GetPagingAsync(
                    new PagingQueryFilterWithSelector<Purchase, PurchaseDto>
                    {
                        Conditions = x => x.UserId == userId,
                        IncludeProperties = new List<Expression<Func<Purchase, object>>> {
                            x => x.ProductOffer,
                            x => x.ProductOffer.Product,
                            x => x.PaymentTransaction
                        },
                        PagingParameter = pagingParameter,
                        OrderBy = x => x.OrderByDescending(x => x.PurchaseId),
                        Selector = x => new PurchaseDto
                        {
                            #region Set Purchase Property
                            PurchaseId = x.PurchaseId,
                            ProductOffer = new
                            {
                                Product = new
                                {
                                    x.ProductOffer.Product.Subject,
                                    x.ProductOffer.Product.Text,
                                    x.ProductOffer.Product.Type
                                },
                                ImageUrl = _configuration["CustomSettings:CdnAddress"] + x.ProductOffer.ImageUrl,
                                x.ProductOffer.Price,
                                x.ProductOffer.Discount,
                                x.ProductOffer.Profit,
                                x.ProductOffer.TotalPrice
                            },
                            PaymentTransaction = new
                            {
                                x.PaymentTransaction.Type,
                                x.PaymentTransaction.Description
                            },
                            Chance = x.Chance,
                            UsedChance = x.UsedChance,
                            IsReFoundable = x.IsReFoundable,
                            ExpireDateSh = x.ExpireDateSh,
                            InsertDateSh = x.InsertDateSh,
                            ModifyDateSh = x.ModifyDateSh
                            #endregion
                        }
                    });

                response.Message = ServiceMessage.Success;
                response.Result = purchases;
                response.IsSuccessful = true;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        private IEnumerable<Purchase> GetPaybackblePurchase()
        {
            var purchases = new List<Purchase>();
            try
            {
                var now = DateTime.Now.Date;
                lock (lockObject)
                {
                    purchases = _appUow.PurchaseRepo.GetAsync(
                    new QueryFilter<Purchase>
                    {
                        AsNoTracking = false,
                        Conditions = x => x.IsReFoundable && x.ExpireDateMi < now,
                        OrderBy = x => x.OrderByDescending(x => x.PurchaseId),
                        IncludeProperties = new List<Expression<Func<Purchase, object>>> {
                            x => x.User,
                            x=> x.ProductOffer,
                            x=> x.ProductOffer.Product
                        }
                    }).Result;

                    if (purchases.Any())
                    {
                        foreach (var item in purchases)
                            item.IsLock = true;

                        _appUow.PurchaseRepo.UpdateRange(purchases);
                        _appUow.SaveChanges();
                    }

                    return purchases;
                }
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return new List<Purchase>();
            }
        }

        private async Task DoPaybackAsync(Purchase item, AppUnitOfWork appUow)
        {
            using (var trans = await appUow.Database.BeginTransactionAsync())
            {
                try
                {
                    #region Get Default Gateway & Create Transaction
                    var defaultPaymentGateway = await appUow.PaymentGatewayRepo.FirstOrDefaultAsync(
                        new QueryFilter<PaymentGateway>
                        {
                            Conditions = x => x.IsActive && x.IsDefault
                        });

                    var transaction = new PaymentTransaction
                    {
                        UserId = item.UserId,
                        IsSuccess = true,
                        TrackingId = "0",
                        Status = ServiceMessage.Success,
                        Type = TransactionType.Payback,
                        Price = (int)(item.ProductOffer.TotalPrice * item.ProductOffer.ReFoundsPercent),
                        ProductOfferId = item.ProductOffer.ProductOfferId,
                        PaymentGatewayId = defaultPaymentGateway.PaymentGatewayId,
                        Description = ServiceMessage.ReFoundPurchase
                    };
                    await appUow.PaymentTransactionRepo.AddAsync(transaction);
                    #endregion

                    item.IsFinished = true;
                    item.IsReFoundable = false;
                    appUow.PurchaseRepo.Update(item);

                    var oldBalance = item.User.Balance;
                    item.User.Balance += transaction.Price;
                    _appUow.UserRepo.Update(item.User);
                    var saveResult = await appUow.ElkSaveChangesAsync();
                    if (saveResult.IsSuccessful)
                    {
                        #region Insert PayBack Notification
                        var notif = new Notification
                        {
                            UserId = item.UserId,
                            Type = NotificationType.PushNotification,
                            Action = NotificationAction.PayBackPurchase,
                            Priority = Priority.Low,
                            IsActive = true,
                            IsSent = false,
                            IsSuccess = false,
                            IsRead = false,
                            Subject = ServiceMessage.PayBackSubject,
                            Text = ServiceMessage.PayBackText.Fill($"{item.User.FirstName} {item.User.LastName}".Trim(), transaction.Price.To3DigitSplited(), item.ProductOffer.Product.Text, item.User.Balance.To3DigitSplited()),
                            IconUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.PayBackPurchaseIconUrl,
                            ImageUrl = _configuration["CustomSettings:CdnAddress"] + ServiceMessage.PayBackPurchaseImageUrl
                        };
                        await appUow.NotificationRepo.AddAsync(notif);
                        await appUow.SaveChangesAsync();
                        #endregion

                        await trans.CommitAsync();
                    }
                    else
                        await trans.RollbackAsync();

                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    FileLoger.Error(e);
                    await Task.FromException(e);
                }
            }
        }

        public async Task ProccessPaybackblePurchaseAsync()
        {
            try
            {
                var PaybackblePurchase = GetPaybackblePurchase();
                foreach (var item in PaybackblePurchase)
                    await DoPaybackAsync(item, _appUow);

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                await Task.FromException(e);
            }
        }
    }
}