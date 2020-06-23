using Checkout.Handlers;
using Checkout.ViewModels;
using DataAccess.Entities;
using Newtonsoft.Json;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public class ItemManager : IItemManager
    {
        private readonly IItemHandler<ItemModel> _item;
        private readonly IRequestManger _requestManger;

        public ItemManager(IItemHandler<ItemModel> item, IRequestManger requestManger)
        {
            _item = item ?? throw new ArgumentNullException(nameof(_item));
            _requestManger = requestManger ?? throw new ArgumentNullException(nameof(_requestManger));
        }
        public async Task Add(ItemViewModel model)
        {
            var product = JsonConvert.DeserializeObject<Results<ItemModel>>(await _requestManger.GetRequest($"{WebSitesUrls.EndPoient}api/AddProduct?productName={model.Name}&Quantity={model.Quantity}"));
            if (product.Success == false)
                return;
            if (_item.List.Any(e => e.Name.ToLower() == product.Result.Name.ToLower()))
                _item.RemoveItem(product.Result);
            _item.AddItem(product);
        }

        public ItemModel GetItem(string name) =>
            _item.GetItem(name);

        public async Task<ItemViewModel> Model(string name) => new ItemViewModel()
        {
            Name = JsonConvert.DeserializeObject<Results<Product>>(await _requestManger.GetRequest($"{WebSitesUrls.EndPoient}api/Products/GetProduct?Name={name}")).Result.ProductName,
            Quantity = _item.GetItem(name).Quantity
        };

        public void remove(int id) =>
            _item.RemoveItem(_item.GetItem(id));
    }
}
