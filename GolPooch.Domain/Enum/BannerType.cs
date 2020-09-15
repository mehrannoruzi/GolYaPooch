using System.ComponentModel;

namespace GolPooch.Domain.Enum
{
    public enum BannerType : byte
    {
        [Description("تمام صفحه")]
        Ctr = 1,

        [Description("مودال")]
        Modal = 2,

        [Description("بنر تصویری")]
        ImgBanner = 3,

        [Description("بنر ویدئویی")]
        VideoBanner = 4,

        [Description("تکمیل پروفایل")]
        CompleteProfile = 5
    }
}