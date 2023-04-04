namespace UK.NHS.CookieBanner.DataServices
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using Dapper;
    using Microsoft.Data.SqlClient;

    public interface IConfigDataService
    {
        string? GetData(string sql);
        string? GetConfigValue(string key);

        bool GetCentreBetaTesting(int centreId);

        string GetConfigValueMissingExceptionMessage(string missingConfigValue);
    }

    public class ConfigDataService : IConfigDataService
    {
        public const string CookiePolicyContent = "CookiePolicyContentHtml";
        public const string CookiePolicyUpdatedDate = "CookiePolicyUpdatedDate";
        
        private readonly IDbConnection connection;
        
        public ConfigDataService(IDbConnection connection)
        {
            this.connection = connection;// new SqlConnection("Data Source=localhost;Initial Catalog=mbdbx101_uar;Integrated Security=True;"); 
        }

        public bool GetCentreBetaTesting(int centreId)
        {
            return connection.Query<bool>(
                @"SELECT BetaTesting FROM Centres WHERE CentreID = @centreId",
                new { centreId }
            ).FirstOrDefault();
        }

        public string? GetData(string sql)
        {
            return connection.Query<string>(sql).FirstOrDefault();
        }

        public string? GetConfigValue(string key)
        {
            return connection.Query<string>(
                @"SELECT ConfigText FROM Config WHERE ConfigName = @key",
                new { key }
            ).FirstOrDefault();
        }

        public string GetConfigValueMissingExceptionMessage(string missingConfigValue)
        {
            return $"Encountered an error while trying to send an email: The value of {missingConfigValue} is null";
        }
    }

    public class ConfigValueMissingException : Exception
    {
        public ConfigValueMissingException(string message)
            : base(message) { }
    }
}
