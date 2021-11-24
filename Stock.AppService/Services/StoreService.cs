using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.AppService.Services
{
    /// <summary>
    /// Store service.
    /// </summary>
    public class StoreService : UniquelyNamedEntityService<Store>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService"/> class.
        /// </summary>
        /// <param name="repository">Store repository.</param>
        public StoreService(IRepository<Store> repository) : base(repository)
        {
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