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
        public ProductService(IRepository<Product> repository) : base(repository) {}

        public new Product Create(Product entity)
        {
			if (!this.NombreUnico(entity.Name)) { throw new System.Exception("The name is already in use"); }
			if (!this.CostoPositivo(entity.CostPrice)) { throw new System.Exception("Product cost price must be greater than zero"); }
			if (!this.CostoVentaPositivo(entity.SalePrice)) { throw new System.Exception("Sale price of product must be greater than zero"); }
			return base.Create(entity);
        }
		
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return false; }
            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        private bool CostoPositivo(decimal cost)
        {
            if (cost <= 0) { return false; }
            else return true;
        }

        private bool CostoVentaPositivo(decimal cost)
        {
            if (cost <= 0) { return false; }
            else return true;
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> filter)
        { return this.Repository.List(filter); }
    }
}