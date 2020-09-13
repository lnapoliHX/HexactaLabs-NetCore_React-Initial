using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stock.Model.Entities;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        [Required]
        public string Name { get; set; }
        public string Id { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

    }
}