using GolPooch.Domain.Enum;

namespace GolPooch.Service.Implements
{
    public static class SendNotifFactory
    {
        public static ISendNotifStrategy GetStrategy(NotificationType type)
        {
            return type switch
            {
                NotificationType.Sms => new SendSmsStrategy(),
                NotificationType.TelegramBot => new SendTelegramStrategy(),
                NotificationType.PushNotification => new SendPushStrategy(),
                _ => new NoActionStrategy(),
            };
        }
    }
}