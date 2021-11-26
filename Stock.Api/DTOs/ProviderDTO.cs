using Stock.Model.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<ProductDTO> OfferedProducts { get; set; }

    }

    public class ProductDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public virtual ProductTypeDTO ProductType { get; set; }

        private int _stock;

        public int Stock => _stock;

        public void DescontarStock(int value)
        {
            if (_stock - value < 0)
                throw new ModelException("No hay stock disponible para efectuar la operación.");

            _stock -= value;
        }

        public void SumarStock(int value)
        {
            _stock += value;
        }

        public string ProviderId { get; set; }

        public ProviderDTO Provider { get; set; }
    }

}