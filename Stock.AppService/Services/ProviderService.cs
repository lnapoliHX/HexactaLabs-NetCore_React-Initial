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
        public ProviderService(IRepository<Provider> repository) : base(repository)
        {
        }

        public new Provider Create(Provider provider)
        {
            if (NombreUnicoProvider(provider.Name))
            {
                return base.Create(provider);
            }
            throw new Exception("The name is already in use");
        }

        private bool NombreUnicoProvider(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }
        
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return Repository.List(filter);
        }


    }
}
