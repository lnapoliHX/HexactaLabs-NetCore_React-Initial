using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.AppService.Services
{
    public class ProviderService : BaseService<Provider>
    {
        public ProviderService(IRepository<Provider> repository) : base(repository)
        {

        }

        public new Provider Create(Provider entity)
        {

            if (this.VerificarEmail(entity.Email))
            {
                return base.Create(entity);
            }

            throw new System.Exception("El email ya fue utilizado.");

        }
        public new Provider Delete (string ID)
        {
            var provider = base.Get(ID);

            if (!(provider == null))
            {
                base.Delete(provider);
                return provider;
              
            }
            throw new System.Exception("No existe el ID indicado.");
            
        }
        public new Provider Get(string ID)
        {
            var provider = base.Get(ID);

            if (!(provider == null))
            {
               
                return provider;

            }
            throw new System.Exception("No existe el ID indicado.");

        }


        // Validacion correo repetido 
        private bool VerificarEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }

            return this.Repository.List(x => x.Email.ToUpper().Equals(Email.ToUpper())).Count == 0;
        }

        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}
