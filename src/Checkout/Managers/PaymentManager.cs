using Checkout.Handlers;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentHandler _paymentHandler;

        public PaymentManager(IPaymentHandler paymentHandler)
        {
            _paymentHandler = paymentHandler ?? throw new ArgumentNullException(nameof(_paymentHandler));
        }

        public async Task<bool> MakeingPurchaseAsync(List<ItemModel> Items, uint type)
        {
            if (!await _paymentHandler.PurchaseCall(await _paymentHandler.GetProductsAsync(Items)))
                return false;
            //_balance.Purchase(Items, type);
            return true;
        }
    }
}
