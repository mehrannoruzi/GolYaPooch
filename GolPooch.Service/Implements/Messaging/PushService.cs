using System;
using Elk.Core;
using Elk.Http;
using System.Linq;
using GolPooch.Domain.Enum;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Collections.Generic;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Configuration;

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

        private async Task<IResponse<bool>> Unsubscribe(int userId)
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

        private async Task<IResponse<bool>> Unsubscribe(string pushKey)
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

        private async Task<IResponse<bool>> Send(Notification push)
        {
            var response = new Response<bool>();
            try
            {
                var allPushCount = 0;
                var failedSentCounter = 0;
                if (push.UserId != null)
                {
                    #region Send To User
                    var endPoints = _appUow.PushEndpointRepo.Get(new QueryFilter<PushEndpoint> { Conditions = x => x.UserId == push.UserId });
                    if (endPoints.IsNull() || endPoints.Count() == 0) return new Response<bool> { Message = ServiceMessage.InvalidUserId };
                    allPushCount = endPoints.Count();

                    foreach (var endPoint in endPoints)
                    {
                        try
                        {
                            #region Create Push Request For FCM
                            var requestBody = new
                            {
                                body = new
                                {
                                    collapse_key = "type_a",
                                    notification = new
                                    {
                                        body = push.Text,
                                        title = push.Subject,
                                        icon = push.IconUrl,
                                    }
                                },
                                data = new
                                {
                                    push.NotificationId,
                                    push.UserId,
                                    push.Type,
                                    push.Action,
                                    push.IsRead,
                                    push.ActionText,
                                    push.Subject,
                                    push.ImageUrl,
                                    push.IconUrl,
                                    push.ActionUrl,
                                    push.Text
                                },
                                to = endPoint.PushKey
                            };
                            var requestHeader = new Dictionary<string, string>
                            {
                                { "authorization", "key=" + _configuration["PushNotificationSetting:ServerKey"] },
                                { "content-type", "application/json" }
                            };
                            #endregion

                            var pushResult = await HttpRequestTools.PostJsonAsync(
                                url: _configuration["PushNotificationSetting:FcmAddress"],
                                values: requestBody,
                                header: requestHeader);
                        }
                        catch (Exception e)
                        {
                            failedSentCounter++;
                            FileLoger.Error(e);
                            if (e.Message.Contains("Subscription no longer valid")) await Unsubscribe((int)push.UserId);
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
                                        body = push.Text,
                                        title = push.Subject,
                                        icon = push.IconUrl,
                                    }
                                },
                                data = new
                                {
                                    push.NotificationId,
                                    push.UserId,
                                    push.Type,
                                    push.Action,
                                    push.IsRead,
                                    push.ActionText,
                                    push.Subject,
                                    push.ImageUrl,
                                    push.IconUrl,
                                    push.ActionUrl,
                                    push.Text
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

                push.SentDateMi = DateTime.Now;
                push.IsSuccess = failedSentCounter == 0;
                push.SendResultMessage = $"All Push Count: {allPushCount}, Failed Sent Count: {failedSentCounter}";
                _appUow.NotificationRepo.UpdateUnAttached(push);
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

        public async Task SendPush()
        {
            try
            {
                var pushes = _appUow.NotificationRepo.Get(
                    new QueryFilter<Notification>
                    {
                        AsNoTracking = false,
                        OrderBy = x => x.OrderBy(x => x.NotificationId),
                        Conditions = x => x.Type == NotificationType.PushNotification && x.IsActive && !x.IsSent
                    });

                if (pushes.IsNotNull() && pushes.Count() > 0)
                {
                    foreach (var push in pushes)
                        push.IsSent = true;

                    _appUow.NotificationRepo.UpdateRange(pushes);
                    await _appUow.SaveChangesAsync();

                    foreach (var push in pushes)
                        await Send(push);
                }

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                await Task.FromException(e);
            }
        }

    }
}