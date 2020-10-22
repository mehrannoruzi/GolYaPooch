namespace GolPooch.Domain.Enum
{
    public enum NotificationAction : byte
    {
        VerifyCode = 1,
        SuccessPurchase = 2,
        SuccessPayment = 3,

        SpendChance = 9,
        NotifyWinners = 10,
        PayBackPurchase = 11
    }
}