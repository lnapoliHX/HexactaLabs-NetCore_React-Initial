using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.AppService.Base
{
    public class ProviderService : BaseService<Provider>
    {
        public ProviderService(IRepository<Provider> repository) : base(repository)
        {
        }
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
        public new Provider Create(Provider entity)
        {
            if (this.ValidarUnicidad(entity))
            {
                return base.Create(entity);
            }

            throw new System.Exception("This Provider is already exist");
        }
        private bool ValidarUnicidad(Provider entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Email) || string.IsNullOrWhiteSpace(entity.Phone))
            {
                return false;
            }

            return this.Repository.List(x => x.Email.ToUpper().Equals(entity.Email.ToUpper()) && x.Phone.ToUpper().Equals(entity.Phone.ToUpper())).Count == 0;
        }        
    }
}
