using System;
using Elk.Core;
using Elk.Http;
using System.Linq;
using GolPooch.CrossCutting;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.Service.Implements
{
    public class SendPushStrategy : ISendNotifStrategy
    {
        private async Task Unsubscribe(PushEndpoint pushEndpoint, AppUnitOfWork _appUow)
        {
            try
            {
                _appUow.PushEndpointRepo.Delete(pushEndpoint);
                await _appUow.SaveChangesAsync();
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
            }
        }

        public async Task SendAsync(Notification notification, AppUnitOfWork _appUow)
        {
            var allPushCount = 0;
            var failedSentCounter = 0;

            var endPoints = _appUow.PushEndpointRepo.Get(new QueryFilter<PushEndpoint> { Conditions = x => x.UserId == notification.UserId });
            if (!endPoints.Any()) await Task.CompletedTask;

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
                                { "authorization", "key=" + GlobalVariables.PushNotificationSetting.ServerKey},
                                { "content-type", "application/json" }
                            };
                    #endregion

                    var pushResult = await HttpRequestTools.PostJsonAsync(
                        url: GlobalVariables.PushNotificationSetting.FcmAddress,
                        values: requestBody,
                        header: requestHeader);
                }
                catch (Exception e)
                {
                    failedSentCounter++;
                    FileLoger.Error(e);
                    if (e.Message.Contains("Subscription no longer valid")) await Unsubscribe(endPoint, _appUow);
                }
            }

            notification.SentDateMi = DateTime.Now;
            notification.IsSuccess = failedSentCounter == 0;
            notification.SendResultMessage = $"All Push Count: {allPushCount}, Failed Sent Count: {failedSentCounter}";
            _appUow.NotificationRepo.UpdateUnAttached(notification);
            var saveResult = await _appUow.ElkSaveChangesAsync();
        }
    }
}