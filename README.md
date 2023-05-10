# UK.NHS.CookieBanner

### Update _Layout.cshtml file

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

### Update _Layout.cshtml file

Add following configuration in _Layout.cshtml
Need to add in Header section nhsuk.css if it doesnt exist

```bash
<link rel="stylesheet" href="~/_content/UK.NHS.CookieBanner/lib/cookiebanner/css/nhsuk.css" asp-append-version="true" />
```

### Update Program.cs file

Program.cs

 #### SQL based source
```bash
builder.Services.AddScoped<ICookiePolicyService, CookiePolicyDBService>();
```

 #### API's
```bash
builder.Services.AddHttpClient<ICookiePolicyApiHttpClient, CookiePolicyApiHttpClient>();
builder.Services.AddScoped<ICookiePolicyService, CookiePolicyAPIService>();
```


 #### Get cookie policy content from SQL based source
  
  ```bash
  "CookiePolicy": {
    "ConnectionStringName": "", // Connection name specified in "ConnectionStrings"
    "CookiePolicySQL": ""
    "UpdatedDateSQL": ""
  }
  ```
or 
 ####  Get cookie policy content from API's
   ```bash
  "CookiePolicy": {
    "ApiUrl": "",
    "CookiePolicyRequestURI": ""
    "ClientIdentityKey": ""
  }
```
