using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum ActivityLogType : byte
    {
        [Description("تایید کد")]
        VerifyCode = 1,

        [Description("ورود")]
        Login = 2,

        [Description("خروج")]
        LogOut = 3,

        [Description("خرید")]
        Purchase = 4
    }
}