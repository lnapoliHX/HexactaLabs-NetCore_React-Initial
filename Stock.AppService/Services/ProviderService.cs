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
            if (this.NombreUnico(entity.Name))
            {
                if(this.EmailUnico(entity.Email))
                {
                    return base.Create(entity);
                }
                
                throw new System.Exception("Email Existente: debe ingresar otro");
            }

            throw new System.Exception("Usuario Existente: debe ingresar otro");
        }
        private bool EmailUnico(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return this.Repository.List(x => x.Email.ToUpper().Equals(email.ToUpper())).Count == 0;
        }
        private bool NombreUnico(string name)
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