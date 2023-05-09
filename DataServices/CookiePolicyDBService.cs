namespace UK.NHS.CookieBanner.DataServices
{
    using System.Data;
    using Dapper;
    using System;
    using System.Collections.Generic;
    using UK.NHS.CookieBanner.Services;
    using Microsoft.Extensions.Configuration;
    using UK.NHS.CookieBanner.Extensions;

    public class CookiePolicyDBService:ICookiePolicyService
    {
        private readonly IDbConnection? connection;
        private readonly IConfiguration? configuration;

        public CookiePolicyDBService()
        {
        }
        public CookiePolicyDBService(IDbConnection connection, IConfiguration configuration)
        {
            this.connection = connection;
            this.configuration = configuration;
        }
        private readonly string? _connectionString;

     
        public CookiePolicy GetCookiePolicyDetails()
        {
            string policySql = configuration.GetPolicySQL();
            string policyUpdateSql = configuration.GetPolicyUpdateDateSQL();

            return GetCookiePolicyDetails(policySql, policyUpdateSql);
        }

        public string? GetData(string sql)
        {
            return connection.Query<string>(sql).FirstOrDefault();
        }

        public CookiePolicy GetCookiePolicyDetails(string policySql, string policyUpdateDateSql)
        {
            CookiePolicy viewmodel = new()
            {
                Details = GetData(policySql),
                AmendDate = GetData(policyUpdateDateSql)
            };

            return viewmodel;
        }
    }
}
