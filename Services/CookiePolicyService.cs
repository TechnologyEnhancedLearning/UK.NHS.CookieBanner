//using Dapper;
//using Newtonsoft.Json;
//using System.Data;

//namespace UK.NHS.CookieBanner.Services
//{
//    public interface ICookiePolicyService
//    {
//        /// <summary>
//        /// The LatestVersionAsync.
//        /// </summary>
//        /// <param name="id">Id.</param>
//        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
//        Task<CookiePolicy> LatestVersionAsync(string request);
//    }
//    public class CookiePolicyService : ICookiePolicyService
//    {
//        private readonly IDbConnection? connection;
//        private readonly IGenericApiHttpClient? genericApiHttpClient;

//        public CookiePolicyService()
//        {
//        }
//        public CookiePolicyService(IDbConnection connection)
//        {
//            this.connection = connection;
//        }
//        public CookiePolicyService(IGenericApiHttpClient genericApiHttpClient)
//        {
//            this.genericApiHttpClient = genericApiHttpClient;
//        }
//        public string? GetData(string sql)
//        {
//            return connection.Query<string>(sql).FirstOrDefault();
//        }

//        public CookiePolicy GetCookiePolicyDetails(string policySql, string policyUpdateDateSql)
//        {
//            CookiePolicy viewmodel = new()
//            {
//                Details = GetData(policySql),
//                AmendDate = GetData(policyUpdateDateSql)
//            };

//            return viewmodel;
//        }
//        public async Task<CookiePolicy> LatestVersionAsync(string request)
//        {
//            CookiePolicy viewmodel = new();

//            if (genericApiHttpClient != null)
//            {
//                var client = await genericApiHttpClient.GetClientAsync();
//                var response = await client.GetAsync(request).ConfigureAwait(false);

//                if (response.IsSuccessStatusCode)
//                {
//                    var result = response.Content.ReadAsStringAsync().Result;
//                    viewmodel = JsonConvert.DeserializeObject<CookiePolicy>(result);
//                }
//                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized
//                            ||
//                         response.StatusCode == System.Net.HttpStatusCode.Forbidden)
//                {
//                    throw new Exception("AccessDenied");
//                }
//            }

//            return viewmodel;
//        }
//    }
//}
