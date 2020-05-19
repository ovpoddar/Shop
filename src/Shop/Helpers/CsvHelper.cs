using Shop.Entities;
using Shop.Handlers;
using Shop.ViewModels;
using System;
using Shop.Handlers.Interfaces;

namespace Shop.Helpers
{
    public class CsvHelper : ICsvHelper
    {
        private readonly IBrandHandler _brand;
        private readonly IWholesaleHandler _wholesaleHandler;
        private readonly ICategoryHandler _category;

        public CsvHelper(IBrandHandler brand, IWholesaleHandler WholesaleHandler, ICategoryHandler catagory)
        {
            _brand = brand ?? throw new ArgumentNullException(nameof(_brand));
            _wholesaleHandler = WholesaleHandler ?? throw new ArgumentNullException(nameof(_wholesaleHandler));
            _category = catagory ?? throw new ArgumentNullException(nameof(_category));
        }

        public double WholesaleID(int size, int packege)
        {
            _wholesaleHandler.Add(new WholeSaleViewModel
            {
                Package = size,
                Size = packege
            });
            return _wholesaleHandler.GetId(size, packege);
        }

        public int Categoryauto(string name)
        {
            _category.AddCategory(new CategoryViewModel
            {
                Name = name
            });
            return _category.GetId(name);
        }
        public void Categoryauto(string underName, string name) =>
            _category.AddCategory(new CategoryViewModel
            {
                Id = _category.GetId(underName),
                Name = name
            });

        public int BrandId(string value)
        {
            _brand.AddBrand(new Brand()
            {
                BrandName = value
            });
            return _brand.GetId(value);
        }
    }
}
