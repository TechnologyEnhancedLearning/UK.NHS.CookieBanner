using Microsoft.Extensions.Options;
using UK.NHS.CookieBanner.DataServices;
using static Dapper.SqlMapper;

namespace UK.NHS.CookieBanner.Services
{
    public interface IGenericApiHttpClient
    {
        HttpClient GetClient();
        Task<HttpClient> GetClientAsync();
    }
    public class GenericApiHttpClient : BaseHttpClient, IGenericApiHttpClient
    {
        public GenericApiHttpClient(IConfiguration configuration, HttpClient client) : base(configuration,client)
        {
        }       
    }
}
