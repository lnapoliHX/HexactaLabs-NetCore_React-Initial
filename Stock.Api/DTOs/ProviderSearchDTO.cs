using System;
using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderSearchDTO
    {
        [Required]
        public string Name { get; set; }

        public string Id { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        public object Condition { get; internal set; }
    }

}