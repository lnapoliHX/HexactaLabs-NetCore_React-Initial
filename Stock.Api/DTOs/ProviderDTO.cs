using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Stock.Model.Entities;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {       
        [Required]
        public string Name { get; set; }
        public string Id { get; set; }  

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<Product> OfferedProducts { get; set; }
    }
}