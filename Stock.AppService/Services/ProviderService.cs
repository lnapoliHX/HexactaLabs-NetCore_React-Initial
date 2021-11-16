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

        public new Provider Create(Provider provider)
        {
            if(NombreUnico(provider.Name) && EmailUnico(provider.Email) && TelefonoUnico(provider.Phone))
            {
                return base.Create(provider);
            }
            throw new Exception("The name, email or phone are already in use");
        }

        private bool TelefonoUnico(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }
            return Repository.List(x => x.Phone.ToUpper().Equals(phone.ToUpper())).Count == 0;

        }

        private bool EmailUnico(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            return Repository.List(x => x.Email.ToUpper().Equals(email.ToUpper())).Count == 0;
        }

        private bool NombreUnico(string name)
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
