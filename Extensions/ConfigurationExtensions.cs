namespace UK.NHS.CookieBanner.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string CookieBannerConsentCookieName = "CookieBannerConsent:CookieName";
        private const string CookieBannerConsentExpiryDays = "CookieBannerConsent:ExpiryDays";
        private const string CookieBannerDeleteCookieNames = "CookieBannerConsent:AnalyticsCookieNamesPrefix";
        private const string CookieBannerAnalyticsCookiesDomainName = "CookieBannerConsent:AnalyticsCookiesDomainName";

        private const string CookiePolicyConnectionStringName = "CookiePolicy:ConnectionStringName";
        private const string CookiePolicySQL = "CookiePolicy:CookiePolicySQL";
        private const string CookiePolicyUpdatedDateSQL = "CookiePolicy:UpdatedDateSQL";

        private const string CookiePolicyAPIUrl = "CookiePolicy:ApiUrl";
        private const string CookiePolicyAPIClientIdentityKey = "CookiePolicy:ClientIdentityKey";
        private const string CookiePolicyRequestURI = "CookiePolicy:CookiePolicyRequestURI";        

        public static string GetCookiePolicyConnectionStringName(this IConfiguration config)
        {
            return config[CookiePolicyConnectionStringName];
        }
        public static string GetCookieBannerConsentCookieName (this IConfiguration config)
        {
            return config[CookieBannerConsentCookieName];
        }
        public static string[] GetCookieBannerDeleteCookieNames(this IConfiguration config)
        {
            var deleteCookieNames = config.GetSection(CookieBannerDeleteCookieNames);
            return deleteCookieNames.GetChildren().Select(x => x.Value).ToArray();
        }
        public static string GetAnalyticsCookiesDomainName(this IConfiguration config)
        {
            return config[CookieBannerAnalyticsCookiesDomainName];
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
        public static string GetPolicyUpdateDateSQL(this IConfiguration config)
        {
            return config[CookiePolicyUpdatedDateSQL];
        }
        public static string GetCookiePolicyAPIUrl(this IConfiguration config)
        {
            return config[CookiePolicyAPIUrl] ?? string.Empty;
        }
        public static string GetCookiePolicyAPIClientIdentityKey(this IConfiguration config)
        {
            return config[CookiePolicyAPIClientIdentityKey] ?? string.Empty;
        }

        public static string GetCookiePolicyRequestURI(this IConfiguration config)
        {
            return config[CookiePolicyRequestURI] ?? string.Empty;
        }
    }
}

