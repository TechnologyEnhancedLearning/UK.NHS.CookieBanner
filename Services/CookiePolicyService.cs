using Newtonsoft.Json;

namespace UK.NHS.CookieBanner.Services
{
    public interface ICookiePolicyService
    {
        /// <summary>
        /// The LatestVersionAsync.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<CookiePolicy> LatestVersionAsync(string request);
    }
    public class CookiePolicyService : ICookiePolicyService
    {
        private readonly IGenericApiHttpClient genericApiHttpClient;
        public CookiePolicyService(IGenericApiHttpClient genericApiHttpClient)
        {
            this.genericApiHttpClient = genericApiHttpClient;
        }

        public async Task<CookiePolicy> LatestVersionAsync(string request)
        {
            CookiePolicy viewmodel = new();

            var client = await genericApiHttpClient.GetClientAsync();            
            var response = await client.GetAsync(request).ConfigureAwait(false);

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

            return viewmodel;
        }
    }
}
