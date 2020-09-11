namespace Stock.Api.DTOs
{

    using System.ComponentModel.DataAnnotations;
    public class ProviderDTO
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}