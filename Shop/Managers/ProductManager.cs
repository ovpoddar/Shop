using Shop.Handlers;
using Shop.ViewModels;
using System;

namespace Shop.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly ICategoryHandler _categoryHandler;
        private readonly IProductHandler _productHandler;
        public ProductManager(ICategoryHandler categoryHandler, IProductHandler productHandler)
        {
            _categoryHandler = categoryHandler ?? throw new ArgumentNullException(nameof(CategoryHandler));
            _productHandler = productHandler ?? throw new ArgumentNullException(nameof(productHandler));
        }

        public ProductListViewModel GetFilteredModel(int id, int PageNumber) => new ProductListViewModel
        {
            Categories = _categoryHandler.Categories(),
            Products = _productHandler.Products(id, PageNumber),
            TotalNo = _productHandler.TotalCount()
        };

        public ProductListViewModel GetModel() => new ProductListViewModel
        {
            Categories = _categoryHandler.Categories(),
            Products = _productHandler.Products(1),
            TotalNo = _productHandler.TotalCount()
        };

        public ProductListViewModel GetModel(int PageNumber) => new ProductListViewModel
        {
            Categories = _categoryHandler.Categories(),
            Products = _productHandler.Products(PageNumber),
            TotalNo = _productHandler.TotalCount()
        };
    }
}
