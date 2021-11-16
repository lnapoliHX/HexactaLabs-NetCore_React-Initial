using Stock.Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Api.MapperProfiles
{
    public  class ProviderDTO
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<Product> OfferedProducts { get; set; }
    }
}