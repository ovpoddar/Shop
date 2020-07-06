using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using System.Collections.Generic;
using System.Net;

namespace Shop.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly ICategoryHandler _categoryHandler;
        private readonly IProductHandler _productHandler;
        private readonly IBalanceManager _balanceManager;
        private readonly IPaymentManager _paymentManager;

        public ProductManager(ICategoryHandler categoryHandler, IProductHandler productHandler, IBalanceManager balanceManager, IPaymentManager paymentManager)
        {
            _categoryHandler = categoryHandler ?? throw new System.ArgumentNullException(nameof(_categoryHandler));
            _productHandler = productHandler ?? throw new System.ArgumentNullException(nameof(_productHandler));
            _balanceManager = balanceManager ?? throw new System.ArgumentNullException(nameof(_balanceManager));
            _paymentManager = paymentManager ?? throw new System.ArgumentNullException(nameof(_paymentManager));
        }

        public ProductListViewModel GetFilteredModel(int id, int pageNumber) =>
            new ProductListViewModel
            {
                Categories = _categoryHandler.Categories(),
                Products = _productHandler.Products(id, pageNumber - 1),
                TotalNo = _productHandler.TotalCount(id)
            };

        public ProductListViewModel GetModel(int pageNumber) =>
            new ProductListViewModel
            {
                Categories = _categoryHandler.Categories(),
                Products = _productHandler.Products(pageNumber - 1),
                TotalNo = _productHandler.TotalCount()
            };

        public OverallResult<List<Results<ItemModel>>> SalesProduct(PurchaseModel purchaseModel, string user)
        {
            var result = _paymentManager.Purchase(purchaseModel);
            if (!result.Success)
                return new OverallResult<List<Results<ItemModel>>>
                {
                    Success = false,
                    Objects = result.Objects
                };
            _balanceManager.Purchase(purchaseModel.Items, purchaseModel.PaymentType, user);
            return new OverallResult<List<Results<ItemModel>>>
            {
                Success = true,
                Objects = result.Objects
            };
        }
    }
}
