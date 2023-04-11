namespace UK.NHS.CookieBanner.Services
{
    public interface IGenericApiHttpClient
    {
        Task<HttpClient> GetClientAsync();
    }
    public class GenericApiHttpClient : BaseHttpClient, IGenericApiHttpClient
    {
        public override string ApiUrl => "B";//  this.WebSettings.UserApiUrl;
    }
}
