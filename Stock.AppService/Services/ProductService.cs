using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{

    public class ProductService : BaseService<Product>
    {
    
        public ProductService(IRepository<Product> repository)
            : base(repository)
        {     
        }

        public new Product Create(Product entity)
        {
            if (NombreUnico(entity.Name))
            {
                return base.Create(entity);
            }

            throw new Exception("The name is already in use");
        }

        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

    }
}