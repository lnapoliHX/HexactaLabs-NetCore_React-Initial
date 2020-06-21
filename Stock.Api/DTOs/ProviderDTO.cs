using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
        
        [Required]
        public string Email { get; set; }
    }
}