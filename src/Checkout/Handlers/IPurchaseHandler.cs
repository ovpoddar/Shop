using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Handlers
{
    public interface IPurchaseHandler
    {
        Task<bool> MakePurchaseCallAsync(PurchaseModel model);
    }
}
