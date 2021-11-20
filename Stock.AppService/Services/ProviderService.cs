using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

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
    }
}
