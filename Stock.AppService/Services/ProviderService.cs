using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{

    public class ProviderService : NameService<Provider>
    {
        public ProviderService(IRepository<Provider> repository) : base(repository)
        {

        }

        public new Provider Create(Provider provider)
        {
            if(NombreUnico(provider.Name))
            {
                return base.Create(provider);
            }
            throw new Exception("The name is already in use");
        }
    
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return Repository.List(filter);
        }

    }
}
