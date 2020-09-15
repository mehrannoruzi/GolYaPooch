using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum DisplayType : byte
    {
        [Description("یکبار")]
        Once = 1,

        [Description("همیشه")]
        Always = 2
    }
}