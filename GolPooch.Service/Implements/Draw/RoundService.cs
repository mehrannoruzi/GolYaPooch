using System;
using Elk.Core;
using System.Linq;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Linq.Expressions;
using GolPooch.Service.Resourses;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class RoundService : IRoundService
    {
        private AppUnitOfWork _appUow { get; set; }

        public RoundService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
        }


        public async Task<IResponse<PagingListDetails<object>>> GetTopRoundsAsync(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<object>>();
            try
            {
                var rounds = await _appUow.DrawChanceRepo.GetPagingAsync(
                    new PagingQueryFilterWithSelector<DrawChance, object>
                    {
                        Conditions = x => x.UserId == userId,
                        IncludeProperties = new List<Expression<Func<DrawChance, object>>>
                        {
                            x => x.Round,
                            x => x.Round.Chest
                        },
                        PagingParameter = pagingParameter,
                        OrderBy = x => x.OrderByDescending(x => x.Round.RoundId),
                        Selector = x => new
                        {
                            #region Set RoundChance Property
                            Chest = x.Round.Chest,
                            RoundId = x.Round.RoundId,
                            RoundState = x.Round.State,
                            OpenDateSh = x.Round.OpenDateSh,
                            CloseDateSh = x.Round.CloseDateSh,
                            DrawDateSh = x.Round.DrawDateSh,
                            #endregion
                        }
                    });

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = rounds;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<List<object>>> GetChanceInRoundAsync(int roundId)
        {
            var response = new Response<List<object>>();
            try
            {
                var chances = await _appUow.DrawChanceRepo.GetAsync(
                    new QueryFilterWithSelector<DrawChance, object>
                    {
                        Conditions = x => x.Round.RoundId == roundId,
                        IncludeProperties = new List<Expression<Func<DrawChance, object>>> { x => x.Round },
                        Selector = x => new
                        {
                            x.Code,
                            x.Counter,
                            x.InsertDateSh,
                            x.Round.OpenDateSh,
                            x.Round.CloseDateSh,
                            x.Round.DrawDateSh
                        }
                    });

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = chances;
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