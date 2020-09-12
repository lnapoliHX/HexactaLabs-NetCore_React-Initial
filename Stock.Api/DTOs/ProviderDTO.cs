using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    
        public string Description { get; set; }

        public string Mail { get; set; }
    }
}
