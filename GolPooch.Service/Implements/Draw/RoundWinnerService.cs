using System;
using Elk.Core;
using Elk.Cache;
using GolPooch.Domain.Dto;
using GolPooch.CrossCutting;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;
using GolPooch.Service.Resourses;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class RoundWinnerService : IRoundWinnerService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IMemoryCacheProvider _cacheProvider;
        private readonly string _lastWinnerCacheKey = GlobalVariables.CacheSettings.LastWinnerCacheKey();
        private readonly string _mustWinnerCacheKey = GlobalVariables.CacheSettings.MustWinnerCacheKey();


        public RoundWinnerService(AppUnitOfWork appUnitOfWork, IMemoryCacheProvider cacheProvider)
        {
            _appUow = appUnitOfWork;
            _cacheProvider = cacheProvider;
        }


        public async Task<IResponse<IEnumerable<WinnerDto>>> GetLastWinnerAsync(PagingParameter pagingParameter)
        {
            var response = new Response<IEnumerable<WinnerDto>>();
            try
            {
                var winners = (IEnumerable<WinnerDto>)_cacheProvider.Get(_lastWinnerCacheKey);
                if (winners == null)
                {
                    winners = await _appUow.RoundWinnerRepo.GetLastWinnersAsync(pagingParameter);

                    foreach (var winner in winners)
                        winner.MobileNumber = winner.MobileNumber
                            .CustomMask(new MaskOption
                            {
                                MaskLength = 3,
                                MaskWith = '*',
                                Mode = MaskMode.Middle
                            });

                    _cacheProvider.Add(_lastWinnerCacheKey, winners, DateTime.Now.AddHours(GlobalVariables.CacheSettings.LastWinnerCacheTimeout()));
                }

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = winners;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<IEnumerable<WinnerDto>>> GetMustWinnerAsync(PagingParameter pagingParameter)
        {
            var response = new Response<IEnumerable<WinnerDto>>();
            try
            {
                var winners = (IEnumerable<WinnerDto>)_cacheProvider.Get(_mustWinnerCacheKey);
                if (winners == null)
                {
                    winners = await _appUow.RoundWinnerRepo.GetMustWinnersAsync(pagingParameter);

                    foreach (var winner in winners)
                        winner.MobileNumber = winner.MobileNumber
                            .CustomMask(new MaskOption
                            {
                                MaskLength = 3,
                                MaskWith = '*',
                                Mode = MaskMode.Middle
                            });

                    _cacheProvider.Add(_mustWinnerCacheKey, winners, DateTime.Now.AddHours(GlobalVariables.CacheSettings.MustWinnerCacheTimeout()));
                }

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = winners;
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