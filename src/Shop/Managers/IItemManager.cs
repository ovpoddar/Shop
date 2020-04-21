using Shop.Models;
using Shop.ViewModels;

namespace Shop.Managers
{
    public interface IItemManager
    {
        void add(ItemViewModel model);
        void remove(int id);
        ItemModel GetItem(string name);
        ItemViewModel model(string name);
    }
}
