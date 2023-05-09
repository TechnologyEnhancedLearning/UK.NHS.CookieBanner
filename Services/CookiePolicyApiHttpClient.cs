using Microsoft.Extensions.Options;
using UK.NHS.CookieBanner.DataServices;
using static Dapper.SqlMapper;

namespace UK.NHS.CookieBanner.Services
{
    public interface ICookiePolicyApiHttpClient
    {
        HttpClient GetClient();
        Task<HttpClient> GetClientAsync();
    }
    public class CookiePolicyApiHttpClient : BaseHttpClient, ICookiePolicyApiHttpClient
    {
        public CookiePolicyApiHttpClient(IConfiguration configuration, HttpClient client) : base(configuration,client)
        {
        }       
    }
}
