using Stock.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Model.Entities
{
    [Table("producttype")]
    public class ProductType: IEntity
    {
        public string Id { get; set; }

        public string Initials { get; set; }

        public string Description { get; set; }
    }
}
