using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Stock.AppService.Services
{
    public class ProviderService: BaseService<Provider>
    {
        public ProviderService(IRepository<Provider> repository)
            : base(repository)
        {
        }

        public new Provider Create(Provider provider){

            if(!string.IsNullOrWhiteSpace(provider.Name)){

                //Comprueba que el nombre sea unico
                if(UniqueName(provider.Name)){
                    return base.Create(provider);        
                }
            }
            throw new Exception("the name already in use");
        }

        public bool UniqueName(string name){
            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count==0;
        }
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}