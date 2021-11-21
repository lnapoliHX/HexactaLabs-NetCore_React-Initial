using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public object ProductTypeId { get; internal set; }
        public object ProductTypeDesc { get; internal set; }
    }
}
