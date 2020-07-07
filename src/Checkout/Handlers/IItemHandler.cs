using Shop.Models;
using System.Collections.Generic;

namespace Checkout.Handlers
{
    public interface IItemHandler<T>
    {
        List<T> List { get; set; }
        void AddItem(Results<ItemModel> model);
        void RemoveItem(ItemModel model);
        ItemModel GetItem(string name);
        ItemModel GetItem(int id);
        decimal Total();
    }
}
