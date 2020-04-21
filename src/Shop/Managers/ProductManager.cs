using Shop.Handlers;
using Shop.ViewModels;
using System;
using System.Net;
using AutoMapper;
using Shop.Entities;
using Shop.Models;

namespace Shop.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly ICategoryHandler _categoryHandler;
        private readonly IProductHandler _productHandler;
        private readonly IBrandHandler _brandHandler;
        private readonly IMapper _mapper;

        public ProductManager(ICategoryHandler categoryHandler, IProductHandler productHandler, IBrandHandler brandHandler, IMapper mapper)
        {
            _categoryHandler = categoryHandler;
            _productHandler = productHandler;
            _brandHandler = brandHandler;
            _mapper = mapper;
        }

        public ProductListViewModel GetFilteredModel(int id, int pageNumber) => new ProductListViewModel
        {
            Categories = _categoryHandler.Categories(),
            Products = _productHandler.Products(id, pageNumber - 1),
            TotalNo = _productHandler.TotalCount(id)
        };

        public Results<SaleProduct> GetProductById(int productId)
        {
            try
            {
                var product = _productHandler.GetProduct(productId);

                return new Results<SaleProduct>
                {
                    Result = _mapper.Map<SaleProduct>(product),
                    HttpStatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception exception)
            {
                return new Results<SaleProduct> { HttpStatusCode = HttpStatusCode.InternalServerError, Exception = exception.Message};
            }
        }

        public Results<SaleProduct> UpdateStockLevel(SaleProduct saleProduct) =>
            _productHandler.RemoveProduct(saleProduct);

        public ProductListViewModel GetModel(int pageNumber) => new ProductListViewModel
        {
            Categories = _categoryHandler.Categories(),
            Products = _productHandler.Products(pageNumber - 1),
            TotalNo = _productHandler.TotalCount()
        };
    }
}
