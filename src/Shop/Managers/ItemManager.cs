using Checkout.Models;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Managers
{
    public class ItemManager : IItemManager
    {
        private readonly IProductHandler _product;
        private readonly IItemHandler _item;

        public ItemManager(IProductHandler product, IItemHandler item)
        {
            _product = product ?? throw new ArgumentNullException(nameof(_product));
            _item = item ?? throw new ArgumentNullException(nameof(_item));
        }
        public void add(ItemViewModel model)
        {
            var product = _product.GetProduct(model.Name);
            if (product == null)
                return;
            if (_item.List.Any(e => e.Name.ToLower() == model.Name.ToLower()))
                _item.RemoveItem(product);
            _item.addItem(product, model.Quantity);
        }

        public List<ItemModel> CreateItemModels(List<string> ids, List<string> names, List<string> brands, List<string> quantitys, List<string> prices, List<string> totalPrices)
        {
            var items = new List<ItemModel>();
            for (var i = 0; i < ids.Count; i++)
            {
                items.Add(new ItemModel()
                {
                    Id = int.Parse(ids[i]),
                    Name = names[i],
                    Brand = brands[i],
                    Price = decimal.Parse(prices[i]),
                    Quantity = int.Parse(quantitys[i]),
                    TotalPrice = double.Parse(totalPrices[i])
                });
            }
            return items;
        }

        public ItemModel GetItem(string name) =>
            _item.GetItem(name);

        public ItemViewModel Model(string name) =>
            new ItemViewModel
            {
                Name = _product.GetProduct(name).ProductName,
                Quantity = _item.GetItem(name).Quantity
            };

        public void remove(int id) =>
            _item.RemoveItem(id);
    }
}
