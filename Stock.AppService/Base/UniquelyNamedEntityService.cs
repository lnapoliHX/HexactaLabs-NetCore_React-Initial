using System;
using Stock.Model.Base;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Base
{
    /// <summary>
    /// Service for entities whose name must be unique.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class UniquelyNamedEntityService<TEntity> : BaseService<TEntity> where TEntity : class, IUniquelyNamedEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniquelyNamedEntityService{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">Generic repository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected UniquelyNamedEntityService(IRepository<TEntity> repository) : base(repository)
        {
        }

        /// <summary>
        /// Creates a new uniquely named item.
        /// </summary>
        /// <param name="entity">Item information.</param>
        /// <returns>The newly created <see cref="TEntity"/>.</returns>
        /// <exception cref="Exception"></exception>
        public new TEntity Create(TEntity entity)
        {
            if (IsUnique(entity.Name))
            {
                return base.Create(entity);
            }

            throw new Exception($"The entity name is not unique: {entity.Name}.");
        }

        protected bool IsUnique(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var entitiesWithSameName = Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper()));

            return entitiesWithSameName.Count == 0;
        }
    }
}
