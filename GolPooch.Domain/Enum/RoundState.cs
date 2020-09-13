namespace GolPooch.Domain.Enum
{
    public enum RoundState : byte
    {
        PreStart = 1,
        Start = 2,
        Close = 3,
        waitForPay = 4,


        End = 10
    }
}