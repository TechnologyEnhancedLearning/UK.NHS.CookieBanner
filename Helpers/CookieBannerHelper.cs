using Microsoft.AspNetCore.Http;

namespace UK.NHS.CookieBanner.Helpers
{
    public static class CookieBannerHelper
    {
        public static void SetCookieBannerCookie(
            this IResponseCookies cookies,
            string cookieName,
            string value,
            string domainName,
            DateTime expiry
        )
        {
            cookies.Append(
                cookieName,
                value,
                new CookieOptions
                {
                    Domain= domainName,
                    Expires = expiry
                }
            );
        }

        public static bool HasCookieBannerCookie(this IRequestCookieCollection cookies, string cookieName, string value)
        {
            if (cookies.ContainsKey(cookieName))
            {
                return cookies[cookieName] == value;
            }

            return false;
        }
    }
}
