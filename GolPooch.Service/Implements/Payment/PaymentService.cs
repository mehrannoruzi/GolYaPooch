using System;
using Elk.Core;
using Elk.Cache;
using System.Linq;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Enum;
using GolPooch.CrossCutting;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class PaymentService : IPaymentService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _paymentGatewayCacheKey = GlobalVariables.CacheSettings.PaymentGatewayCacheKey();

        public PaymentService(AppUnitOfWork appUnitOfWork, IMemoryCacheProvider cacheProvider,
            IConfiguration configuration)
        {
            _appUow = appUnitOfWork;
            _cacheProvider = cacheProvider;
            _configuration = configuration;
        }

        public IResponse<List<PaymentGatwayDto>> GetAllGateway()
        {
            var response = new Response<List<PaymentGatwayDto>>();
            try
            {
                var gatways = (List<PaymentGatwayDto>)_cacheProvider.Get(_paymentGatewayCacheKey);
                if (gatways == null)
                {
                    gatways = _appUow.PaymentGatewayRepo.Get(
                        new QueryFilterWithSelector<PaymentGateway, PaymentGatwayDto>
                        {
                            Conditions = x => x.IsActive,
                            OrderBy = x => x.OrderByDescending(x => x.IsDefault),
                            Selector = x => new PaymentGatwayDto
                            {
                                Name = x.Name,
                                BankName = x.BankName,
                                IsActive = x.IsActive,
                                IsDefault = x.IsDefault,
                                PaymentGatewayId = x.PaymentGatewayId,
                                ImageUrl = _configuration["CustomSettings:CdnAddress"] + $"Assets/BanksImage/{x.BankName}.png"
                            }
                        }).ToList();

                    _cacheProvider.Add(_paymentGatewayCacheKey, gatways, DateTime.Now.AddHours(GlobalVariables.CacheSettings.PaymentGatewayCacheTimeout()));
                }

                response.Result = gatways;
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

        public async Task<IResponse<string>> CreateAsync(PaymentTransaction paymentTransaction)
        {
            var response = new Response<string>();
            try
            {
                var paymentGatway = await _appUow.PaymentGatewayRepo.FirstOrDefaultAsync(
                    new QueryFilter<PaymentGateway>
                    {
                        Conditions = x => x.IsActive && x.PaymentGatewayId == paymentTransaction.PaymentGatewayId
                    });
                if (paymentGatway.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidPaymentGateway };

                var productOffer = await _appUow.ProductOfferRepo.FirstOrDefaultAsync(
                    new QueryFilter<ProductOffer>
                    {
                        Conditions = x => x.IsActive && x.ProductOfferId == paymentTransaction.ProductOfferId,
                        IncludeProperties = new List<Expression<Func<ProductOffer, object>>> { x => x.Product }
                    });
                if (productOffer.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidProductOffer };

                paymentTransaction.Price = productOffer.TotalPrice;
                paymentTransaction.Type = TransactionType.Purchase;
                await _appUow.PaymentTransactionRepo.AddAsync(paymentTransaction);
                var saveResult = await _appUow.ElkSaveChangesAsync();

                var redirectUrl = string.Empty;
                if (saveResult.IsSuccessful)
                {
                    // response.Result = $"https://localhost:44349/Payment/ZarinPalVerify?"+
                    response.Result = $"{_configuration["PaymentGatewaySettings:GatwayCallbackUrl_Zarinpal"]}" +
                        $"PaymentTransactionId={paymentTransaction.PaymentTransactionId}&" +
                        $"Status=OK&Authority=123456789";
                    response.IsSuccessful = true;
                    response.Message = saveResult.Message;

                    //var createPaymentResult = await PaymentFactory.GetInstance(paymentGatway.BankName)
                    //    .CreateAsync(_appUow, paymentTransaction, _configuration);

                    //response.Result = createPaymentResult.Result;
                    //response.Message = createPaymentResult.Message;
                    //response.IsSuccessful = createPaymentResult.IsSuccessful;
                }
                else
                {
                    response.Message = saveResult.Message;
                    response.IsSuccessful = saveResult.IsSuccessful;
                }

                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<string>> VerifyAsync(int paymentTransactionId, string Status, string Authority)
        {
            var response = new Response<string>();
            try
            {
                var paymentTransaction = await _appUow.PaymentTransactionRepo.FirstOrDefaultAsync(
                    new QueryFilter<PaymentTransaction>
                    {
                        AsNoTracking = false,
                        Conditions = x => x.PaymentTransactionId == paymentTransactionId
                    });
                if (paymentTransaction.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidPaymentTransaction };

                var paymentGatway = await _appUow.PaymentGatewayRepo.FirstOrDefaultAsync(
                    new QueryFilter<PaymentGateway>
                    {
                        Conditions = x => x.IsActive && x.PaymentGatewayId == paymentTransaction.PaymentGatewayId
                    });
                if (paymentGatway.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidPaymentGateway };

                var productOffer = await _appUow.ProductOfferRepo.FirstOrDefaultAsync(
                    new QueryFilter<ProductOffer>
                    {
                        Conditions = x => x.IsActive && x.ProductOfferId == paymentTransaction.ProductOfferId,
                        IncludeProperties = new List<Expression<Func<ProductOffer, object>>> { x => x.Product }
                    });
                if (productOffer.IsNull()) return new Response<string> { Message = ServiceMessage.InvalidProductOffer };

                //var verifyResult = await PaymentFactory.GetInstance(paymentGatway.BankName)
                //    .VerifyAsync(_appUow, paymentTransaction, _configuration);

                paymentTransaction.IsSuccess = true;
                paymentTransaction.Status = Status;
                paymentTransaction.TrackingId = Randomizer.GetUniqueKey(12);
                _appUow.PaymentTransactionRepo.Update(paymentTransaction);
                var verifyResult = await _appUow.ElkSaveChangesAsync();

                if (verifyResult.IsSuccessful)
                {
                    response.Result = $"{_configuration["CustomSettings:ReactPaymentGatewayResultUrl"]}" +
                        $"IsSuccessful={verifyResult.IsSuccessful}&" +
                        $"TrackingId={paymentTransaction.TrackingId}&" +
                        $"ProductSubject={productOffer.Product.Subject}&" +
                        $"ProductText={productOffer.Product.Text}&" +
                        $"Date={paymentTransaction.ModifyDateSh}&" +
                        $"Time={paymentTransaction.ModifyDateMi.ToLongTimeString()}&" +
                        $"Price={paymentTransaction.Price}&" +
                        $"Chance={productOffer.Chance}";
                    response.Message = verifyResult.Message;
                    response.IsSuccessful = verifyResult.IsSuccessful;
                }
                else
                {
                    response.Message = verifyResult.Message;
                    response.IsSuccessful = verifyResult.IsSuccessful;
                }

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