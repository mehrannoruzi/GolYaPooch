namespace GolPooch.Domain.Enum
{
    public enum NotificationAction : byte
    {
        VerifyCode = 1,
        SuccessPurchase = 2,
        SuccessPayment = 3,

        NotifyWinners = 10
    }
}