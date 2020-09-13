using System;
using Elk.Core;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class PurchaseService : IPurchaseService
    {
        private AppUnitOfWork _appUow { get; set; }

        public PurchaseService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
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

    }
}