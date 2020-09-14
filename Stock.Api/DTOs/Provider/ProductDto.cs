using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs.Provider
{
    public class ProductDto
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public ProductTypeDTO ProductType { get; set; }

        [Required]
        public int Stock { get; set; }

        public string ProviderId { get; set; }
    }
}