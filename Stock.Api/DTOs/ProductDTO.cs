using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public virtual ProductTypeDTO ProductType { get; set; }
        public int Stock {get;}
        public string ProviderId { get; set; }
        public ProviderDTO Provider { get; set; }
    }
}