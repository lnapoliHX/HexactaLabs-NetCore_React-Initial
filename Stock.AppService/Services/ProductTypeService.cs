using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class ProductTypeService: BaseService<ProductType>
    {                
        public ProductTypeService(IRepository<ProductType> repository) : base(repository) {}
		
        public new ProductType Create(ProductType entity)
        {
			if (!this.NombreUnico(entity.Initials)) { throw new System.Exception("Chosen initials are already in use"); }
			return base.Create(entity);
        }
		
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return false; }
            return this.Repository.List(x => x.Initials.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        public IEnumerable<ProductType> Search(Expression<Func<ProductType, bool>> filter)
        { return this.Repository.List(filter); }

    }
}