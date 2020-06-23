using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Handlers
{
    public interface IPurchaseHandler
    {
        Task<OverallResult<List<Results<ItemModel>>>> MakePurchaseCallAsync(PurchaseModel model);
    }
}
