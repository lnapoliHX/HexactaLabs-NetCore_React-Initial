using Microsoft.Extensions.Configuration;

namespace Stock.Repository.LiteDb.Configuration
{
    public class ConfigurationProvider
    {
        IConfiguration config;
        public ConfigurationProvider(IConfiguration config)
        {
            this.config = config;
        }

        public string LiteDbName
        {
            get
            {
                return this.config.GetSection("ConnectionString").GetSection("LiteSection").GetSection("LitePath").Value;
            }
        }

        public string LiteUrl
        {
            get
            {
               return this.config.GetSection("ConnectionString").GetSection("LiteSection").GetSection("LitePath").Value;
            }
        }
    }
}