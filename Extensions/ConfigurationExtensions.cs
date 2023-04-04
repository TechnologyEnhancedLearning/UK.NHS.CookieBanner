namespace UK.NHS.CookieBanner.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string CookieBannerConsentCookieName = "CookieBannerConsent:CookieName";
        private const string CookieBannerConsentExpiryDays = "CookieBannerConsent:ExpiryDays";
        private const string CookiePolicyConnectionStringName = "CookiePolicy:ConnectionStringName";
        private const string CookiePolicySQL = "CookiePolicy:CookiePolicySQL";

        public static string GetCookiePolicyConnectionStringName(this IConfiguration config)
        {
            return config[CookiePolicyConnectionStringName];
        }
        public static string GetCookieBannerConsentCookieName(this IConfiguration config)
        {
            return config[CookieBannerConsentCookieName];
        }

        public static int GetCookieBannerConsentExpiryDays(this IConfiguration config)
        {
            int.TryParse(config[CookieBannerConsentExpiryDays], out int expiryDays);
            return expiryDays;
        }
        public static string GetPolicySQL(this IConfiguration config)
        {
            return config[CookiePolicySQL];
        }
    }
}

