using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace InsuranceManagement.Utils
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string configFilePath)
        {
            // Load the configuration from the appsettings.json file
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set base path to the current directory
                .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
                .Build();

            // Get the connection string from the configuration
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Create and return the SqlConnection object
            return new SqlConnection(connectionString);
        }
    }
}