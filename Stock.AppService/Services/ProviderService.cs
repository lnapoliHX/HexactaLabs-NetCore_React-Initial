using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.AppService.Services
{
    public class ProviderService : BaseService<Provider>
    {
        public ProviderService(IRepository<Provider> repository)
            : base(repository) 
        { }

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
        /// Checks if the provider name is unique or not.
        /// </summary>
        /// <param name="name">Provider name to check.</param>
        /// <returns></returns>
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
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
