using Checkout.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Managers.Interfaces
{
    public interface IPaymentManager
    {
        Task<bool> MakeingPurchaseAsync(List<ItemModel> Items, uint type);
    }
}
