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

            //CreateMap<Product, ProductDTO>()
                //.ForMember(d => d.ProductTypeId, opt => opt.MapFrom(s => s.ProductType.Id))
                //.ForMember(d => d.ProductTypeDesc, opt => opt.MapFrom(s => s.ProductType.Description))
                //.ForMember(d => d.ProductType.Id, opt => opt.MapFrom(s => s.ProductType.Id))
                //.ForMember(d => d.ProductType.Description, opt => opt.MapFrom(s => s.ProductType.Description))
                //.ReverseMap()
                //.ForMember(s => s.Id, opt => opt.Ignore())
                //.ForMember(s => s.ProductType, opt => opt.Ignore());       

            CreateMap<Provider, ProviderDTO>()
                //.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                //.ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                //.ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ReverseMap();
                //.ForMember(s => s.Id, opt => opt.Ignore());         
        }        
    }


}
