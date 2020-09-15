using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum ActionType : byte
    {
        [Description("بسته شود")]
        Close = 1,

        [Description("صدا زدن Api")]
        CallApi = 2,

        [Description("بازکردن لینک")]
        OpenLink = 3,

        [Description("بازکردن اپلیکیشن")]
        OpenApp = 3,

        [Description("بازکردن صفحه")]
        OpenPage = 3,

    }
}