using LiteDB;
using Stock.Repository.LiteDb.Interface;
using Stock.Repository.LiteDb.Configuration;

namespace Stock.Repository.LiteDb
{
    public class DataContext : IDbContext
    {
        private readonly ILiteConfiguration configuration;
        private readonly ConfigurationProvider provider; 

        public DataContext(ILiteConfiguration configuration, ConfigurationProvider provider)
        {
            this.configuration = configuration;
            this.provider = provider;
        }

        public LiteDatabase Database => this.configuration.GetDatabase(this.provider);
    }
}
