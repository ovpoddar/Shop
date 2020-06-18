using DataAccess.Entities;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
