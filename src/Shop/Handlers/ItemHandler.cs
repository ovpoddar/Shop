using Shop.Entities;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using Shop.Handlers.Interfaces;

namespace Shop.Handlers
{
    public class ItemHandler : IItemHandler<ItemModel>
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

        public void RemoveItem(Product old) => 
            List.Remove(List.Where(e => e.Name.ToUpper() == old.ProductName.ToUpper() && e.Price == old.Price).FirstOrDefault());
    }
}
