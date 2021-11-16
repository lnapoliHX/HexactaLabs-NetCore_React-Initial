using System;
using System.Collections.Generic;
using Stock.Model.Base;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Base
{
    /// <summary>
    /// Base service.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseService<TEntity>
        where TEntity : class, IEntity
    {
        protected IRepository<TEntity> Repository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">Generic repository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected BaseService(IRepository<TEntity> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="entity">Item information.</param>
        /// <returns>Created item.</returns>
        public TEntity Create(TEntity entity)
        {
            return Repository.Add(entity);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return Repository.List();
        }

        /// <summary>
        /// Gets an item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns></returns>
        public TEntity Get(string id)
        {
            return Repository.GetById(id);            
        }

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        /// <summary>
        /// Updates an item.
        /// </summary>
        /// <param name="entity">Item information.</param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            Repository.Update(entity);
            return entity;
        }

        /// <summary>
        /// Checks if the name is unique or not.
        /// </summary>
        /// <param name="name">Entity name to check.</param>
        /// <returns></returns>
        public bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            
            return Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }
    }
}
