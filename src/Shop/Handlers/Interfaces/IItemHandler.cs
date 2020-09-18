using Shop.Models;

namespace Shop.Handlers.Interfaces
{
    public interface IItemHandler
    {
        Results<ItemModel> AddItem(string productName, int Quantity);
    }
}
