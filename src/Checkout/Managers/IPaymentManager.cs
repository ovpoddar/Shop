using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public interface IPaymentManager
    {
        Task<bool> MakeingPurchaseAsync(List<ItemModel> list, uint payment);
    }
}
