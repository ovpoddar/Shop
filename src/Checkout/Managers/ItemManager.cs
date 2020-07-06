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
            var product = JsonConvert.DeserializeObject<Results<ItemModel>>(await _requestManger.GetRequest($"{WebSitesUrls.EndPoient}api/AddProduct?productName={model.Name}&Quantity={model.Quantity}", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWFyIFBvZGRhciIsImVtYWlsIjoiYW1hcnBvZGRlcjBAZ21haWwuY29tIiwiZ2VuZGVyIjoiTWFsZSIsImV4cCI6MTU5NDA5MzA3NCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFtYXJwb2RkYXIiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU5NjE2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzUwLyJ9.cn3mkaTx9Yueat9RVOyJx7MBQasyXP3xhwmXaeVR9b9AW-mnPr3Cmj6hu2ESQ8W6zI5wO_MN5R6QpombZKimsg"));
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
            Name = JsonConvert.DeserializeObject<Results<Product>>(await _requestManger.GetRequest($"{WebSitesUrls.EndPoient}api/Products/GetProduct?Name={name}", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWFyIFBvZGRhciIsImVtYWlsIjoiYW1hcnBvZGRlcjBAZ21haWwuY29tIiwiZ2VuZGVyIjoiTWFsZSIsImV4cCI6MTU5NDA5MzA3NCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFtYXJwb2RkYXIiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU5NjE2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzUwLyJ9.cn3mkaTx9Yueat9RVOyJx7MBQasyXP3xhwmXaeVR9b9AW-mnPr3Cmj6hu2ESQ8W6zI5wO_MN5R6QpombZKimsg")).Result.ProductName,
            Quantity = _item.GetItem(name).Quantity
        };

        public void remove(int id) =>
            _item.RemoveItem(_item.GetItem(id));
    }
}
