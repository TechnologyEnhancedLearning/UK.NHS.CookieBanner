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
        Task<string> LatestVersionAsync(int id);
    }
    public class CookiePolicyService : ICookiePolicyService
    {
        public async Task<string> LatestVersionAsync(int id)
        {
            string viewmodel = string.Empty;

            //var client = await GetClientAsync();

            //var request = $"TermsAndConditions/LatestVersion/{tenantId}";
            //var response = await client.GetAsync(request).ConfigureAwait(false);

            //if (response.IsSuccessStatusCode)
            //{
            //    var result = response.Content.ReadAsStringAsync().Result;
            //    viewmodel = JsonConvert.DeserializeObject<string>(result);
            //}
            //else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            //            ||
            //         response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            //{
            //    throw new Exception("AccessDenied");
            //}

            return viewmodel;
        }
    }
}
