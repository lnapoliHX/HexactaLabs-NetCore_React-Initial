using AutoMapper;
using Stock.Api.DTOs;
using Stock.Model.Entities;

namespace Stock.Api.MapperProfiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<ProductType, ProductTypeDTO>()
                //.IgnoreAllNonExisting()
                .ReverseMap()
                .ForMember(s => s.Id, opt => opt.Ignore());
            
            CreateMap<Store, StoreDTO>()
                //.IgnoreAllNonExisting()
                .ReverseMap()
                .ForMember(s => s.Id, opt => opt.Ignore());

            // CreateMap<Product, ProductDTO>()
            //     .ForMember(d => d.ProductTypeId, opt => opt.MapFrom(s => s.ProductType.Id))
            //     .ForMember(d => d.ProductTypeDesc, opt => opt.MapFrom(s => s.ProductType.Description))
            //     .ReverseMap()
            //     .ForMember(s => s.Id, opt => opt.Ignore())
            //     .ForMember(s => s.ProductType, opt => opt.Ignore());       

            

            CreateMap<Provider, ProviderDTO>()
               .ForMember(c => c.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(c => c.Name, opt => opt.MapFrom(s => s.Name))
               .ForMember(c => c.Phone, opt => opt.MapFrom(s => s.Phone))
               .ForMember(c => c.Email, opt => opt.MapFrom(s => s.Email))

               .ForMember(c => c.OfferedProducts, opt => opt.MapFrom(s => s.OfferedProducts))
               .ReverseMap()
               .ForMember(s => s.Id, opt => opt.Ignore());
            //.ReverseMap()
            //.ForPath(s => s.OfferedProducts, opt => opt.Ignore());

            CreateMap<Provider, ProviderSearchDTO>()
               .ForMember(c => c.Name, opt => opt.MapFrom(s => s.Name))
               .ForMember(c => c.Phone, opt => opt.MapFrom(s => s.Phone))
               .ForMember(c => c.Email, opt => opt.MapFrom(s => s.Email));


        }
    }


}
