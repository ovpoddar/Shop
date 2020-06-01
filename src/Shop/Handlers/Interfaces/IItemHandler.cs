using System.Collections.Generic;
using Shop.Entities;
using Shop.Models;

namespace Shop.Handlers.Interfaces
{
    public interface IItemHandler<T>
    {
        List<T> List { get; set; }
        void addItem(Product product, int quantity);
        void RemoveItem(Product old);
        ItemModel GetItem(string name);
    }
}
