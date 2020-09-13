using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum TicketType
    {
        [Description("سوال")]
        Soal = 1,

        [Description("انتقاد")]
        Enteghad = 2,

        [Description("پیشنهاد")]
        Pishnahad = 3,

    }
}