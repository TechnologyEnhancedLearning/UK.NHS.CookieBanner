using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Net.Http.Headers;
using static Dapper.SqlMapper;

namespace UK.NHS.CookieBanner.Services
{
    public abstract class BaseHttpClient
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;
        //private readonly LearningHubAuthServiceConfig authConfig;
        private readonly ILogger logger;
        /// private readonly ICacheService cacheService;
        public abstract string ApiUrl { get; }

       // protected Settings WebSettings { get; }

        public async Task<HttpClient> GetClientAsync()
        {
            string accessToken = string.Empty;

            // get the current HttpContext to access the tokens
            var currentContext = this.httpContextAccessor.HttpContext;

            //if (this.WebSettings.EnableTempDebugging != null && this.WebSettings.EnableTempDebugging.ToLower() == "true")
            //{
            //    this.logger.LogError("Temp Debugging: LearningHubHttpClient > GetClientAsync User is authenticated. User=" + currentContext.User.Identity.Name);
            //}

            if (currentContext?.User?.Identity?.IsAuthenticated == true)
            {
                // should we renew access and refresh tokens?
                // get expires_at value
                var expires_at = await currentContext.GetTokenAsync("expires_at");

                // compare
                if (string.IsNullOrWhiteSpace(expires_at)
                    || (DateTimeOffset.Parse(expires_at).AddSeconds(-60).ToUniversalTime() < DateTime.UtcNow))
                {
                    accessToken = await this.RenewTokensAsync();
                }
                else
                {
                    // get access token
                    accessToken = await currentContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                }

                //if (!this.httpClient.DefaultRequestHeaders.Contains("x-tz-offset"))
                //{
                //    var tzOffset = await this.cacheService.GetAsync<int?>(currentContext.User.GetTimezoneOffsetCacheKey());
                //    if (tzOffset.HasValue)
                //    {
                //        this.httpClient.DefaultRequestHeaders.Add("x-tz-offset", tzOffset.Value.ToString());
                //    }
                //}
            }

            //this.httpClient.SetBearerToken(accessToken);
            return this.httpClient;
        }

        /// <summary>
        /// The initialise.
        /// </summary>
        private void Initialise()
        {
            this.httpClient.BaseAddress = new Uri(this.ApiUrl);
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // this.httpClient.DefaultRequestHeaders.Add("Client-Identity-Key", this.WebSettings.LHClientIdentityKey);
        }

        /// <summary>
        /// The renew tokens.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<string> RenewTokensAsync()
        {
            //if (this.WebSettings.EnableTempDebugging != null && this.WebSettings.EnableTempDebugging.ToLower() == "true")
            //{
            //    this.logger.LogError("Temp Debugging: LearningHubHttpClient > RenewTokensAsync User is authenticated. ClientID=" + this.authConfig.ClientId);
            //}

            //// get the current HttpContext to access the tokens
            //var currentContext = this.httpContextAccessor.HttpContext;

            //// get the meta data
            //var discoveryClient = await this.httpClient.GetDiscoveryDocumentAsync(this.authConfig.Authority);

            // create a new token client to get the new tokens
            //var tokenClient = new TokenClient(this.httpClient, new TokenClientOptions()
            //{
            //    Address = discoveryClient.TokenEndpoint,
            //    ClientId = this.authConfig.ClientId,
            //    ClientSecret = this.authConfig.ClientSecret,
            //});

            // get the saved refresh token
            //var currentRefreshToken = await currentContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            // refresh the tokens
            // var tokenResult = await tokenClient.RequestRefreshTokenAsync(currentRefreshToken);

            //if (!tokenResult.IsError)
            //{
            //    //if (this.WebSettings.EnableTempDebugging != null && this.WebSettings.EnableTempDebugging.ToLower() == "true")
            //    //{
            //    //    this.logger.LogError("Temp Debugging: LearningHubHttpClient > RenewTokensAsync. tokenResult.IsError=false");
            //    //}

            //    // update the tokens & expiry value
            //    var updatedTokens = new List<AuthenticationToken>();
            //    updatedTokens.Add(new AuthenticationToken
            //    {
            //        Name = OpenIdConnectParameterNames.IdToken,
            //        Value = tokenResult.IdentityToken,
            //    });
            //    updatedTokens.Add(new AuthenticationToken
            //    {
            //        Name = OpenIdConnectParameterNames.AccessToken,
            //        Value = tokenResult.AccessToken,
            //    });
            //    updatedTokens.Add(new AuthenticationToken
            //    {
            //        Name = OpenIdConnectParameterNames.RefreshToken,
            //        Value = tokenResult.RefreshToken,
            //    });

            //    var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResult.ExpiresIn);
            //    updatedTokens.Add(new AuthenticationToken
            //    {
            //        Name = "expires_at",
            //        Value = expiresAt.ToString("o", CultureInfo.InvariantCulture),
            //    });

            //    // get authenticated result, containing the current principle & properties
            //    var currentAuthenticateResult = await currentContext.AuthenticateAsync("Cookies");

            //    // store the updated tokens
            //    currentAuthenticateResult.Properties.StoreTokens(updatedTokens);

            //    // sign in
            //    await currentContext.SignInAsync("Cookies", currentAuthenticateResult.Principal, currentAuthenticateResult.Properties);

            //    // return the new access token
            //    return tokenResult.AccessToken;
            //}
            //else
            //{
            //    if (this.WebSettings.EnableTempDebugging != null && this.WebSettings.EnableTempDebugging.ToLower() == "true")
            //    {
            //        this.logger.LogError("Temp Debugging: LearningHubHttpClient > RenewTokensAsync. tokenResult.IsError=true. ErrorDescription=" + tokenResult.ErrorDescription);
            //    }

            //    throw new Exception("Problem encountered while refeshing tokens", tokenResult.Exception);
            //}
            return null;
        }
    }
}
