using Microsoft.AspNetCore.Html;

namespace UK.NHS.CookieBanner.ViewModels
{
    public class CookieConsentViewModel
    {
        private string? policyUpdatedDateAsShort;
        private string? userConsent;

        public HtmlString? CookiePolicyContent { get; }

        public CookieConsentViewModel()
        {
        }
        public CookieConsentViewModel(string cookiePolicyContent)
        {
            CookiePolicyContent = new HtmlString(cookiePolicyContent);
        }
        public string? PolicyUpdatedDate { get; set; }
        public string? PolicyUpdatedDateAsShort
        {
            get
            {
                if (!string.IsNullOrEmpty(PolicyUpdatedDate))
                    return Convert.ToDateTime(PolicyUpdatedDate).ToString("MMM yyyy");
                return null;
            }
            set => policyUpdatedDateAsShort = value;
        }
        public string? UserConsent { get => userConsent; set => userConsent = value; }
    }
}
