using System;

namespace GolPooch.CrossCutting
{
    public static class GlobalVariables
    {
        public static class CacheSettings
        {
            public static string MenuKey(Guid userId) => userId.ToString().Replace("-", "_");

            public static int BannerCacheTimeout() => 3;
            public static int ProductCacheTimeout() => 3;
            public static int ChestCacheTimeout() => 24;
            public static int RegionCacheTimeout() => 24;
            public static int BankNameCacheTimeout() => 24;
            public static int TiketTypeCacheTimeout() => 24;
            public static int BannerTypeCacheTimeout() => 24;
            public static int PaymentGatewayCacheTimeout() => 24;
            public static int LastWinnerCacheTimeout() => 3;
            public static int MustWinnerCacheTimeout() => 3;

            public static string BannerCacheKey() => "BannerCache";
            public static string ProductCacheKey() => "ProductCache";
            public static string ChestCacheKey() => "ChestCache";
            public static string RegionCacheKey() => "RegionCache";
            public static string BankNameCacheKey() => "BankNameCache";
            public static string TiketTypeCacheKey() => "TiketTypeCache";
            public static string BannerTypeCacheKey() => "BannerTypeCache";
            public static string PaymentGatewayCacheKey() => "PaymentGatewayCache";
            public static string LastWinnerCacheKey() => "LastWinnerCache";
            public static string MustWinnerCacheKey() => "MustWinnerCache";
        }


        public static class SmsGatewaySettings
        {
            public static string ApiKey = "41636B3243496472392F72356834316954764D7231734564714F6446464B512F2B455230465379784455383D";

            public static string Sender = "1000596446";
        }


        public static class PushNotificationSetting
        {
            public static string SenderID = "873210377614";

            public static string AppID = "1:873210377614:web:284744c8f3e250cf80e0df";

            public static string FcmAddress = "https://fcm.googleapis.com/fcm/send";

            public static string WebAPIKey = "AIzaSyDAx5O8Te76lUHFEEfTx7URneZBEu-Stuc";

            public static string ServerKey = "AAAAy09k9Y4:APA91bHHC7Nj-QPL1NZAM7xzVLVEBWoggsT_BSkhwiz8sk5aMc_57DqsPGxAD8h-3yRR5NmLAckz9iM4Kc4ShY2mPeR0Q5yCXgWeDO16_yoAQaa8H2O85zvVjqq6KVnkGL5VB6XJwaVu";
        }
    }
}