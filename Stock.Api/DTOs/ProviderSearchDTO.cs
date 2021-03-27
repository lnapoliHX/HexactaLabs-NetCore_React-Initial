using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderSearchDTO
    {
         public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

    }
}