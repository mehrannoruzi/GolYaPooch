using System;
using Elk.Core;
using System.Linq;
using GolPooch.Domain.Dto;
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

        public async Task<IResponse<bool>> PurchaseAsync(PaymentTransaction transaction)
        {
            var response = new Response<bool>();
            try
            {
                var productOffer = await _appUow.ProductOfferRepo.FirstOrDefaultAsync(
                    new QueryFilter<ProductOffer>
                    {
                        Conditions = x => x.ProductOfferId == transaction.ProductOfferId && x.IsActive,
                    });
                if (productOffer.IsNull()) return new Response<bool> { Message = ServiceMessage.InvalidProductOfferId };

                var purchase = new Purchase
                {
                    UserId = transaction.UserId,
                    UsedChance = 0,
                    IsFinished = false,
                    IsReFoundable = true,
                    Chance = productOffer.Chance,
                    ProductOfferId = productOffer.ProductOfferId,
                    PaymentTransactionId = transaction.PaymentTransactionId
                };
                await _appUow.PurchaseRepo.AddAsync(purchase);
                var saveResult = await _appUow.ElkSaveChangesAsync();

                response.Message = saveResult.Message;
                response.Result = saveResult.IsSuccessful;
                response.IsSuccessful = saveResult.IsSuccessful;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<PagingListDetails<PurchaseDto>>> GetTopPurchases(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<PurchaseDto>>();
            try
            {
                var purchases = await _appUow.PurchaseRepo.GetPagingAsync(
                    new PagingQueryFilterWithSelector<Purchase, PurchaseDto>
                    {
                        Conditions = x => x.UserId == userId && !x.IsFinished && x.UsedChance < x.Chance,
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

    }
}