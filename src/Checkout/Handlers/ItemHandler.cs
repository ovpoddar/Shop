using Checkout.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.Handlers
{
    public class ItemHandler : IItemHandler
    {
        public List<ItemModel> List { get; set; } = new List<ItemModel> { new ItemModel { Id = 0, Name = "", Brand = "", Price = 0, Quantity = 0, TotalPrice = 0 } };

        public void addItem(Product product, int quantity) =>
            List.Add(new ItemModel
            {
                Id = List.Count,
                Name = product.ProductName,
                Brand = product.Brands.BrandName,
                Price = product.Price,
                Quantity = quantity,
                TotalPrice = (double)product.Price * quantity
            });

        public ItemModel GetItem(string name) =>
            List.Find(e => e.Name == name);

        public void RemoveItem(int id) =>
            List.Remove(List.Find(e => e.Id == id));

        public void RemoveItem(Product old) =>
            List.Remove(List.Where(e => e.Name.ToUpper() == old.ProductName.ToUpper() && e.Price == old.Price).FirstOrDefault());

        public double Total()
        {
            double total = 0;
            foreach (var item in List)
            {
                total += item.TotalPrice;
            }
            return total;
        }
    }
}
