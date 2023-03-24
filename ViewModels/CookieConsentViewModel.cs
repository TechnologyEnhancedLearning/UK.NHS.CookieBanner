﻿using Microsoft.AspNetCore.Html;

namespace UK.NHS.CookieBanner.ViewModels
{
    public class CookieConsentViewModel
    {
        private string policyUpdatedDateAsShort;

        public HtmlString CookiePolicyContent { get; }

        public CookieConsentViewModel()
        {
        }
        public CookieConsentViewModel(string cookiePolicyContent)
        {
            CookiePolicyContent = new HtmlString(cookiePolicyContent);
        }
        public string PolicyUpdatedDate { get; set; }
        public string PolicyUpdatedDateAsShort { get => Convert.ToDateTime(PolicyUpdatedDate).ToString("MMM yyyy"); set => policyUpdatedDateAsShort = value; }
        public string UserConsent { get; set; }
    }
}
