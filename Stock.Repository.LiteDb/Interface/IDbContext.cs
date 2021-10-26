using LiteDB;

namespace Stock.Repository.LiteDb.Interface
{
    /// <summary>
    /// Database context interface.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Gets the <see cref="LiteDatabase"/>.
        /// </summary>
        LiteDatabase Database { get; }
    }
}