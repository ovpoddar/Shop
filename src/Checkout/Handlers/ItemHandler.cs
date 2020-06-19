using DataAccess.Entities;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Handlers
{
    public class ItemHandler : IItemHandler<ItemModel>
    {
        public List<ItemModel> List { get; set; } = new List<ItemModel>();

        public void AddItem(Results<ItemModel> model) =>
            List.Add(model.Result);

        public ItemModel GetItem(string name) =>
            List.Find(e => e.Name == name);

        public ItemModel GetItem(int id) =>
            List.Find(e => e.Id == id);

        public void RemoveItem(ItemModel model) =>
            List.Remove(List.Where(e => e.Name.ToLower() == model.Name.ToLower() && e.Price == model.Price).FirstOrDefault());

        public decimal Total()
        {
            decimal total = 0;
            foreach(var item in List)
            {
                total += (decimal)item.TotalPrice;
            };
            return total;
        }
    }
}
