using Stock.Model.Base;

namespace Stock.Model.Entities
{
    public class Provider : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}