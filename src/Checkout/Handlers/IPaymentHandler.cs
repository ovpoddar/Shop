using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Handlers
{
    public interface IPaymentHandler
    {
        Task<bool> PurchaseCall(List<SaleProduct> products);
        Task<bool> SalesCall(List<SaleProduct> products);
        Task<List<SaleProduct>> GetProductsAsync(List<ItemModel> items);
    }
}
