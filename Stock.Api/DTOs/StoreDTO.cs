using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class StoreDTO
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

    }
}