using System.Collections.Generic;
using Stock.Model.Base;

namespace Stock.Model.Entities
{
    public class Provider : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Cellphone { get; set; }

        public string Emilio { get; set; }

        public List<Product> OfferedProducts { get; set; }
    }
}