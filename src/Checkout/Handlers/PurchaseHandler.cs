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

        public async Task<OverallResult<List<Results<ItemModel>>>> MakePurchaseCallAsync(PurchaseModel model) =>
            JsonConvert.DeserializeObject<OverallResult<List<Results<ItemModel>>>>(await _requestManger.PostRequest($"{WebSitesUrls.EndPoient}api/Products/Purchase", model));
    }
}
