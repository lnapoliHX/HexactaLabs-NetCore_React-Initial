using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    /// <summary>
    /// Provider Service
    /// </summary>
    public class ProviderService : BaseService<Provider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderService"/> class.
        /// </summary>
        /// <param name="repository">Store repository.</param>
        public ProviderService(IRepository<Provider> repository)
            : base(repository)
        {
        }

        /// <summary>
        /// Search provider.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return Repository.List(filter);
        }

    }
}
