namespace GolPooch.Domain.Enum
{
    public enum NotificationAction : byte
    {
        VerificationCode_Android = 1,
        VerificationCode_Ios = 2,
        SuccessPayment = 3,
    }
}