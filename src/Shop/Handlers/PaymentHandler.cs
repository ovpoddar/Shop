using Newtonsoft.Json;
using Shop.Manager;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class PaymentHandler : IPaymentHandler
    {
        private readonly IRequestManger _request;

        public PaymentHandler(IRequestManger request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(_request));
        }
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
