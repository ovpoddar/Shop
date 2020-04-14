using Shop.Entities;
using Shop.Handlers;
using Shop.ViewModels;
using System;
using System.Collections.Generic;

namespace Shop.Managers
{
    public class BalanceManager : IBalanceManager
    {
        private readonly IBalanceHandler _balance;
        private readonly IProductHandler _product;

        public BalanceManager(IBalanceHandler balance, IProductHandler product)
        {
            _balance = balance ?? throw new ArgumentNullException(nameof(_balance));
            _product = product ?? throw new ArgumentNullException(nameof(_product));
        }

        public void Sales()
        {
            throw new NotImplementedException();
            //all done
        }

        public void Purchase(List<ItemViewModel> products, uint purchaseType)
        {
            foreach(var product in products)
            {
                var price = _product.GetProduct(product.Id).Price * product.Quantity;
                _balance.AddBalance(new Balance
                {
                    Date = DateTime.Now,
                    PaymentTypeId = (int)purchaseType,
                    ProductId = product.Id,
                    Quantity = (uint)product.Quantity,
                    Incoming = price,
                    Ammount = _balance.GetLastBalance().Ammount + price
                });
            }
        }
    }
}
