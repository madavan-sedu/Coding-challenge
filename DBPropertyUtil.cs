using System.IO;
using Microsoft.Extensions.Configuration;

namespace InsuranceManagement.Utils
{
    public static class DBPropertyUtil
    {
        private static IConfigurationRoot? _configuration;

        static DBPropertyUtil()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetConnectionString(string name = "DefaultConnection")
        {
            return _configuration.GetConnectionString(name);
        }
    }
}