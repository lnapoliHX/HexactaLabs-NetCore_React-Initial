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
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderService"/> class.
        /// </summary>
        /// <param name="repository">Provider repository.</param>
        public ProviderService(IRepository<Provider> repository) : base(repository)
        {
        }

        /// <summary>
        /// Creates a provider.
        /// </summary>
        /// <param name="provider">Provider information.</param>
        /// <returns>A <see cref="Provider"/></returns>
        /// <exception cref="Exception"></exception>
        public new Provider Create(Provider provider)
        {
            if (NameIsUnique(provider.Name))
            {
                return base.Create(provider);
            }

            throw new Exception("The name is already in use");
        }

        private bool NameIsUnique(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            var providersWithSameName = Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper()));

            return providersWithSameName.Count == 0;
        }

        /// <summary>
        /// Search providers.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>A collection of <see cref="Provider"/>.</returns>
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return Repository.List(filter);
        }
    }
}
