using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Stock.Model.Base;
using Stock.Model.Entities;

namespace Stock.Api.DTOs
{
    public class ProviderDTO : IEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

    }
}