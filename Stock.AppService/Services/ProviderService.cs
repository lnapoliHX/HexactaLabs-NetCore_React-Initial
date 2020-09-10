using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Stock.AppService.Services
{
    public class ProviderService: BaseService<Provider>
    {
        public ProviderService(IRepository<Provider> repository)
            : base(repository)
        {
        }
        public IEnumerable<Provider> getOne(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}