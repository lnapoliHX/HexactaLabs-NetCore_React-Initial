using LiteDB;
using Stock.Repository.LiteDb.Interface;

namespace Stock.Repository.LiteDb.Configuration
{
    public class LiteConfiguration : ILiteConfiguration
    {
        public LiteDatabase GetDatabase(ConfigurationProvider provider)
        {
            var stringConnection = $"Filename=${provider.LiteUrl};connection=shared";
            var db = new LiteDatabase(stringConnection);
            db.Mapper.IncludeNonPublic = true;
            return db;
        }
    }
}