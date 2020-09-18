using Shop.Models;
using System.Collections.Generic;

namespace Shop.Managers.Interfaces
{
    public interface IPaymentManager
    {
        OverallResult<List<Results<ItemModel>>> Purchase(PurchaseModel model);
    }
}
