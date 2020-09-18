﻿using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Stock.AppService.Services
{
    
        public class ProviderService : BaseService<Provider>
        {
            public ProviderService(IRepository<Provider> repository) : base(repository)
            {

            }

        
        public new Provider Create(Provider entity) // ID, Name, Phone, Email, List<Product> OfferedProducts
        {
            if (this.NombreUnico(entity.Name))
            {
                return base.Create(entity);
            }
            throw new System.Exception("The name is already in use");
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
