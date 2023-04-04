using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using UK.NHS.CookieBanner.DataServices;
using UK.NHS.CookieBanner.Extensions;
using UK.NHS.CookieBanner.Helpers;
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

        public CookieConsentController(IConfiguration configuration)
        {
            this.configuration = configuration;
            CookieBannerConsentCookieName = configuration.GetCookieBannerConsentCookieName();
            CookieBannerConsentCookieExpiryDays = configuration.GetCookieBannerConsentExpiryDays();

            string cookieBannerCSName = configuration.GetCookiePolicyConnectionStringName();
            string? cookieBannerConnectionString = configuration.GetSection("ConnectionStrings")
                .GetChildren().FirstOrDefault(config => config.Key == cookieBannerCSName)?.Value;
            connection = new SqlConnection(cookieBannerConnectionString);
            configDataService = new ConfigDataService(connection);
            this.configDataService = configDataService;
        }

        public IActionResult CookiePolicy()
        {
            string cookiePolicyContent = string.Empty;
            string policyLastUpdatedDate = string.Empty;

            if (connection != null)
            {
                string policySQL = configuration.GetPolicySQL();
                cookiePolicyContent = configDataService.GetData(policySQL);
                policyLastUpdatedDate = configDataService.GetConfigValue(ConfigDataService.CookiePolicyUpdatedDate);
                cookiePolicyContent ??= "Cookie policy content is null";
            }
            else
            {
                cookiePolicyContent = "Please check the connection string in configuration file";
            }

            var model = new CookieConsentViewModel(cookiePolicyContent);
            model.PolicyUpdatedDate = policyLastUpdatedDate;
            if (Request != null)
            {
                if (Request.Cookies.HasCookieBannerCookie(CookieBannerConsentCookieName, "true"))
                    model.UserConsent = "true";
                else if (Request.Cookies.HasCookieBannerCookie(CookieBannerConsentCookieName, "false"))
                    model.UserConsent = "false";
            }

            return View(model);
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
