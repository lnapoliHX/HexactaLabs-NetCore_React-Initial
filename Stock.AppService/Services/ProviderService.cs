using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.AppService.Services
{
    /// <summary>
    /// Provider service.
    /// </summary>
    public class ProviderService : BaseService<Provider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderService"/> class.
        /// <param name="repository"> Provider repository. </param>
        public ProviderService(IRepository<Provider> repository)
            : base(repository)
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

            throw new Exception("the name is already in use.");
        }

        /// <summary>
        /// Gets all providers.
        /// </summary>
        /// <returns></returns>
        public new IEnumerable<Provider> GetAll()
        {
            return Repository.List();
        }

        /// <summary>
        /// Gets an item by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns></returns>
        public new Provider Get(string id)
        {
            return Repository.GetById(id);
        }

        /// <summary>
        /// Deletes a provider.
        /// </summary>
        /// <param name="entity">Provider information.</param>
        /// <returns></returns>
        public new void Delete(Provider entity)
        {
            base.Delete(entity);
        }

        /// <summary>
        /// Updates a provider.
        /// </summary>
        /// <param name="entity">Provider information</param>
        public new Provider Update(Provider entity)
        {
            return base.Update(entity);
        }

        /// <summary>
        /// Checks if the provider name is unique or not.
        /// </summary>
        /// <param name="name">Provider name to check</param>
        /// <returns></returns>
        public bool NombreUnico(string name)
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
