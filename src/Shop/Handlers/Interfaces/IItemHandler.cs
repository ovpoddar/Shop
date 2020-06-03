using Checkout.Models;
using DataAccess.Entities;
using System.Collections.Generic;

namespace Shop.Handlers.Interfaces
{
    public interface IItemHandler
    {
        List<ItemModel> List { get; set; }
        void addItem(Product product, int quantity);
        void RemoveItem(Product old);
        void RemoveItem(int id);
        ItemModel GetItem(string name);
        double Total();
    }
}
