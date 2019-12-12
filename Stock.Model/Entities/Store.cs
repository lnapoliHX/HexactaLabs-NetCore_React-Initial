using Stock.Model.Base;

namespace Stock.Model.Entities
{
    public class Store : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}