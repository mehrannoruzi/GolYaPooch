using System;
using Elk.Core;
using Elk.Cache;
using System.Linq;
using GolPooch.Domain.Dto;
using GolPooch.CrossCutting;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class PaymentService : IPaymentService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _paymentGatewayCacheKey = GlobalVariables.CacheSettings.PaymentGatewayCacheKey();

        public PaymentService(AppUnitOfWork appUnitOfWork, IMemoryCacheProvider cacheProvider)
        {
            _appUow = appUnitOfWork;
            _cacheProvider = cacheProvider;
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
                                PaymentGatewayId = x.PaymentGatewayId
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


    }
}