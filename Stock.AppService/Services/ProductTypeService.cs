using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class ProductTypeService: BaseService<ProductType>
    {                
        public ProductTypeService(IRepository<ProductType> repository)
            : base(repository)
        {
        }

        public new ProductType Create(ProductType entity)
        {
            if (this.UniqueInitials(entity.Id, entity.Initials) && 
                this.UniqueDescription(entity.Id, entity.Description))
            {
                return base.Create(entity);
            }

            throw new System.Exception("The Initials or the Description are already in use");
        }

        private bool UniqueInitials(string id, string initials)
        {
            if (string.IsNullOrWhiteSpace(initials))
            {
                return false;
            }

            return this.Repository.List(x => x.Initials.ToUpper().
                                                Equals(initials.ToUpper()) &&
                                             !x.Id.Equals(id)).Count == 0;
        }

        private bool UniqueDescription(string id, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return false;
            }

            return this.Repository.List(x => x.Description.ToUpper().
                                                Equals(description.ToUpper()) &&
                                             !x.Id.Equals(id)).Count == 0;
        }

        public new ProductType Update(ProductType entity)
        {
            if (this.UniqueInitials(entity.Id, entity.Initials) && 
                this.UniqueDescription(entity.Id, entity.Description))
            {
                return base.Update(entity);
            }

            throw new System.Exception("The Initials or the Description are already in use");
        }
    }
}
