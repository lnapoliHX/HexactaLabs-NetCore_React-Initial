using LiteDB;
using Stock.Repository.LiteDb.Interface;

namespace Stock.Repository.LiteDb.Configuration
{
    public class LiteConfiguration : ILiteConfiguration
    {
        public LiteDatabase GetDatabase(ConfigurationProvider provider)
        {
            var db = new LiteDatabase(provider.LiteUrl);
            db.Mapper.IncludeNonPublic = true;
            return db;
        }
    }
}