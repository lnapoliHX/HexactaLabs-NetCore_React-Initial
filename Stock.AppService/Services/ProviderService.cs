using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.AppService.Services
{
    /// <summary>
    /// Provider Service.
    /// </summary>
    public class ProviderService : BaseService<Provider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderService"/> class.
        /// </summary>
        /// <param name="repository">Provider Repository.</param>
        public ProviderService(IRepository<Provider> repository) : base(repository)
        {
        }

        /// <summary>
        /// Creates a provider.
        /// </summary>
        /// <param name="entity">Provider information.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public new Provider Create(Provider entity)
        {
            if (NombreUnico(entity.Name))
            {
                return base.Create(entity);
            }

            throw new Exception("The name is already in use");
        }

        /// <summary>
        /// Search providers.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return Repository.List(filter);
        }
    }
}
