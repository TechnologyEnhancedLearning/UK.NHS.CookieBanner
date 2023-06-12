# UK.NHS.CookieBanner

# Cookie Banner NuGet Package for .NET Core
This NuGet package provides an easy-to-use cookie banner and cookie policy page for .NET Core web applications. The cookie banner allows you to comply with the EU Law and the GDPR by informing users about the use of cookies on your website and giving them the option to accept or decline cookies.

 It is designed to work with API calls, SQL server, and custom classes.

# Installation
You can install the package via NuGet Package Manager or by running the following command in the Package Manager Console:

```bash
Install-Package UK.NHS.CookieBanner
````

# Usage

# Implementing Cookie Banner
## How to Implement Cookie Banner
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

 #### Cookie banner settings
  
  ```bash
   "CookieBannerConsent": {
    "CookieBannerContent": "",
    "ApplicationName": "",
    "CookieName": "",
    "ExpiryDays": "",
    "AnalyticsCookieNamesPrefix": [ "", "" ],
    "AnalyticsCookiesDomainName": ""
  }
  ```

# Implementing built-in Cookie Policy page
There is a possibility of utilising various sources to populate the cookie policy content through different means, including calling APIs, using a SQL server, or implementing a custom approach.

## How to Implement Cookie Policy Page
### SQL based source
#### Add following line in Program.cs
```bash
builder.Services.AddScoped<ICookiePolicyService, CookiePolicyDBService>();
```
#### SQL based source
To configure the "CookiePolicy" section in your appsettings file, you need to provide the appropriate values for each property. Here's an explanation of each property:

- "ConnectionStringName": Specify the name of the connection string from your "ConnectionStrings" section that will be used to connect to your database.
- "CookiePolicySQL": Specify the SQL query that will retrieve the cookie policy information from your database.
 - "UpdatedDateSQL": Specify the SQL query that will retrieve the last updated date of the cookie policy from your database.
 ```bash
  "CookiePolicy": {
    "ConnectionStringName": "", // Connection name specified in "ConnectionStrings"
    "CookiePolicySQL": ""
    "UpdatedDateSQL": ""
  }
  ```

 ### API's
 #### Add following lines in Program.cs
```bash
builder.Services.AddHttpClient<ICookiePolicyApiHttpClient, CookiePolicyApiHttpClient>();
builder.Services.AddScoped<ICookiePolicyService, CookiePolicyAPIService>();

```
 ####  Get cookie policy content from API's
To configure the "CookiePolicy" section in your appsettings file, you need to provide the appropriate values for each property. Here's an explanation of each property:


- "ApiUrl": Specify the URL of the API that handles the cookie policy requests.
- "CookiePolicyRequestURI": Specify the URI or endpoint on the API that is responsible for managing cookie policy-related requests.
- "ClientIdentityKey": Specify the key or identifier used to authenticate the client making the cookie policy requests.

```bash
  "CookiePolicy": {   
    "ApiUrl": "",
    "CookiePolicyRequestURI": ""
    "ClientIdentityKey": ""
  }
```
 
# How to use this package in Existing Cookie Policy Page

If you wish to use your own policy page, please update the "ControllerName" and "ActionName" in the appsettings file. When using your own cookie policy, ensure that you utilise the existing cookie consent confirmation partial view. This view is responsible for updating analytical cookies and the user consent cookie.

```bash
@await Html.PartialAsync("_CookiePolicyConfirmation", new CookieConsentViewModel {UserConsent = cookieBannerCookieValue, Layout = Layout})
```
```bash
"CookiePolicy": {
   "ControllerName": "", 
   "ActionName": ""
 }
```

- "ControllerName": Specify the name of the controller that will handle the Cookie Policy page logic.
- "ActionName": Specify the name of the action method within the specified controller that will render the Cookie Policy view.


## Contributions
Contributions are welcome! Please feel free to submit issues and pull requests.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
 
