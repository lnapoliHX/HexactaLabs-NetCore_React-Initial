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
            if(this.isUniqueName(entity.Name))
            {
                return base.Create(entity);
            }
            throw new System.Exception("The name is already in used");

        }

        public new Provider Update(Provider entity)
        {
            if(this.isUniqueNameUpdate(entity.Name,entity.Id))
            {
                return base.Update(entity);
            }
            throw new SystemException("The name is already in used");
        }

        private bool isUniqueName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            return this.Repository.List(x=> x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        private bool isUniqueNameUpdate(string name, string id)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            return this.Repository.List(x=> x.Name.ToUpper().Equals(name.ToUpper()) && !(x.Id != id)).Count == 0;
        }

        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}