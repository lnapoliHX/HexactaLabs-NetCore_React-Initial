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
        ///<summary>Constructor de la clase ProductService</summary>
        ///<param name="repository">Repositorio</param>
        public ProductService(IRepository<Product> repository) : base(repository) {}

        ///<summary>Metodo que crea un nuevo servicio de tipo Product</summary>
        ///<param name="entity">Entidad de tipo producto</param>
        ///<returns>Si la operacion fue correcta, retornara una nueva entidad de tipo Product</returns>
        public new Product Create(Product entity)
        {
            if (!this.NombreUnico(entity.Name)) throw new System.Exception("The name is already in use");
            if (!this.CostoPositivo(entity.CostPrice)) throw new System.Exception("Product cost price must be greater than zero");
            if (!this.CostoVentaPositivo(entity.SalePrice)) throw new System.Exception("Sale price of product must be greater than zero");
			return base.Create(entity);
        }
		
        ///<summary>Metodo que calcula si el costo de venta de un producto es mayor a cero (positivo)</summary>
        ///<param name="cost">Costo de venta proporcionado</param>
        ///<returns>Si el costo es mayor a cero, devuelve el valor True; de lo contrario devuelve False</returns>
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return false; }
            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        ///<summary>Metodo que calcula si el costo de un producto es mayor a cero (positivo)</summary>
        ///<param name="cost">Costo del producto proporcionado</param>
        ///<returns>Si el costo es mayor a cero, devuelve el valor True; de lo contrario devuelve False</returns>
        private bool CostoPositivo(decimal cost)
        {
            if (cost <= 0) return false;
            else return true;
        }

        ///<summary>Metodo que calcula si el costo de venta de un producto es mayor a cero (positivo)</summary>
        ///<param name="cost">Costo de venta proporcionado</param>
        ///<returns>Si el costo es mayor a cero, devuelve el valor True; de lo contrario devuelve False</returns>
        private bool CostoVentaPositivo(decimal cost)
        {
            if (cost <= 0) return false;
            else return true;
        }

        ///<summary>Metodo encargado de buscar una entidad de tipo Producto en base a un filtro</summary>
        public IEnumerable<Product> Search(Expression<Func<Product, bool>> filter)
        { return this.Repository.List(filter); }
    }
}