using Microsoft.Extensions.Configuration;

namespace Stock.Repository.LiteDb.Configuration
{
    public class ConfigurationProvider
    {
        readonly IConfiguration config;

        public ConfigurationProvider(IConfiguration config)
        {
            this.config = config;
        }

        public string LiteDbName => config.GetSection("ConnectionString").GetSection("LiteSection").GetSection("LitePath").Value;

        public string LiteUrl => config.GetSection("ConnectionString").GetSection("LiteSection").GetSection("LitePath").Value;
    }
}