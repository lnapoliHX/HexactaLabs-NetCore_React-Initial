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
            if (this.NombreUnico(entity.Id, entity.Name))
            {
                return base.Create(entity);
            }

            throw new System.Exception("The name is already in use");
        }

        private bool NombreUnico(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper()) &&
                                             !x.Id.Equals(id)).Count == 0;
        }

        public new Provider Update(Provider entity)
        {
            if (this.NombreUnico(entity.Id, entity.Name))
            {
                return base.Update(entity);
            }

            throw new System.Exception("The name is already in use");
        }
       
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}