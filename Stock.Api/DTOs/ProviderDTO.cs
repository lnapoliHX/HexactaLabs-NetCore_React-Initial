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
        
        [Required(ErrorMessage ="Please enter a valid Name")]
        [StringLength(70)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter a valid Phone number")]
        public string Phone { get; set; }
        
        [EmailAddress(ErrorMessage ="Please enter a valid Email")]
        public string Email { get; set; }
    }
}
