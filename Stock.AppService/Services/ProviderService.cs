using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;

namespace Stock.AppService.Services
{
    public class ProviderService : BaseService<Provider>
    {
        public ProviderService(Repository.LiteDb.Interface.IRepository<Provider> repository) : base(repository)
        {
        }

        public new Provider Create(Provider entity)
        {
            return null;
        }

        private bool NombreUnico(string name)
        {
            return false;
        }

        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return null;
        }
    }
}