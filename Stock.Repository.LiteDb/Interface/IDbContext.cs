using LiteDB;

namespace Stock.Repository.LiteDb.Interface
{
   public interface IDbContext
    {
        LiteDatabase Database { get; }
    }
}