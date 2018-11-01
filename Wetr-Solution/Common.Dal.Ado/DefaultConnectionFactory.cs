using System.Configuration;
using System.Data.Common;

namespace Common.Dal.Ado
{
    public class DefaultConnectionFactory : IConnectionFactory
    {
        private DbProviderFactory dbProviderFactory;

        public static IConnectionFactory FromConfiguration(string connectionStringConfigName)
        {
            string connectionString = ConfigurationManager
                                 .ConnectionStrings[connectionStringConfigName]
                                 .ConnectionString;

            string providerName = ConfigurationManager
                                   .ConnectionStrings[connectionStringConfigName]
                                   .ProviderName;

            return new DefaultConnectionFactory(providerName, connectionString);
        }

        public DefaultConnectionFactory(string providerName, string connectionString)
        {
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
            this.dbProviderFactory = DbProviderFactories.GetFactory(providerName);
        }

        public string ConnectionString { get; }

        public string ProviderName { get; }

        public DbConnection CreateConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = this.ConnectionString;
            connection.Open();
            return connection;
        }
    }
}