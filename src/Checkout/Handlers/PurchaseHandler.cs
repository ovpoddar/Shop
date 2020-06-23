using Checkout.Managers;
using Newtonsoft.Json;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Handlers
{
    public class PurchaseHandler : IPurchaseHandler
    {
        private readonly IRequestManger _requestManger;

        public PurchaseHandler(IRequestManger requestManger)
        {
            _requestManger = requestManger ?? throw new ArgumentNullException(nameof(_requestManger));
        }

        public async Task<bool> MakePurchaseCallAsync(PurchaseModel model)
        {
            if (!JsonConvert.DeserializeObject<OverallResult<List<ItemModel>>>(await _requestManger.PostRequest("http://localhost:59616/api/Products/Purchase", model)).Success)
                return false;
            return true;
        }
    }
}
