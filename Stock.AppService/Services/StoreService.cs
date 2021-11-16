using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    /// <summary>
    /// Store service.
    /// </summary>
    public class StoreService : BaseService<Store>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService"/> class.
        /// </summary>
        /// <param name="repository">Store repository.</param>
        public StoreService(IRepository<Store> repository)
            : base(repository)
        {     
        }

        /// <summary>
        /// Creates a store.
        /// </summary>
        /// <param name="entity">Store information.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public new Store Create(Store entity)
        {
            if (NombreUnico(entity.Name))
            {
                return base.Create(entity);
            }

            throw new Exception("The name is already in use");
        }

        /// <summary>
        /// Search stores.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<Store> Search(Expression<Func<Store, bool>> filter)
        {
            return Repository.List(filter);
        }
    }
}