using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using System;
using System.Collections.Generic;

namespace Shop.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentHandler _paymentHandler;
        private readonly IProductHandler _productHandler;

        public PaymentManager(IPaymentHandler paymentHandler, IProductHandler productHandler)
        {
            _paymentHandler = paymentHandler ?? throw new ArgumentNullException(nameof(_paymentHandler));
            _productHandler = productHandler ?? throw new ArgumentNullException(nameof(_productHandler));
        }

        public OverallResult<List<Results<ItemModel>>> Purchase(PurchaseModel model)
        {
            var result = new OverallResult<List<Results<ItemModel>>>()
            {
                Success = true,
                Objects = new List<Results<ItemModel>>()
            };
            foreach (var product in model.Items)
            {
                var responce = _productHandler.RemoveProduct(product);
                result.Objects.Add(responce);
                if (!responce.Success)
                    result.Success = false;

            }
            return result;
        }

    }
}
