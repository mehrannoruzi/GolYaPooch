using System;
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
        private readonly string _bankNameCacheKey = GlobalVariables.CacheSettings.BankNameCacheKey();

        public BaseController(IMemoryCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        private List<KeyValue> GetRegions()
        {
            var response = new List<KeyValue>();
            try
            {
                response = (List<KeyValue>)_cacheProvider.Get(_regionCacheKey);
                if (response == null)
                {
                    response = new List<KeyValue>();
                    EnumExtension.GetEnumElements<RegionNames>()
                        .ForEach(element =>
                        {
                            response.Add(new KeyValue { Title = element.Description, Name = element.Name, Value = int.Parse(element.Value.ToString()) });
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

        private List<KeyValue> BankNames()
        {
            var response = new List<KeyValue>();
            try
            {
                response = (List<KeyValue>)_cacheProvider.Get(_bankNameCacheKey);
                if (response == null)
                {
                    response = new List<KeyValue>();
                    EnumExtension.GetEnumElements<BankNames>()
                        .ForEach(element =>
                        {
                            response.Add(new KeyValue { Title = element.Description, Name = element.Name, Value = int.Parse(element.Value.ToString()) });
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

        private List<KeyValue> GetTicketTypes()
        {
            var response = new List<KeyValue>();
            try
            {
                response = (List<KeyValue>)_cacheProvider.Get(_ticketTypeCacheKey);
                if (response == null)
                {
                    response = new List<KeyValue>();
                    EnumExtension.GetEnumElements<TicketType>()
                        .ForEach(element =>
                        {
                            response.Add(new KeyValue { Title = element.Description, Name = element.Name, Value = int.Parse(element.Value.ToString()) });
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



        [HttpGet]
        public IActionResult AllSettings()
        {
            return Ok(new {
                Regions = GetRegions(),
                BankNames = BankNames(),
                TicketTypes = GetTicketTypes(),
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