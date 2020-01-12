using Shop.Entities;
using Shop.Handlers;
using Shop.ViewModels;
using System;

namespace Shop.Helpers
{
    public class CsvHelper :  ICsvHelper
    {
        private readonly IBrandHandler _brand;
        private readonly IWholesaleHandler _WholesaleHandler;
        private readonly ICategoryHandler _catagory;

        public CsvHelper(IBrandHandler brand, IWholesaleHandler WholesaleHandler, ICategoryHandler catagory)
        {
            _brand = brand ?? throw new ArgumentNullException(nameof(_brand));
            _WholesaleHandler = WholesaleHandler ?? throw new ArgumentNullException(nameof(_WholesaleHandler));
            _catagory = catagory ?? throw new ArgumentNullException(nameof(_catagory));
        }

        public double WholesaleID(int value1, int value2)
        {
            var wholesaleSize = new WholeSaleViewModel()
            {
                Package = value1,
                Size = value2
            };
            _WholesaleHandler.Add(wholesaleSize);
            return _WholesaleHandler.GetId(value1, value2);
        }

        public int Categorieauto(string v1)
        {
            var model = new CategoryViewModel()
            {
                Name = v1
            };
            _catagory.AddCategory(model);
            return _catagory.GetId(v1);
        }
        public void Categorieauto(string v1, string v2)
        {
            var id = _catagory.GetId(v1);
            var model = new CategoryViewModel()
            {
                Id = id,
                Name = v2
            };
            _catagory.AddCategory(model);
        }

        public int BrandId(string value)
        {
            var brand = new Brand()
            {
                BrandName = value
            };
            _brand.AddBrand(brand);
            return _brand.GetId(value);
        }
    }
}
