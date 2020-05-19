using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Models;

namespace Shop.Handlers.Interfaces
{
    public interface IPaymentHandler
    {
        Task<bool> PurchaseCall(List<SaleProduct> products);
        Task<bool> SalesCall(List<SaleProduct> products);
        List<SaleProduct> GetProducts(List<ItemModel> items);
    }
}
