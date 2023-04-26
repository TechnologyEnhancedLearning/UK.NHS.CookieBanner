using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Net.Http.Headers;
using UK.NHS.CookieBanner.DataServices;
using UK.NHS.CookieBanner.Extensions;
using static Dapper.SqlMapper;

namespace UK.NHS.CookieBanner.Services
{
    public abstract class BaseHttpClient
    {
        private readonly HttpClient httpClient;              
        private readonly IConfiguration configuration;

        public BaseHttpClient(IConfiguration configuration,HttpClient client)
        {
            this.httpClient = client;
            this.configuration = configuration;
            this.Initialise();
        }

        public async Task<HttpClient> GetClientAsync()
        {           
            return this.httpClient;
        }

        /// <summary>
        /// The initialise.
        /// </summary>
        private void Initialise()
        {
            if (!string.IsNullOrEmpty(this.configuration.GetCookiePolicyAPIUrl()))
            {
                this.httpClient.BaseAddress = new Uri(this.configuration.GetCookiePolicyAPIUrl());
                this.httpClient.DefaultRequestHeaders.Accept.Clear();
                this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                this.httpClient.DefaultRequestHeaders.Add("Client-Identity-Key", this.configuration.GetCookiePolicyAPIClientIdentityKey());
            }            
        }
    }
}
