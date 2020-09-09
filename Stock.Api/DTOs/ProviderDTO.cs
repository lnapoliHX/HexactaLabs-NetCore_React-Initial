using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        public string Id { get; set; }

        [Required]
        public string Initials { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
