using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stock.Model.Entities;
//using System.Collections.Generic;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
         [Required]
         [StringLength(100)]
        public string Name { get; set; }
  
         [Required]
         [StringLength(100)]
        public string Id { get; set; }

         [Required]
         [StringLength(100)]
        public string Phone { get; set; }
   
         [Required]
         [StringLength(100)]
        public string Email { get; set; }

        [Required]
         [StringLength(1000)]
        public List<Product> OfferedProducts { get; set; }
        
    }
}