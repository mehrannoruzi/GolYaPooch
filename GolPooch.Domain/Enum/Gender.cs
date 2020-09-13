using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum Gender : byte
    {
        [Description("زن")]
        Female = 0,

        [Description("مرد")]
        Male = 1
    }
}