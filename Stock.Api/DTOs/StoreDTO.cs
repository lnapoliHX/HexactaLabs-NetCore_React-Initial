using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class StoreDTO
    {
        [Required]
         [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Address { get; set; }

    }
}