using System.Collections.Generic;
using Stock.Model.Base;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Phone { get; set; }

        public string Email { get; set; }

       // public List<ProductDTO> OfferedProducts { get; set; }
    }
}