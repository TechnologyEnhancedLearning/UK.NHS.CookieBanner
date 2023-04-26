using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using UK.NHS.CookieBanner.DataServices;
using UK.NHS.CookieBanner.Extensions;
using UK.NHS.CookieBanner.Helpers;
using UK.NHS.CookieBanner.Services;
using UK.NHS.CookieBanner.ViewModels;

namespace UK.NHS.CookieBanner.Controllers
{
    public class CookieConsentController : Controller
    {
        private readonly IConfigDataService configDataService;
        private readonly IConfiguration configuration;
        private string CookieBannerConsentCookieName = "";
        private int CookieBannerConsentCookieExpiryDays = 0;
        private readonly IDbConnection connection;
        private readonly ICookiePolicyService cookiePolicyService;
        private readonly IGenericApiHttpClient genericApiHttpClient;

        public CookieConsentController(IConfiguration configuration, ICookiePolicyService cookiePolicyService, IGenericApiHttpClient genericApiHttpClient)
        {
            this.configuration = configuration;
            CookieBannerConsentCookieName = configuration.GetCookieBannerConsentCookieName();
            CookieBannerConsentCookieExpiryDays = configuration.GetCookieBannerConsentExpiryDays();

            string cookieBannerCSName = configuration.GetCookiePolicyConnectionStringName();
            string? cookieBannerConnectionString = configuration.GetSection("ConnectionStrings")
                .GetChildren().FirstOrDefault(config => config.Key == cookieBannerCSName)?.Value;
            connection = new SqlConnection(cookieBannerConnectionString);
            this.configDataService = new ConfigDataService(connection);            
            this.cookiePolicyService = cookiePolicyService;
            this.genericApiHttpClient = genericApiHttpClient;   
        }

        public async Task<IActionResult> CookiePolicy()
        {
            var cookiePolicyDetails = await GetCookiePolicyContentDetails();
            var model = new CookieConsentViewModel(cookiePolicyDetails.Details)
            {
                PolicyUpdatedDate = cookiePolicyDetails.AmendDate
            };

            if (Request != null)
            {
                if (Request.Cookies.HasCookieBannerCookie(CookieBannerConsentCookieName, "true"))
                    model.UserConsent = "true";
                else if (Request.Cookies.HasCookieBannerCookie(CookieBannerConsentCookieName, "false"))
                    model.UserConsent = "false";
            }

            return View(model);
        }

        private async Task<CookiePolicy> GetCookiePolicyContentDetails()
        {
            var cookiePolicy = new CookiePolicy();
            string request = configuration.GetCookiePolicyRequestURI();
            
            if (!string.IsNullOrEmpty(connection.Database))
            {
                string policySQL = configuration.GetPolicySQL();
                cookiePolicy.Details = configDataService.GetData(policySQL);
                cookiePolicy.Details ??= "Cookie policy content is null";
                cookiePolicy.AmendDate = configDataService.GetConfigValue(ConfigDataService.CookiePolicyUpdatedDate);                
            }
            else if(!string.IsNullOrEmpty(request))
            {
                cookiePolicy = await cookiePolicyService.LatestVersionAsync(request);
            }
            else
            {
                cookiePolicy.Details = "Please check the connection string in configuration file";
            }

            return cookiePolicy;
        }

        [HttpPost]
        public IActionResult CookiePolicy(CookieConsentViewModel model)
        {
            string consent = model.UserConsent?.ToString();
            if (!string.IsNullOrEmpty(consent))
                ConfirmCookieConsent(consent);

            return View("CookieConfirmation");
        }
        public IActionResult CookieConsentConfirmation(string consent, string path)
        {
            if (!string.IsNullOrEmpty(consent))
                ConfirmCookieConsent(consent, true);

            string controllerName = string.Empty;
            string actionName = string.Empty;
            string routeValue = string.Empty;

            string[] strTemp = path.Split('/');

            for (int i = 0; i < strTemp.Length; i++)
            {
                if (i == 1) controllerName = strTemp[i] ?? "Home";
                if (i == 2) actionName = strTemp[i] ?? "Index";
                if (i == 3) routeValue = strTemp[i];
            }

            return RedirectToAction(actionName, controllerName);
        }

        public void ConfirmCookieConsent(string consent, bool setTempDataConsentViaBannerPost = false)
        {
            if (Response != null)
            {
                if (consent == "true")
                    Response.Cookies?.SetCookieBannerCookie(CookieBannerConsentCookieName, consent,
                        DateTime.UtcNow.AddDays(CookieBannerConsentCookieExpiryDays));

                else if (consent == "false")
                    Response.Cookies?.SetCookieBannerCookie(CookieBannerConsentCookieName, consent,
                        DateTime.UtcNow.AddDays(CookieBannerConsentCookieExpiryDays));

                TempData["userConsentCookieOption"] = consent;

                if (setTempDataConsentViaBannerPost) TempData["consentViaBannerPost"] = consent; // Need this tempdata to display the confirmation banner
            }
        }
    }
}
