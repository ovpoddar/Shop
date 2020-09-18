using Checkout.Managers;
using Newtonsoft.Json;
using Shop.Models;
using System;
using System.Collections.Generic;
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

        public async Task<OverallResult<List<Results<ItemModel>>>> MakePurchaseCallAsync(PurchaseModel model, string token) =>
            JsonConvert.DeserializeObject<OverallResult<List<Results<ItemModel>>>>(await _requestManger.PatchRequest($"{WebSitesUrls.EndPoient}api/Products/Purchase", entity: model, token: token));
    }
}
