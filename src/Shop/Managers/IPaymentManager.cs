using Shop.Models;
using Shop.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public interface IPaymentManager
    {
        Task<bool> PurchaseCall(List<SaleProduct> products);
        ItemModel CreateModel(string id, string name, string brand, string quantity, string price, string totalPrice);
        PaymentViewModel GetModel(List<ItemModel> items, decimal total);
    }
}
