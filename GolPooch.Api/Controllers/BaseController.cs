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


        /// <summary>
        /// Return Region Enum in key,Value model
        /// result of this method must be cached.
        /// </summary>
        /// <returns>List of Key,Value Object</returns>
        [HttpGet]
        public IActionResult Regions()
            => Ok(GetRegions());

    }
}