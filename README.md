# UK.NHS.CookieBanner

#### Update _Layout.cshtml file

Add following configuration in _Layout.cshtml
Need to add in Header section nhsuk.css if it doesnt exist

```bash
<link rel="stylesheet" href="~/_content/UK.NHS.CookieBanner/lib/cookiebanner/css/nhsuk.css" asp-append-version="true" />
```

```bash
<partial name="_CookieConsentPartial" />
```

```bash
 @RenderSection("NavBreadcrumbs", false)
```
#### Update Configuration file

appsettings.json

```bash
 "CookieBannerConsent": {
    "ApplicationName": "Application name",
    "CookieName": "",
    "ExpiryDays": "365"
  }
```
  // Get cookie policy content from SQL based source
  
  ```bash
  "CookiePolicy": {
    "ConnectionStringName": "", // Connection name secified in "ConnectionStrings"
    "CookiePolicySQL": ""
  }
  ```
or 
   // Get cookie policy content from API's
   ```bash
  "CookiePolicy": {
    "ApiUrl": "",
    "CookiePolicyRequestURI": ""
  }
```
