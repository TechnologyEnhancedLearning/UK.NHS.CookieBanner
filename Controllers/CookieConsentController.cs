﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using UK.NHS.CookieBanner.DataServices;
using UK.NHS.CookieBanner.Extensions;
using UK.NHS.CookieBanner.Helpers;
using UK.NHS.CookieBanner.Services;
using UK.NHS.CookieBanner.ViewModels;

namespace UK.NHS.CookieBanner.Controllers
{
    public class CookieConsentController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ICookiePolicyService? cookiePolicyService;
        private string CookieBannerConsentCookieName = "";
        private int CookieBannerConsentCookieExpiryDays = 0;
        private string[] CookieBannerAnalyticsCookieNamesPrefix;
        private string AnalyticsCookiesDomainName = "";

        public CookieConsentController(IConfiguration configuration, ICookiePolicyService? cookiePolicyService = null)
        {
            this.configuration = configuration;
            this.cookiePolicyService = cookiePolicyService;
            CookieBannerConsentCookieName = configuration.GetCookieBannerConsentCookieName();
            CookieBannerConsentCookieExpiryDays = configuration.GetCookieBannerConsentExpiryDays();
            CookieBannerAnalyticsCookieNamesPrefix = configuration.GetCookieBannerDeleteCookieNames();
            AnalyticsCookiesDomainName = configuration.GetAnalyticsCookiesDomainName();

            string cookieBannerCSName = configuration.GetCookiePolicyConnectionStringName();
            if (!string.IsNullOrEmpty(cookieBannerCSName))
            {
                string? cookieBannerConnectionString = configuration.GetSection("ConnectionStrings")
                    .GetChildren().FirstOrDefault(config => config.Key == cookieBannerCSName)?.Value;

                this.cookiePolicyService = new CookiePolicyDBService(new SqlConnection(cookieBannerConnectionString), configuration);
            }
        }

        public IActionResult CookiePolicy()
        {
            var model = new CookieConsentViewModel();
            try
            {
                if (cookiePolicyService != null)
                {
                    var cookiePolicyDetails = cookiePolicyService.GetCookiePolicyDetails();

                    model = new CookieConsentViewModel(cookiePolicyDetails);

                    if (Request != null)
                    {
                        string domainName = HttpContext.Request.Host.Host;
                        if (Request.Cookies.HasCookieBannerCookie(CookieBannerConsentCookieName, "true"))
                            model.UserConsent = "true";
                        else if (Request.Cookies.HasCookieBannerCookie(CookieBannerConsentCookieName, "false"))
                            model.UserConsent = "false";
                    }
                }
                else
                {
                    model = new CookieConsentViewModel("Cookie Policy Implementation Not Found");
                }
            }
            catch (Exception ex)
            {
                model = new CookieConsentViewModel(ex?.Message?.ToString() + "\n" + ex?.InnerException?.Message.ToString());
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult CookiePolicy(CookieConsentViewModel model)
        {
            string? consent = model.UserConsent?.ToString();
            if (!string.IsNullOrEmpty(consent))
                ConfirmCookieConsent(consent);
            TempData["Layout"] = model.Layout;
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

        public IActionResult ConfirmCookieConsent(string consent, bool setTempDataConsentViaBannerPost = false)
        {
            if (Response != null)
            {
                string domainName = HttpContext.Request.Host.Host;

                if (consent == "true")
                    Response.Cookies?.SetCookieBannerCookie(CookieBannerConsentCookieName, consent, domainName,
                        DateTime.UtcNow.AddDays(CookieBannerConsentCookieExpiryDays));

                else if (consent == "false")
                {
                    Response.Cookies?.SetCookieBannerCookie(CookieBannerConsentCookieName, consent, domainName,
                        DateTime.UtcNow.AddDays(CookieBannerConsentCookieExpiryDays));
                    RemoveCookiesDefinedInConfig();
                }

                TempData["ObtainUserConsentToTrackPriorToPageLoad"] = consent;

                if (setTempDataConsentViaBannerPost) TempData["consentViaBannerPost"] = consent; // Need this tempdata to display the confirmation banner
            }
            return Json("OK");
        }

        private void RemoveCookiesDefinedInConfig()
        {
            string domainName = GetDomainName();

            foreach (var removeCookieName in CookieBannerAnalyticsCookieNamesPrefix)
            {
                // List and delete all cookies from config
                var cookies = Request.Cookies.Where(c => c.Key.StartsWith(removeCookieName)).ToList();
                foreach (var cookie in cookies)
                {
                    Response.Cookies.Delete(cookie.Key, new CookieOptions { Domain = domainName });
                }
            }
        }

        private string GetDomainName()
        {
            // Get the domain name from the request URL without protocol or "www" prefix
            string domainName = HttpContext.Request.Host.Host;
            if (!string.IsNullOrEmpty(AnalyticsCookiesDomainName))
                domainName = AnalyticsCookiesDomainName;
            else
            {
                int dotIndex = domainName.IndexOf(".");
                domainName = dotIndex >= 0 ? domainName.Substring(dotIndex) : domainName;
            }

            return domainName;
        }
    }
}
