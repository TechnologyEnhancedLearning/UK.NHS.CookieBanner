# UK.NHS.CookieBanner

# Cookie Banner NuGet Package for .NET Core
This NuGet package provides an easy-to-use cookie banner for .NET Core web applications. The cookie banner allows you to comply with the EU Law and the GDPR by informing users about the use of cookies on your website and giving them the option to accept or decline cookies.

 It is designed to work with API calls, SQL server, and custom classes.

# Installation
You can install the package via NuGet Package Manager or by running the following command in the Package Manager Console:

```bash
Install-Package UK.NHS.CookieBanner
````

## Usage

### Update _Layout.cshtml file

In your _Layout.cshtml file (or any other view where you want to display the cookie banner), add the following code
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

```bash
<link rel="stylesheet" href="~/_content/UK.NHS.CookieBanner/lib/cookiebanner/css/nhsuk.css" asp-append-version="true" />
```

### Update Program.cs file
Program.cs

There is a possibility of utilising various sources to populate the cookie policy content through different means, including calling APIs, using a SQL server, or implementing a custom approach.

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

## Contributions
Contributions are welcome! Please feel free to submit issues and pull requests.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
 
