using System.Collections.Generic;
using Shop.Models;

namespace Shop.Managers.Interfaces
{
    public interface IBalanceManager
    {
        void Sales();
        void Purchase(List<ItemModel> items, uint purchaseType);
    }
}
