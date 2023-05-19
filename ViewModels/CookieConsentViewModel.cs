using Microsoft.AspNetCore.Html;
using UK.NHS.CookieBanner.Services;

namespace UK.NHS.CookieBanner.ViewModels
{
    public class CookieConsentViewModel
    {
        private string? policyUpdatedDateAsShort;
        private string? userConsent;

        public HtmlString? CookiePolicyContent { get; }
        public string? ErrorMessage { get; }
        public CookieConsentViewModel()
        {
        }
        public CookieConsentViewModel(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public CookieConsentViewModel(CookiePolicy cookiePolicyContent)
        {
            CookiePolicyContent = new HtmlString(cookiePolicyContent.Details);
            PolicyUpdatedDate = cookiePolicyContent.AmendDate;
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

        private string? optionalLayout;

        public string? Layout { get => optionalLayout; set => optionalLayout = value; }       
    }
}
