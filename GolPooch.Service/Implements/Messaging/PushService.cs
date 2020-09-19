using System;
using WebPush;
using Elk.Core;
using System.Linq;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GolPooch.Service.Implements
{
    public class PushService : IPushService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly WebPushClient _webPushClient;
        private readonly IConfiguration _configuration;

        public PushService(AppUnitOfWork appUnitOfWork, IConfiguration configuration,
            WebPushClient webPushClient)
        {
            _appUow = appUnitOfWork;
            _webPushClient = webPushClient;
            _configuration = configuration;
        }

        public async Task<IResponse<bool>> Subscribe(PushEndpoint model)
        {
            var response = new Response<bool>();
            try
            {
                var endpoint = new PushEndpoint
                {
                    UserId = model.UserId,
                    Endpoint = model.Endpoint,
                    AuthSecretKey = model.AuthSecretKey,
                    P256DhSecretKey = model.P256DhSecretKey
                };
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

        public async Task<IResponse<bool>> Unsubscribe(int userId)
        {
            var response = new Response<bool>();
            try
            {
                var endpoint = await _appUow.PushEndpointRepo.FirstOrDefaultAsync(new QueryFilter<PushEndpoint> { Conditions = x => x.UserId == userId, AsNoTracking = false });
                if (endpoint.IsNull()) return new Response<bool> { Message = ServiceMessage.InvalidUserId };

                _appUow.PushEndpointRepo.Delete(endpoint);
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

        public async Task<IResponse<bool>> Unsubscribe(string P256DhSecretKey)
        {
            var response = new Response<bool>();
            try
            {
                var endpoint = await _appUow.PushEndpointRepo.FirstOrDefaultAsync(new QueryFilter<PushEndpoint> { Conditions = x => x.P256DhSecretKey == P256DhSecretKey, AsNoTracking = false });
                if (endpoint.IsNull()) return new Response<bool> { Message = ServiceMessage.InvalidParameter };

                _appUow.PushEndpointRepo.Delete(endpoint);
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

        public async Task<IResponse<bool>> Send(Notification notification)
        {
            var response = new Response<bool>();
            try
            {
                var allPushCount = 0;
                var failedSentCounter = 0;
                if (notification.UserId != null)
                {
                    #region Send To User
                    var endPoints = _appUow.PushEndpointRepo.Get(new QueryFilter<PushEndpoint> { Conditions = x => x.UserId == notification.UserId });
                    if (endPoints.IsNull() || endPoints.Count() == 0) return new Response<bool> { Message = ServiceMessage.InvalidUserId };
                    allPushCount = endPoints.Count();

                    foreach (var endpoint in endPoints)
                    {
                        try
                        {
                            var subscribtion = new PushSubscription(endpoint.Endpoint, endpoint.P256DhSecretKey, endpoint.AuthSecretKey);
                            var vapidDetails = new VapidDetails { PublicKey = _configuration["PushNotificationSetting:PublicKey"], PrivateKey = _configuration["PushNotificationSetting:PrivateKey"] };
                            await _webPushClient.SendNotificationAsync(subscribtion, notification.SerializeToJson(), vapidDetails);
                        }
                        catch (WebPushException e)
                        {
                            failedSentCounter++;
                            FileLoger.Error(e);
                            if (e.Message == "Subscription no longer valid") await Unsubscribe((int)notification.UserId);
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Send To All
                    var endPoints = _appUow.PushEndpointRepo.Get(new QueryFilter<PushEndpoint> { });
                    if (endPoints.IsNull() || endPoints.Count() == 0) return new Response<bool> { Message = ServiceMessage.PushSubscriberNotExist };
                    allPushCount = endPoints.Count();

                    foreach (var endPoint in endPoints)
                    {
                        try
                        {
                            var subscribtion = new PushSubscription(endPoint.Endpoint, endPoint.P256DhSecretKey, endPoint.AuthSecretKey);
                            var vapidDetails = new VapidDetails { PublicKey = _configuration["PushNotificationSetting:PublicKey"], PrivateKey = _configuration["PushNotificationSetting:PrivateKey"] };
                            await _webPushClient.SendNotificationAsync(subscribtion, notification.SerializeToJson(), vapidDetails);
                        }
                        catch (WebPushException e)
                        {
                            failedSentCounter++;
                            FileLoger.Error(e);
                            if (e.Message == "Subscription no longer valid") await Unsubscribe((int)notification.UserId);
                        }
                    }
                    #endregion
                }

                notification.SentDateMi = DateTime.Now;
                notification.IsSuccess = failedSentCounter == 0;
                notification.SendResultMessage = $"All Push Count: {allPushCount}, Failed Sent Count: {failedSentCounter}";
                _appUow.NotificationRepo.UpdateUnAttached(notification);
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