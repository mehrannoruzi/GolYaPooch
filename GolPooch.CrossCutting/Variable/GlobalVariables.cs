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
            public static int PaymentGatewayCacheTimeout() => 24;

            public static string BannerCacheKey() => "BannerCache";
            public static string ProductCacheKey() => "ProductCache";
            public static string ChestCacheKey() => "ChestCache";
            public static string RegionCacheKey() => "RegionCache";
            public static string BankNameCacheKey() => "BankNameCache";
            public static string TiketTypeCacheKey() => "TiketTypeCache";
            public static string PaymentGatewayCacheKey() => "PaymentGatewayCache";
        }


        public static class SmsGatewaySettings
        {
            public static string ApiKey = "41636B3243496472392F72356834316954764D7231734564714F6446464B512F2B455230465379784455383D";

            public static string Sender = "1000596446";
        }
    }
}