using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IBalanceManager _balance;
        private readonly IPaymentHandler _payment;

        public PaymentManager(IBalanceManager balance, IPaymentHandler payment)
        {
            _balance = balance ?? throw new ArgumentNullException(nameof(_balance));
            _payment = payment ?? throw new ArgumentNullException(nameof(_payment));
        }

        public async Task<bool> MakeingPurchaseAsync(List<ItemModel> Items, uint type)
        {
            if (!await _payment.PurchaseCall(_payment.GetProducts(Items)))
                return false;
            _balance.Purchase(Items, type);
            return true;
        }
    }
}
