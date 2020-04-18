using Shop.Handlers;
using Shop.ViewModels;
using System;
using System.Net;
using Shop.Entities;
using Shop.Models;

namespace Shop.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly ICategoryHandler _categoryHandler;
        private readonly IProductHandler _productHandler;
        private readonly IBrandHandler _brandHandler;

        public ProductManager(ICategoryHandler categoryHandler, IProductHandler productHandler, IBrandHandler brandHandler)
        {
            _categoryHandler = categoryHandler;
            _productHandler = productHandler;
            _brandHandler = brandHandler;
        }

        public ProductListViewModel GetFilteredModel(int id, int pageNumber) => new ProductListViewModel
        {
            Categories = _categoryHandler.Categories(),
            Products = _productHandler.Products(id, pageNumber - 1),
            TotalNo = _productHandler.TotalCount(id)
        };

        public Results<Product> GetProductById(int productId)
        {
            try
            {
                var product = _productHandler.GetProduct(productId);
                return new Results<Product>
                {
                    Result = product,
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception exception)
            {
                return new Results<Product> { HttpStatusCode = HttpStatusCode.InternalServerError, Exception = exception.Message};
            }
        }

        public Results<SaleProduct> UpdateStockLevel(SaleProduct saleProduct) =>
            _productHandler.RemoveProduct(saleProduct);

        public Results<Brand> AddBrand(Brand brand)
        {
            var brandWithReturn =  _brandHandler.AddBrandWithReturn(brand);

            return new Results<Brand> {HttpStatusCode = HttpStatusCode.OK, Result = brandWithReturn};
        }

        public ProductListViewModel GetModel(int pageNumber) => new ProductListViewModel
        {
            Categories = _categoryHandler.Categories(),
            Products = _productHandler.Products(pageNumber - 1),
            TotalNo = _productHandler.TotalCount()
        };
    }
}
