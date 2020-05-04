using Newtonsoft.Json;
using Shop.Manager;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IRequestManger _request;

        public PaymentManager(IRequestManger request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(_request));
        }

        public ItemModel CreateModel(string id, string name, string brand, string quantity, string price, string totalPrice) => 
            new ItemModel
            {
                Id = int.Parse(id),
                Name = name,
                Brand = brand,
                Price = decimal.Parse(price),
                Quantity = int.Parse(quantity),
                TotalPrice = double.Parse(totalPrice)
            };

        public PaymentViewModel GetModel(List<ItemModel> items, decimal total) =>
             new PaymentViewModel
             {
                 Items = items,
                 Total = total
             };

        public async Task<bool> PurchaseCall(List<SaleProduct> products)
        {
            foreach(var product in products)
            {
                var responce = await _request.PatchRequest("api/Products/StockLevel", product);
                var obj = JsonConvert.DeserializeObject<Results<SaleProduct>>(responce);
                if (!obj.Success)
                    return false;
            }
            return true;
        }
    }
}
