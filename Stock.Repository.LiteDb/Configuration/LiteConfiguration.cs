using LiteDB;
using Stock.Repository.LiteDb.Interface;

namespace Stock.Repository.LiteDb.Configuration
{
  public class LiteConfiguration : ILiteConfiguration
  {
    private LiteDatabase db { get; set; } = null;
    public LiteDatabase GetDatabase(ConfigurationProvider provider)
    {
      db = db ?? new LiteDatabase(provider.LiteUrl);
      db.Mapper.IncludeNonPublic = true;
      return db;
    }
  }
}