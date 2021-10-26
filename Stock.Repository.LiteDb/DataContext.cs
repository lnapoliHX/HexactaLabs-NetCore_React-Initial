using System;
using LiteDB;
using Stock.Repository.LiteDb.Interface;
using Stock.Repository.LiteDb.Configuration;

namespace Stock.Repository.LiteDb
{
    /// <summary>
    /// Defines the database context.
    /// </summary>
    public class DataContext : IDbContext
    {
        private readonly ILiteConfiguration configuration;
        private readonly ConfigurationProvider provider; 

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="configuration">Lite db configuration.</param>
        /// <param name="provider">Provider configuration.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DataContext(ILiteConfiguration configuration, ConfigurationProvider provider)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Gets the lite db.
        /// </summary>
        public LiteDatabase Database => configuration.GetDatabase(provider);
    }
}
