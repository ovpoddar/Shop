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
            _WholesaleHandler.Add(new WholeSaleViewModel()
            {
                Package = value1,
                Size = value2
            });
            return _WholesaleHandler.GetId(value1, value2);
        }

        public int Categorieauto(string v1)
        {
            _catagory.AddCategory(new CategoryViewModel()
            {
                Name = v1
            });
            return _catagory.GetId(v1);
        }
        public void Categorieauto(string v1, string v2) =>
            _catagory.AddCategory(new CategoryViewModel()
            {
                Id = _catagory.GetId(v1),
                Name = v2
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
