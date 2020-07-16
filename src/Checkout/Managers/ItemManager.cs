using Checkout.Handlers;
using Checkout.ViewModels;
using DataAccess.Entities;
using Newtonsoft.Json;
using Shop.Models;
using System;
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
        public async Task<string> Add(ItemViewModel model, string token)
        {
            var product = JsonConvert.DeserializeObject<Results<ItemModel>>(await _requestManger.GetRequest($"{WebSitesUrls.EndPoient}api/AddProduct?productName={model.Name}&Quantity={model.Quantity}", token));
            if (product.Success == false)
                return "Request Fail";
            if (product.Result == null)
                return product.Exception;
            if (_item.List.Any(e => e.Name.ToLower() == product.Result.Name.ToLower()))
                _item.RemoveItem(product.Result);
            _item.AddItem(product);
            return null;
        }

        public ItemModel GetItem(string name) =>
            _item.GetItem(name);

        public async Task<ItemViewModel> Model(string name, string token) => new ItemViewModel()
        {
            Name = JsonConvert.DeserializeObject<Results<Product>>(await _requestManger.GetRequest($"{WebSitesUrls.EndPoient}api/Products/GetProduct?Name={name}", token)).Result.ProductName,
            Quantity = _item.GetItem(name).Quantity
        };

        public void remove(int id) =>
            _item.RemoveItem(_item.GetItem(id));
    }
}
