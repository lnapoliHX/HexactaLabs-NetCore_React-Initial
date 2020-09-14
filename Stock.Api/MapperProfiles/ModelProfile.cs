using AutoMapper;
using Stock.Api.DTOs;
using Stock.Model.Entities;

namespace Stock.Api.MapperProfiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
           // CreateMap<ProductType, ProductTypeDTO>()
            //   .IgnoreAllNonExisting()
            //   .ReverseMap()
            //   .ForMember(s => s.Id, opt => opt.Ignore());

          //  CreateMap<Product, ProductDTO>()
          //      .ForMember(d => d.ProductTypeId, opt => opt.MapFrom(s => s.ProductType.Id))
           //     .ForMember(d => d.ProductTypeDesc, opt => opt.MapFrom(s => s.ProductType.Description))
           //     .ReverseMap()
           //     .ForMember(s => s.Id, opt => opt.Ignore())
           //     .ForMember(s => s.ProductType, opt => opt.Ignore());       

           CreateMap<Provider, ProviderDTO>().ReverseMap();         
        }        
    }


}
