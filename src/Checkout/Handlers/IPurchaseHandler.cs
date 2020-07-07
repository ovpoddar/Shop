using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Handlers
{
    public interface IPurchaseHandler
    {
        Task<OverallResult<List<Results<ItemModel>>>> MakePurchaseCallAsync(PurchaseModel model, string token);
    }
}
