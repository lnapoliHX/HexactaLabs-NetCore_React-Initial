using System;
using Stock.Model.Base;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Base
{
    public abstract class UniquelyNamedEntityService<TEntity> : BaseService<TEntity> where TEntity : class, IUniquelyNamedEntity
    {
        protected UniquelyNamedEntityService(IRepository<TEntity> repository) : base(repository)
        {
        }
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
