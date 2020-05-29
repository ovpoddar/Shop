using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Linq;

namespace Shop.Managers
{
    public class ItemManager : IItemManager
    {
        private readonly IProductHandler _product;
        private readonly IItemHandler<ItemModel> _item;

        public ItemManager(IProductHandler product, IItemHandler<ItemModel> item)
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

        public ItemModel GetItem(string name) =>
            _item.GetItem(name);

        public ItemViewModel Model(string name) => 
            new ItemViewModel
            {
                Name = _product.GetProduct(name).ProductName,
                Quantity = _item.GetItem(name).Quantity
            };

        public void remove(int id)
        {
            _item.List.Remove(_item.List.Find(e => e.Id == id));
        }
    }
}
