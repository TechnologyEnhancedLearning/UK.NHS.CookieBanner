using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UK.NHS.CookieBanner.Helpers;
using UK.NHS.CookieBanner.ViewModels;

namespace UK.NHS.CookieBanner.Controllers
{
    public class CookieConsentController : Controller
    {
        public IActionResult CookiePolicy()
        {
            //var cookiePolicyContent = configDataService.GetConfigValue(ConfigDataService.CookiePolicyContent);
            //var policyLastUpdatedDate = configDataService.GetConfigValue(ConfigDataService.CookiePolicyUpdatedDate);
            //if (cookiePolicyContent == null)
            //{
            //    logger.LogError("Cookie policy content from Config table is null");
            //    return StatusCode(500);
            //}

            //var model = new CookieConsentViewModel(cookiePolicyContent);
            //model.PolicyUpdatedDate = policyLastUpdatedDate;
            //if (Request != null)
            //{
            //    if (Request.Cookies.HasDLSBannerCookie(CookieBannerConsentCookieName, "true"))
            //        model.UserConsent = "true";
            //    else if (Request.Cookies.HasDLSBannerCookie(CookieBannerConsentCookieName, "false"))
            //        model.UserConsent = "false";
            //}
            var model = new CookieConsentViewModel();
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
                //if (consent == "true")
                //    Response.Cookies?.(CookieBannerConsentCookieName, consent,
                //        clockUtility.UtcNow.AddDays(CookieBannerConsentCookieExpiryDays));

                //else if (consent == "false")
                //    Response.Cookies?.SetCookieBannerCookie(CookieBannerConsentCookieName, consent,
                //        DateTime.UtcNow.AddDays(CookieBannerConsentCookieExpiryDays));

                TempData["userConsentCookieOption"] = consent;

                if (setTempDataConsentViaBannerPost) TempData["consentViaBannerPost"] = consent; // Need this tempdata to display the confirmation banner
            }
        }
    }
}
