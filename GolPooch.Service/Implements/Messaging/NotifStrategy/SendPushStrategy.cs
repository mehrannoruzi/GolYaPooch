using System;
using Elk.Core;
using Elk.Http;
using System.Linq;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using GolPooch.Service.Resourses;

namespace GolPooch.Service.Implements
{
    public class SendPushStrategy : ISendNotifStrategy
    {
        private AppUnitOfWork _appUow { get; set; }

        public SendPushStrategy(AppUnitOfWork appUow)
        {
            _appUow = appUow;
        }

        public async Task SendAsync(Notification notification)
        {
            //var allPushCount = 0;
            //var failedSentCounter = 0;
            //if (notification.UserId != null)
            //{
            //    #region Send To User
            //    var endPoints = _appUow.PushEndpointRepo.Get(new QueryFilter<PushEndpoint> { Conditions = x => x.UserId == notification.UserId });
            //    if (endPoints.IsNull() || endPoints.Count() == 0) return;
            //    allPushCount = endPoints.Count();

            //    foreach (var endPoint in endPoints)
            //    {
            //        try
            //        {
            //            #region Create Push Request For FCM
            //            var requestBody = new
            //            {
            //                body = new
            //                {
            //                    collapse_key = "type_a",
            //                    notification = new
            //                    {
            //                        body = notification.Text,
            //                        title = notification.Subject,
            //                        icon = notification.IconUrl,
            //                    }
            //                },
            //                data = new
            //                {
            //                    notification.NotificationId,
            //                    notification.UserId,
            //                    notification.Type,
            //                    notification.Action,
            //                    notification.IsRead,
            //                    notification.ActionText,
            //                    notification.Subject,
            //                    notification.ImageUrl,
            //                    notification.IconUrl,
            //                    notification.ActionUrl,
            //                    notification.Text
            //                },
            //                to = endPoint.PushKey
            //            };
            //            var requestHeader = new Dictionary<string, string>
            //                {
            //                    { "authorization", "key=" + _configuration["PushNotificationSetting:ServerKey"] },
            //                    { "content-type", "application/json" }
            //                };
            //            #endregion

            //            var pushResult = await HttpRequestTools.PostJsonAsync(
            //                url: _configuration["PushNotificationSetting:FcmAddress"],
            //                values: requestBody,
            //                header: requestHeader);
            //        }
            //        catch (Exception e)
            //        {
            //            failedSentCounter++;
            //            FileLoger.Error(e);
            //            if (e.Message.Contains("Subscription no longer valid")) await Unsubscribe((int)notification.UserId);
            //        }
            //    }
            //    #endregion
            //}
            //else
            //{
            //    #region Send To All
            //    var endPoints = _appUow.PushEndpointRepo.Get(new QueryFilter<PushEndpoint> { });
            //    if (endPoints.IsNull() || endPoints.Count() == 0) return new Response<bool> { Message = ServiceMessage.PushSubscriberNotExist };
            //    allPushCount = endPoints.Count();

            //    foreach (var endPoint in endPoints)
            //    {
            //        try
            //        {
            //            var requestBody = new
            //            {
            //                body = new
            //                {
            //                    collapse_key = "type_a",
            //                    notification = new
            //                    {
            //                        body = push.Text,
            //                        title = push.Subject,
            //                        icon = push.IconUrl,
            //                    }
            //                },
            //                data = new
            //                {
            //                    push.NotificationId,
            //                    push.UserId,
            //                    push.Type,
            //                    push.Action,
            //                    push.IsRead,
            //                    push.ActionText,
            //                    push.Subject,
            //                    push.ImageUrl,
            //                    push.IconUrl,
            //                    push.ActionUrl,
            //                    push.Text
            //                },
            //                to = endPoint.PushKey
            //            };
            //            var requestHeader = new Dictionary<string, string>
            //                {
            //                    { "authorization", "key=" + _configuration["PushNotificationSetting:ServerKey"] },
            //                    { "content-type", "application/json" }
            //                };
            //            var pushResult = await HttpRequestTools.PostJsonAsync(
            //                url: _configuration["PushNotificationSetting:FcmAddress"],
            //                values: requestBody,
            //                header: requestHeader);
            //        }
            //        catch (Exception e)
            //        {
            //            failedSentCounter++;
            //            FileLoger.Error(e);
            //            if (e.Message == "Subscription no longer valid") await Unsubscribe(endPoint.PushKey);
            //        }
            //    }
            //    #endregion
            //}

            //notification.SentDateMi = DateTime.Now;
            //notification.IsSuccess = failedSentCounter == 0;
            //notification.SendResultMessage = $"All Push Count: {allPushCount}, Failed Sent Count: {failedSentCounter}";
            //_appUow.NotificationRepo.UpdateUnAttached(notification);
            //var saveResult = await _appUow.ElkSaveChangesAsync();
        }
    }
}