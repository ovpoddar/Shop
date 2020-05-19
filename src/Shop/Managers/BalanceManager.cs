using Shop.Entities;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
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

        public void Purchase(List<ItemModel> items, uint purchaseType)
        {
            foreach (var item in items)
            {
                var product = _product.GetProduct(item.Name);
                _balance.AddBalance(new Balance
                {
                    Date = DateTime.Now,
                    PaymentTypeId = (int)purchaseType,
                    ProductId = product.Id,
                    Quantity = (uint)item.Quantity,
                    Incoming = product.Price * item.Quantity,
                    Ammount = _balance.GetLastBalance().Ammount + product.Price * item.Quantity
                });
            }
        }
    }
}
