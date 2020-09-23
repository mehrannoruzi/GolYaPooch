using System;
using Elk.Core;
using Elk.Http;
using System.Linq;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GolPooch.Service.Implements
{
    public class PushService : IPushService
    {
        private AppUnitOfWork _appUow { get; set; }
        private readonly IConfiguration _configuration;

        public PushService(AppUnitOfWork appUnitOfWork, IConfiguration configuration)
        {
            _appUow = appUnitOfWork;
            _configuration = configuration;
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

        public async Task<IResponse<bool>> Unsubscribe(string pushKey)
        {
            var response = new Response<bool>();
            try
            {
                var endpoint = await _appUow.PushEndpointRepo.FirstOrDefaultAsync(new QueryFilter<PushEndpoint> { Conditions = x => x.PushKey == pushKey, AsNoTracking = false });
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

                    foreach (var endPoint in endPoints)
                    {
                        try
                        {
                            var requestBody = new
                            {
                                body = new
                                {
                                    collapse_key = "type_a",
                                    notification = new
                                    {
                                        body = notification.Text,
                                        title = notification.Subject,
                                        icon = notification.IconUrl,
                                    }
                                },
                                data = new
                                {
                                    notification.NotificationId,
                                    notification.UserId,
                                    notification.Type,
                                    notification.Action,
                                    notification.IsRead,
                                    notification.ActionText,
                                    notification.Subject,
                                    notification.ImageUrl,
                                    notification.IconUrl,
                                    notification.ActionUrl,
                                    notification.Text
                                },
                                to = endPoint.PushKey
                            };
                            var requestHeader = new Dictionary<string, string>
                            {
                                { "authorization", "key=" + _configuration["PushNotificationSetting:ServerKey"] },
                                { "content-type", "application/json" }
                            };
                            var pushResult = await HttpRequestTools.PostJsonAsync(
                                url: _configuration["PushNotificationSetting:FcmAddress"],
                                values: requestBody,
                                header: requestHeader);
                        }
                        catch (Exception e)
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
                            var requestBody = new
                            {
                                body = new
                                {
                                    collapse_key = "type_a",
                                    notification = new
                                    {
                                        body = notification.Text,
                                        title = notification.Subject,
                                        icon = notification.IconUrl,
                                    }
                                },
                                data = new
                                {
                                    notification.NotificationId,
                                    notification.UserId,
                                    notification.Type,
                                    notification.Action,
                                    notification.IsRead,
                                    notification.ActionText,
                                    notification.Subject,
                                    notification.ImageUrl,
                                    notification.IconUrl,
                                    notification.ActionUrl,
                                    notification.Text
                                },
                                to = endPoint.PushKey
                            };
                            var requestHeader = new Dictionary<string, string>
                            {
                                { "authorization", "key=" + _configuration["PushNotificationSetting:ServerKey"] },
                                { "content-type", "application/json" }
                            };
                            var pushResult = await HttpRequestTools.PostJsonAsync(
                                url: _configuration["PushNotificationSetting:FcmAddress"],
                                values: requestBody,
                                header: requestHeader);
                        }
                        catch (Exception e)
                        {
                            failedSentCounter++;
                            FileLoger.Error(e);
                            if (e.Message == "Subscription no longer valid") await Unsubscribe(endPoint.PushKey);
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