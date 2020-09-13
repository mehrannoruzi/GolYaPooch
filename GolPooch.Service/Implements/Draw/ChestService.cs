using System;
using Elk.Core;
using Elk.Cache;
using System.Linq;
using GolPooch.CrossCutting;
using GolPooch.Domain.Entity;
using GolPooch.DataAccess.Ef;
using GolPooch.Service.Resourses;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class ChestService : IChestService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _chestCacheKey = GlobalVariables.CacheSettings.ChestCacheKey();

        public ChestService(AppUnitOfWork appUnitOfWork, IMemoryCacheProvider cacheProvider)
        {
            _appUow = appUnitOfWork;
            _cacheProvider = cacheProvider;
        }

        public IResponse<List<Chest>> GetAllAvailable()
        {
            var response = new Response<List<Chest>>();
            try
            {
                var chests = (List<Chest>)_cacheProvider.Get(_chestCacheKey);
                if (chests == null)
                {
                    var now = DateTime.Now;
                    chests = _appUow.ChestRepo.Get(
                        new QueryFilter<Chest>
                        {
                            Conditions = x => x.IsActive,
                            OrderBy = x => x.OrderByDescending(x => x.ParticipantCount)
                        }).ToList();

                    _cacheProvider.Add(_chestCacheKey, chests, DateTime.Now.AddHours(GlobalVariables.CacheSettings.ChestCacheTimeout()));
                }

                response.Result = chests;
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