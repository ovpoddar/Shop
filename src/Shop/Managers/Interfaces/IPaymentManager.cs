using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Managers.Interfaces
{
    public interface IPaymentManager
    {
        ItemModel CreateModel(string id, string name, string brand, string quantity, string price, string totalPrice);
        Task<bool> MakeingPaymentAsync(List<ItemModel> Items, uint type);
    }
}
