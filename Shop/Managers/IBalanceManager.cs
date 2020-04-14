using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Managers
{
    public interface IBalanceManager
    {
        void Sales();
        void Purchase(List<ItemViewModel> products, uint purchaseType);
    }
}
