using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Model.Exceptions;
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
            this.ValidateName(entity.Name);
            return base.Create(entity);
        }

        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("The name can't be empty");
            }
            if (this.Repository.List().Any(provider => provider.Equals(name))) {
                throw new ValidationException("The name is already in use");
            }
        }
    }
}