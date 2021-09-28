using System;
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
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public TEntity Create(TEntity entity)
        {
            return Repository.Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Repository.List();
        }

        public TEntity Get(string id)
        {
            return Repository.GetById(id);            
        }

        public void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public TEntity Update(TEntity entity)
        {
            Repository.Update(entity);
            return entity;
        }
    }
}
