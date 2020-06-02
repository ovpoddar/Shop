using Checkout.Models;
using Shop.Models;
using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Managers.Interfaces
{
    public interface IItemManager
    {
        void add(ItemViewModel model);
        void remove(int id);
        ItemModel GetItem(string name);
        ItemViewModel Model(string name);
        List<ItemModel> CreateItemModels(List<string> ids, List<string> names, List<string> brands, List<string> quantitys, List<string> prices, List<string> totalPrices);
    }
}
