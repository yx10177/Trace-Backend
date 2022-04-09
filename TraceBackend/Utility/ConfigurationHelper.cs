using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace Utility
{
    public class ConfigurationHelper
    {
        public static IConfigurationSection GetSection(string sectionName)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string appsettingFileName = string.IsNullOrEmpty(environmentName) ? "appsettings.json" : $"appsettings.{environmentName}.json";
            var path = Path.Combine(Directory.GetCurrentDirectory(), appsettingFileName);
            var configurationBuilder = new ConfigurationBuilder()
                                           .AddJsonFile(path,false)
                                           .Build();
            return configurationBuilder.GetSection(sectionName);
        }
    }
}
