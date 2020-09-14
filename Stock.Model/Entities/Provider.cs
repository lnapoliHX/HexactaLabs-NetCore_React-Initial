using Stock.Model.Base;

namespace Stock.Model.Entities
{
    public class Provider : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Product OfferedProducts { get; set; }
    }
}