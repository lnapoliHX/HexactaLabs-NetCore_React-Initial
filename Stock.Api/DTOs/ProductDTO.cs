using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public virtual ProductTypeDTO ProductType { get; set; }

        private int _stock;       

        public string ProviderId { get; set; }
        public ProviderDTO Provider { get; set; }
    }

}