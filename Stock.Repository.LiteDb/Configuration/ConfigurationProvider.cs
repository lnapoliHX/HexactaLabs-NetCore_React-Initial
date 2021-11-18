using System;
using Microsoft.Extensions.Configuration;

namespace Stock.Repository.LiteDb.Configuration
{
    /// <summary>
    /// Configuration provider.
    /// </summary>
    public class ConfigurationProvider
    {
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationProvider"/> class.
        /// </summary>
        /// <param name="config">Aspnet configuration object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConfigurationProvider(IConfiguration config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public string LiteDbName => config.GetSection("ConnectionString").GetSection("LiteSection").GetSection("LitePath").Value;

        public string LiteUrl => config.GetSection("ConnectionString").GetSection("LiteSection").GetSection("LitePath").Value;
    }
}