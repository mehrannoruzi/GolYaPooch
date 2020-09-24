namespace GolPooch.Domain.Enum
{
    public enum RoundState : byte
    {
        Preparation = 1,
        Open = 2,
        Close = 3,
        waitForPay = 4,


        End = 10
    }
}