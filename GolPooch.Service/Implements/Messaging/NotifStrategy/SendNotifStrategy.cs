using GolPooch.Domain.Enum;
using GolPooch.DataAccess.Ef;

namespace GolPooch.Service.Implements
{
    public static class SendNotifStrategy
    {
        public static ISendNotifStrategy GetStrategy(NotificationType type, AppUnitOfWork appUow)
        {
            switch (type)
            {
                case NotificationType.Sms:
                    return new SendSmsStrategy(appUow);
                case NotificationType.TelegramBot:
                    return new SendTelegramStrategy(appUow);
                case NotificationType.PushNotification:
                    return new SendPushStrategy(appUow);
                default:
                    return new SendPushStrategy(appUow);
            }
        }
    }
}
