using System;
using Elk.Core;
using Elk.Http;
using System.Linq;
using GolPooch.CrossCutting;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

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

            var endPoints = await _appUow.PushEndpointRepo.GetAsync(new QueryFilter<PushEndpoint> { Conditions = x => x.UserId == notification.UserId });
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
                        priority = "high",
                        to = endPoint.PushKey
                    };
                    var requestHeader = new Dictionary<string, string>
                            {
                                //{ "content-type", "application/json" },
                                { "Sender", $" id={GlobalVariables.PushNotificationSetting.SenderID}" },
                                { "authorization", $" key={GlobalVariables.PushNotificationSetting.ServerKey}" }
                            };
                    #endregion

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={GlobalVariables.PushNotificationSetting.SenderID}");
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={GlobalVariables.PushNotificationSetting.ServerKey}");

                        var httpContent = new StringContent(requestBody.SerializeToJson(), Encoding.UTF8, "application/json");
                        var requestResult = await client.PostAsync(GlobalVariables.PushNotificationSetting.FcmAddress, httpContent);
                        var result = await requestResult.Content.ReadAsStringAsync();
                        if(result.Contains("NotRegisterd")) await Unsubscribe(endPoint, _appUow);
                    }
                }
                catch (Exception e)
                {
                    failedSentCounter++;
                    FileLoger.Error(e);
                    if (e.Message.Contains("NotRegisterd")) await Unsubscribe(endPoint, _appUow);
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