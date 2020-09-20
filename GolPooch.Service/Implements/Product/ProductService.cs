using System;
using Elk.Core;
using Elk.Cache;
using System.Linq;
using GolPooch.CrossCutting;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class ProductService : IProductService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _productCacheKey = GlobalVariables.CacheSettings.ProductCacheKey();

        public ProductService(AppUnitOfWork appUnitOfWork, IConfiguration configuration,
            IMemoryCacheProvider cacheProvider)
        {
            _appUow = appUnitOfWork;
            _configuration = configuration;
            _cacheProvider = cacheProvider;
        }

        public IResponse<List<ProductOffer>> GetAllAvailable()
        {
            var response = new Response<List<ProductOffer>>();
            try
            {
                var productOffers = (List<ProductOffer>)_cacheProvider.Get(_productCacheKey);
                if (productOffers == null)
                {
                    productOffers = _appUow.ProductOfferRepo.Get(
                        new QueryFilter<ProductOffer>
                        {
                            Conditions = x => x.IsActive,
                            OrderBy = x => x.OrderBy(x => x.Price),
                            IncludeProperties = new List<System.Linq.Expressions.Expression<Func<ProductOffer, object>>> { x => x.Product }
                        }).ToList();

                    foreach (var offer in productOffers)
                        offer.ImageUrl = offer.ImageUrl != null
                            ? _configuration["CustomSettings:CdnAddress"] + offer.ImageUrl
                            : null;

                    _cacheProvider.Add(_productCacheKey, productOffers, DateTime.Now.AddHours(GlobalVariables.CacheSettings.ProductCacheTimeout()));
                }

                response.IsSuccessful = true;
                response.Result = productOffers;
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