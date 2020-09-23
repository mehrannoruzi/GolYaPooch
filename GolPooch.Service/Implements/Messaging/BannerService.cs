﻿using System;
using Elk.Core;
using Elk.Cache;
using System.Linq;
using GolPooch.CrossCutting;
using GolPooch.Domain.Entity;
using GolPooch.DataAccess.Ef;
using System.Linq.Expressions;
using GolPooch.Service.Resourses;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class BannerService : IBannerService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _bannerCacheKey = GlobalVariables.CacheSettings.BannerCacheKey();

        public BannerService(AppUnitOfWork appUnitOfWork, IMemoryCacheProvider cacheProvider,
            IConfiguration configuration)
        {
            _appUow = appUnitOfWork;
            _cacheProvider = cacheProvider;
            _configuration = configuration;
        }

        public IResponse<List<Banner>> GetAllAvailable()
        {
            var response = new Response<List<Banner>>();
            try
            {
                var banners = (List<Banner>)_cacheProvider.Get(_bannerCacheKey);
                if (banners == null)
                {
                    var now = DateTime.Now;
                    banners = _appUow.BannerRepo.Get(
                        new QueryFilter<Banner>
                        {
                            Conditions = x => x.IsActive && x.ExpirationDate > now,
                            IncludeProperties = new List<Expression<Func<Banner, object>>> { x => x.Page },
                        }).ToList();

                    foreach (var banner in banners)
                    {
                        banner.IconUrl = banner.IconUrl != null
                              ? _configuration["CustomSettings:CdnAddress"] + banner.IconUrl
                              : null;
                        banner.ImageUrl = banner.ImageUrl != null
                                ? _configuration["CustomSettings:CdnAddress"] + banner.ImageUrl
                                : null;
                    }

                    _cacheProvider.Add(_bannerCacheKey, banners, DateTime.Now.AddHours(GlobalVariables.CacheSettings.BannerCacheTimeout()));
                }

                response.Result = banners;
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