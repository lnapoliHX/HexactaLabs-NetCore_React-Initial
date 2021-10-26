using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    /// <summary>
    /// Product type service.
    /// </summary>
    public class ProductTypeService: BaseService<ProductType>
    {                
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeService"/> class.
        /// </summary>
        /// <param name="repository">Product type repository.</param>
        public ProductTypeService(IRepository<ProductType> repository)
            : base(repository)
        {
        }
    }
}
