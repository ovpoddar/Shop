using Shop.Handlers;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;

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
        public ItemModel CreateModel(string id, string name, string brand, string quantity, string price, string totalPrice) =>
            new ItemModel
            {
                Id = int.Parse(id),
                Name = name,
                Brand = brand,
                Price = decimal.Parse(price),
                Quantity = int.Parse(quantity),
                TotalPrice = double.Parse(totalPrice)
            };

        public async Task<bool> MakeingPaymentAsync(List<ItemModel> Items, uint type)
        {
            if (!await _payment.PurchaseCall(_payment.GetProducts(Items)))
                return false;
            _balance.Purchase(Items, type);
            return true;
        }
    }
}
