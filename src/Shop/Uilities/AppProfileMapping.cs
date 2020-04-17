using AutoMapper;
using Shop.Entities;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Uilities
{
    public class AppProfileMapping : Profile
    {
        public AppProfileMapping()
        {
            CreateMap<Product, Suggestion>()
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.ProductName))
                .ForMember(opt => opt.Brand, opt => opt.MapFrom(o => o.Brands.BrandName));
        }
    }
}
