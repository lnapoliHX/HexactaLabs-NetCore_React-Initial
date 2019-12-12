using LiteDB;
using Stock.Repository.LiteDb.Configuration;

namespace Stock.Repository.LiteDb.Interface
{
    public interface ILiteConfiguration
    {
        LiteDatabase GetDatabase(ConfigurationProvider provider);
    }
}