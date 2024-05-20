using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class ProviderService : BaseService<Provider>
    {        
        public ProviderService(IRepository<Provider> repository) : base(repository)
        {

        }

        public new Provider Create(Provider entity)
        {           
            return base.Create(entity);  
        }

        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}
