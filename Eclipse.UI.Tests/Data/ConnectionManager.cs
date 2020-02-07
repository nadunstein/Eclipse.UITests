using System;
using System.Configuration;
using System.Data.Common;

namespace Eclipse.UI.Tests.Data
{
    public static class ConnectionManager
    {
        private const string ConnectionStringConfigKey = "UserDBConnectionString";

        public static T ExecuteReturn<T>(Func<DbConnection, T> command)
        {
            var connection = GetConnection();
            try
            {
                connection.Open();
                T result = command(connection);
                return result;
            }
            finally
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
            }
        }

        public static void Execute(Action<DbConnection> command)
        {
            var connection = GetConnection();
            try
            {
                connection.Open();
                command(connection);
            }
            finally
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
            }
        }

        private static DbConnection GetConnection()
        {
            return CreateConnectionUsingConfigKey(ConnectionStringConfigKey);
        }

        private static DbConnection CreateConnectionUsingConfigKey(string connectionStringKey)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            var providerName = ConfigurationManager.ConnectionStrings[connectionStringKey].ProviderName;

            var provider = DbProviderFactories.GetFactory(providerName);
            var connection = provider.CreateConnection();
            if (connection != null)
                connection.ConnectionString = connectionString;
            return connection;
        }
    }
}