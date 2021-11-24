using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stock.AppService.Services
{
    public class ProviderService : BaseService<Provider>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Provider Repository</param>
        public ProviderService(IRepository<Provider> repository)
            : base(repository)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a new provider
        /// </summary>
        /// <param name="NewProvider"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public new Provider Create(Provider NewProvider)
        {
            if (NombreUnico(NewProvider.Name))
            {
                return base.Create(NewProvider);
            }

            throw new Exception("El nombre ya está en uso.");
        }

        /// <summary>
        /// Edit information of a provider.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="NewDataProvider"></param>
        /// <returns></returns>
        public void EditProvider(Provider NewDataProvider)
        {
            Repository.Update(NewDataProvider);
        }

        /// <summary>
        /// Delete a provider.
        /// </summary>
        /// <param name="Id"></param>
        public void EraseProvider(Provider entity)
        {
            Repository.Delete(entity);
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
        #endregion
        #region Aux
        /// <summary>
        /// Checks if the store name is unique or not.
        /// </summary>
        /// <param name="name">Store name to check.</param>
        /// <returns></returns>
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }
        #endregion
    }
}
