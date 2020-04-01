using AutoMapper;
using Shop.Entities;
using Shop.Models;

namespace Shop.Uilities
{
    public class AppProfileMapping : Profile
    {
        public AppProfileMapping()
        {
            CreateMap<Product, SuggestionModel>()
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.ProductName))
                .ForMember(opt => opt.Brand, opt => opt.MapFrom(o => o.Brand.BrandName));
        }
    }
}
