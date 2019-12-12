using System.Collections.Generic;
using Stock.Model.Base;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Base
{
    public class BaseService<TEntity>
        where TEntity : class, IEntity
    {
        protected IRepository<TEntity> Repository { get; set; }

        public BaseService(IRepository<TEntity> repository)
        {
            this.Repository = repository;
        }

        public TEntity Create(TEntity entity)
        {
            return this.Repository.Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.Repository.List();
        }

        public TEntity Get(string id)
        {
            return this.Repository.GetById(id);            
        }

        public void Delete(TEntity entity)
        {
            this.Repository.Delete(entity);
        }

        public TEntity Update(TEntity entity)
        {
            this.Repository.Update(entity);
            return entity;
        }
    }
}
