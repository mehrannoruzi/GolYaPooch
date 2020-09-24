﻿using System;
using Elk.Core;
using Elk.Cache;
using GolPooch.Api.Models;
using GolPooch.Domain.Enum;
using GolPooch.CrossCutting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class BaseController : Controller
    {
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _regionCacheKey = GlobalVariables.CacheSettings.RegionCacheKey();
        private readonly string _ticketTypeCacheKey = GlobalVariables.CacheSettings.TiketTypeCacheKey();
        private readonly string _bannerTypeCacheKey = GlobalVariables.CacheSettings.BannerTypeCacheKey();
        private readonly string _bankNameCacheKey = GlobalVariables.CacheSettings.BankNameCacheKey();

        public BaseController(IMemoryCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        private List<KeyValueModel> GetRegions()
        {
            var response = new List<KeyValueModel>();
            try
            {
                response = (List<KeyValueModel>)_cacheProvider.Get(_regionCacheKey);
                if (response == null)
                {
                    response = new List<KeyValueModel>();
                    EnumExtension.GetEnumElements<RegionNames>()
                        .ForEach(element =>
                        {
                            response.Add(new KeyValueModel { Title = element.Description, Name = element.Name, Value = int.Parse(element.Value.ToString()) });
                        });

                    _cacheProvider.Add(_regionCacheKey, response, DateTime.Now.AddHours(GlobalVariables.CacheSettings.RegionCacheTimeout()));
                }

                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return response;
            }
        }

        private List<KeyValueModel> BankNames()
        {
            var response = new List<KeyValueModel>();
            try
            {
                response = (List<KeyValueModel>)_cacheProvider.Get(_bankNameCacheKey);
                if (response == null)
                {
                    response = new List<KeyValueModel>();
                    EnumExtension.GetEnumElements<BankNames>()
                        .ForEach(element =>
                        {
                            response.Add(new KeyValueModel { Title = element.Description, Name = element.Name, Value = int.Parse(element.Value.ToString()) });
                        });

                    _cacheProvider.Add(_bankNameCacheKey, response, DateTime.Now.AddHours(GlobalVariables.CacheSettings.BankNameCacheTimeout()));
                }

                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return response;
            }
        }

        private List<KeyValueModel> GetTicketTypes()
        {
            var response = new List<KeyValueModel>();
            try
            {
                response = (List<KeyValueModel>)_cacheProvider.Get(_ticketTypeCacheKey);
                if (response == null)
                {
                    response = new List<KeyValueModel>();
                    EnumExtension.GetEnumElements<TicketType>()
                        .ForEach(element =>
                        {
                            response.Add(new KeyValueModel { Title = element.Description, Name = element.Name, Value = int.Parse(element.Value.ToString()) });
                        });

                    _cacheProvider.Add(_ticketTypeCacheKey, response, DateTime.Now.AddHours(GlobalVariables.CacheSettings.TiketTypeCacheTimeout()));
                }

                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return response;
            }
        }

        private List<KeyValueModel> GetBannerTypes()
        {
            var response = new List<KeyValueModel>();
            try
            {
                response = (List<KeyValueModel>)_cacheProvider.Get(_bannerTypeCacheKey);
                if (response == null)
                {
                    response = new List<KeyValueModel>();
                    EnumExtension.GetEnumElements<BannerType>()
                        .ForEach(element =>
                        {
                            response.Add(new KeyValueModel { Title = element.Description, Name = element.Name, Value = int.Parse(element.Value.ToString()) });
                        });

                    _cacheProvider.Add(_bannerTypeCacheKey, response, DateTime.Now.AddHours(GlobalVariables.CacheSettings.BannerTypeCacheTimeout()));
                }

                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                return response;
            }
        }



        [HttpGet]
        public IActionResult AllSettings()
        {
            return Ok(new {
                Regions = GetRegions(),
                BankNames = BankNames(),
                TicketTypes = GetTicketTypes(),
                BannerTypes = GetBannerTypes(),
            });
        }

        [HttpGet]
        public IActionResult Regions()
            => Ok(GetRegions());

        [HttpGet]
        public IActionResult TicketTypes()
            => Ok(GetTicketTypes());

    }
}