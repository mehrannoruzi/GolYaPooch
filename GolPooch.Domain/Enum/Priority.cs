using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum Priority : byte
    {
        [Description("اولویت پایین")]
        Low = 1,

        [Description("اولویت متوسط")]
        Medium = 2,

        [Description("اولویت زیاد")]
        High = 3
    }
}