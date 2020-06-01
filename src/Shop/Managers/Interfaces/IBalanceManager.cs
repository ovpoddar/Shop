using Shop.Models;
using System.Collections.Generic;

namespace Shop.Managers.Interfaces
{
    public interface IBalanceManager
    {
        void Sales();
        void Purchase(List<ItemModel> items, uint purchaseType);
    }
}
