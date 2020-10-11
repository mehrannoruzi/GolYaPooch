using System;
using Elk.Core;
using System.Linq;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class ChangeLogService : IChangeLogService
    {
        private AppUnitOfWork _appUow { get; set; }

        public ChangeLogService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
        }


        public async Task<IResponse<ChangeLog>> GetAsync(string version)
        {
            var response = new Response<ChangeLog>();
            try
            {
                var changeLog = await _appUow.ChangeLogRepo.FirstOrDefaultAsync(
                    new QueryFilter<ChangeLog>
                    {
                        Conditions = x => x.Version == version.Trim()
                    });

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = changeLog;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<PagingListDetails<ChangeLog>>> GetTopChangeAsync(PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<ChangeLog>>();
            try
            {
                var changeLogs = await _appUow.ChangeLogRepo.GetPagingAsync(
                    new PagingQueryFilter<ChangeLog>
                    {
                        PagingParameter = pagingParameter,
                        OrderBy = x => x.OrderByDescending(x => x.Version)
                    });

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = changeLogs;
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