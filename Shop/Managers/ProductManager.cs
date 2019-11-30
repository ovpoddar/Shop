using Shop.Handlers;
using Shop.ViewModels;
using System;

namespace Shop.Managers
{
    public class ProductManager : IProductManager
    {
        public readonly ICategoryHandler CategoryHandler;
        public readonly IProductHandler ProductHandler;
        public ProductManager(ICategoryHandler categoryHandler, IProductHandler productHandler)
        {
            CategoryHandler = categoryHandler ?? throw new ArgumentNullException(nameof(CategoryHandler));
            ProductHandler = productHandler ?? throw new ArgumentNullException(nameof(productHandler));
        }

        public ProductListViewModel GetFilteredModel(int id) => new ProductListViewModel
        {
            Categories = CategoryHandler.Categories(),
            Products = ProductHandler.Products(id)
        };

        public ProductListViewModel GetModel() => new ProductListViewModel
        {
            Categories = CategoryHandler.Categories(),
            Products = ProductHandler.Products()
        };
    }
}
