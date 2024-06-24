using System;
using Stock.AppService.Base;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class ProviderService: BaseService<Provider>
    {
        public ProviderService(IRepository<Provider> repository): base(repository)
        {
            
        }   

        public new Provider Create(Provider entity)
        {
            if (!this.NameIsUnique(entity.Name))
            {
                throw new System.Exception("The name is already in use.");
            }

            return base.Create(entity);

        }

        private bool NameIsUnique(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
        
    }
}