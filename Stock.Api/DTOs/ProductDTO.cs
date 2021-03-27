using System.ComponentModel.DataAnnotations;
using Stock.Model.Entities;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public virtual ProductType ProductType { get; set; }

        [Required]
        public int Stock { get; }

        [Required]
        public string ProviderId { get; set; }
 
        [Required]
        public Provider Provider { get; set; }
    }
}