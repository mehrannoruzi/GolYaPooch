using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum LocationType : byte
    {
        [Description("بالای صفحه")]
        Top = 1,

        [Description("وسط صفحه")]
        Middle = 2,

        [Description("پایین صفحه")]
        Bottom = 3,

        [Description("تمام صفحه")]
        Full = 4
    }
}