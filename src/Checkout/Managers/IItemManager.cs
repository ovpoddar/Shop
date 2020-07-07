using Checkout.ViewModels;
using Shop.Models;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public interface IItemManager
    {
        Task<string> Add(ItemViewModel model, string token);
        void remove(int id);
        ItemModel GetItem(string name);
        Task<ItemViewModel> Model(string name, string token);
    }
}
