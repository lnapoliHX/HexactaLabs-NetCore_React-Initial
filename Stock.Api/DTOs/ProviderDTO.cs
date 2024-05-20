using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProviderDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Phone { get; set; }

        public string Email { get; set; }

        public List<Product> OfferedProducts { get; set; }

    
    public class ProductProvider
    {
        public string Id { get; set; }

    }
    }
}