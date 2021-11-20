using Stock.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public virtual ProductType ProductType { get; set; }

        public int Stock { get; set; }

        public string ProviderId { get; set; }

        public Provider Provider { get; set; }
    }
}