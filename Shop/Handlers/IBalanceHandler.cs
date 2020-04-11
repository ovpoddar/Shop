using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface IBalanceHandler
    {
        void Purchase();
        void Sales(List<ItemViewModel> products, string purchaseType);
    }
}
