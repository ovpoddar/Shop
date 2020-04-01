using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Managers
{
    public interface IPaymentManager
    {
        ItemViewModel GetViewModels(string qunatity, string productId);
        void PurchaseCall(List<ItemViewModel> models);
    }
}
