using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs.Provider
{
    public class ProviderDto
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // NOT REQUIRED AT THE MOMENT
        //[Required]
        //public List<ProductDto> OfferedProducts { get; set; }
    }
}