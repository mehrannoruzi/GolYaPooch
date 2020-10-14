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

        public async Task<IResponse<object>> ReFoundsAsync(int userId, int purchaseId)
        {
            var response = new Response<object>();
            using (var trans = await _appUow.Database.BeginTransactionAsync())
            {
                try
                {
                    #region Get Purchase & User
                    var now = DateTime.Now;
                    var purchase = await _appUow.PurchaseRepo.FindAsync(purchaseId);
                    if (purchase.IsNull()) return new Response<object> { Message = ServiceMessage.InvalidPurchase };
                    if (purchase.UsedChance != 0 && purchase.ExpireDateMi < now && purchase.IsFinished) return new Response<object> { Message = ServiceMessage.PurchaseNotReFoundable };

                    var user = await _appUow.UserRepo.FindAsync(userId);
                    if (user.IsNull()) return new Response<object> { Message = ServiceMessage.InvalidUserId };
                    #endregion

                    #region ReFounds Purchase
                    var defaultPaymentGateway = await _appUow.PaymentGatewayRepo.FirstOrDefaultAsync(
                        new QueryFilter<PaymentGateway>
                        {
                            Conditions = x => x.IsActive && x.IsDefault
                        });

                    var productOffer = await _appUow.ProductOfferRepo.FirstOrDefaultAsync(
                        new QueryFilter<ProductOffer>
                        {
                            Conditions = x => x.ProductOfferId == purchase.ProductOfferId,
                        });

                    var transaction = new PaymentTransaction
                    {
                        UserId = user.UserId,
                        IsSuccess = true,
                        TrackingId = "0",
                        Status = ServiceMessage.Success,
                        Type = TransactionType.Refound,
                        Price = (int)(productOffer.TotalPrice * productOffer.ReFoundsPercent),
                        ProductOfferId = productOffer.ProductOfferId,
                        PaymentGatewayId = defaultPaymentGateway.PaymentGatewayId,
                        Description = ServiceMessage.ReFoundPurchase
                    };
                    await _appUow.PaymentTransactionRepo.AddAsync(transaction);
                    #endregion

                    purchase.IsFinished = true;
                    purchase.IsReFoundable = false;
                    _appUow.PurchaseRepo.Update(purchase);

                    var oldBalance = user.Balance;
                    user.Balance += transaction.Price;
                    _appUow.UserRepo.Update(user);

                    var saveResult = await _appUow.ElkSaveChangesAsync();
                    response.Result = saveResult.IsSuccessful ? new { OldBalance = oldBalance, newBalance = user.Balance } : null;
                    response.IsSuccessful = saveResult.IsSuccessful;
                    response.Message = saveResult.Message;
                    await trans.CommitAsync();
                    return response;
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    FileLoger.Error(e);
                    response.Message = ServiceMessage.Exception;
                    return response;
                }
            }
        }

    }
}