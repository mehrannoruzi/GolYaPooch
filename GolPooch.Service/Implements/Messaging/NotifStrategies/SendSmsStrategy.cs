using System;
using GolPooch.SmsGateway;
using GolPooch.Domain.Entity;
using GolPooch.DataAccess.Ef;
using System.Threading.Tasks;

namespace GolPooch.Service.Implements
{
    public class SendSmsStrategy : ISendNotifStrategy
    {
        public async Task SendAsync(Notification notification, AppUnitOfWork _appUow)
        {
            var sendResult = await SmsGatway.SendAsync(notification.User.MobileNumber.ToString(), notification.Text);

            notification.SentDateMi = DateTime.Now;
            notification.IsSuccess = sendResult.Result;
            notification.SendResultMessage = sendResult.Message;

            _appUow.NotificationRepo.Update(notification);
            await _appUow.SaveChangesAsync();
        }
    }
}
