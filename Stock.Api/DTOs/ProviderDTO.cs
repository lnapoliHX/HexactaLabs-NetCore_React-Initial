using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Phone { get; set; }

        
        public string Email { get; set; }

      
       

    }
}
