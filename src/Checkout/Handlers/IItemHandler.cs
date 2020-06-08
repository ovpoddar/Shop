using Checkout.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Handlers
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
