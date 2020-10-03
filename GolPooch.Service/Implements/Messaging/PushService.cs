using System;
using Elk.Core;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class PushService : IPushService
    {
        private AppUnitOfWork _appUow { get; set; }

        public PushService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
        }

        public async Task<IResponse<bool>> Subscribe(PushEndpoint endpoint)
        {
            var response = new Response<bool>();
            try
            {
                if (string.IsNullOrEmpty(endpoint.PushKey)) return new Response<bool> { Message = ServiceMessage.InvalidParameter };
                await _appUow.PushEndpointRepo.AddAsync(endpoint);
                var saveResult = await _appUow.ElkSaveChangesAsync();

                response.Message = saveResult.Message;
                response.Result = saveResult.IsSuccessful;
                response.IsSuccessful = saveResult.IsSuccessful;
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