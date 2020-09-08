using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeDesc { get; set; }
    }
}
