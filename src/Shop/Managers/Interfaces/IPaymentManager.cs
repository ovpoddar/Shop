using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Managers.Interfaces
{
    public interface IPaymentManager
    {
        OverallResult<List<Results<ItemModel>>> Purchase(PurchaseModel model);
    }
}
