using Newtonsoft.Json;
using System.Data;
using UK.NHS.CookieBanner.Extensions;
using UK.NHS.CookieBanner.Services;
using static Dapper.SqlMapper;

namespace UK.NHS.CookieBanner.DataServices
{
    public class CookiePolicyAPIService : ICookiePolicyService
    {
        private readonly IConfiguration configuration;
        private readonly ICookiePolicyApiHttpClient genericApiHttpClient;

        public CookiePolicyAPIService(ICookiePolicyApiHttpClient genericApiHttpClient, IConfiguration configuration)
        {
            this.genericApiHttpClient = genericApiHttpClient;
            this.configuration = configuration;
        }

        public CookiePolicy GetCookiePolicyDetails()
        {
            string request = configuration.GetCookiePolicyRequestURI();
            CookiePolicy viewmodel = new();

            if (genericApiHttpClient != null)
            {
                var client = genericApiHttpClient.GetClient();
                HttpResponseMessage response = client.GetAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    viewmodel = JsonConvert.DeserializeObject<CookiePolicy>(result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized
                            ||
                         response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new Exception("AccessDenied");
                }
            }

            return viewmodel;
        }
    }
}
