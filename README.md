# UK.NHS.CookieBanner

Add following configuration in _Layout.cshtml
Need to add in Header section nhsuk.css if it doesnt exist
<link rel="stylesheet" href="~/_content/UK.NHS.CookieBanner/lib/cookiebanner/css/nhsuk.css" asp-append-version="true" />

<partial name="_CookieConsentPartial" />

 @RenderSection("NavBreadcrumbs", false)


appsettings.json

 "CookieBannerConsent": {
    "ApplicationName": "Application name",
    "CookieName": "",
    "ExpiryDays": "365"
  },

  // Get cookie policy content from SQL based source
  "CookiePolicy": {
    "ConnectionStringName": "", // Connection name secified in "ConnectionStrings"
    "CookiePolicySQL": ""
  }

   // Get cookie policy content from API's
  "CookiePolicy": {
    "ApiUrl": "",
    "CookiePolicyRequestURI": ""
  }

