using AutoMapper;
using DataAccess.Entities;
using Microsoft.VisualBasic;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Utilities
{
    public class AppProfileMapping : Profile
    {
        public AppProfileMapping()
        {
            CreateMap<Product, Suggestion>()
                .ForMember(opt => opt.Name, opt => opt.MapFrom(o => o.ProductName))
                .ForMember(opt => opt.Brand, opt => opt.MapFrom(o => o.Brands.BrandName));

            CreateMap<Product, SaleProduct>()
                .ForMember(opt => opt.ProductId, opt => opt.MapFrom(product => product.Id))
                .ForMember(opt => opt.ProductName, opt => opt.MapFrom(product => product.ProductName))
                .ForMember(opt => opt.BarCode, opt => opt.MapFrom(product => product.BarCode))
                .ForMember(opt => opt.Price, opt => opt.MapFrom(product => product.Price))
                .ForMember(opt => opt.StockLevel, opt => opt.MapFrom(product => product.StockLevel))
                .ForMember(opt => opt.OrderLevel, opt => opt.MapFrom(product => product.OrderLevel))
                .ForMember(opt => opt.CategoriesId, opt => opt.MapFrom(product => product.CategoriesId))
                .ForMember(opt => opt.Category, opt => opt.MapFrom(product => product.Categories.Name))
                .ForMember(opt => opt.BrandId, opt => opt.MapFrom(product => product.BrandId))
                .ForMember(opt => opt.Brand, opt => opt.MapFrom(product => product.Brands.BrandName));

            CreateMap<SignInViewModel, Employer>()
                .ForMember(opt => opt.UserName, opt => opt.MapFrom(signInViewModel => signInViewModel.UserName))
                .ForMember(opt => opt.PhoneNumber, opt => opt.MapFrom(signInViewModel => signInViewModel.MobileNo.ToString()))
                .ForMember(opt => opt.Email, opt => opt.MapFrom(signInViewModel => signInViewModel.Email))
                .ForMember(opt => opt.LastName, opt => opt.MapFrom(signInViewModel => signInViewModel.LastName))
                .ForMember(opt => opt.FirstName, opt => opt.MapFrom(signInViewModel => signInViewModel.FirstName))
                .ForMember(opt => opt.Gender, opt => opt.MapFrom(signInViewModel => signInViewModel.Gender.ToString()))
                .ForMember(opt => opt.Active, opt => opt.MapFrom(o => true))
                .ForMember(opt => opt.LastLogin, opt => opt.MapFrom(signInViewModel => DateAndTime.Now))
                .ForMember(opt => opt.City, opt => opt.MapFrom(signInViewModel => signInViewModel.City));
        }
    }
}
