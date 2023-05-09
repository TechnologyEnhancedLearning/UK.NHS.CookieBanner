using UK.NHS.CookieBanner.Services;

namespace UK.NHS.CookieBanner.DataServices
{
    public interface ICookiePolicyService
    {
        CookiePolicy GetCookiePolicyDetails();
    }
}
