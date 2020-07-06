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
            JsonConvert.DeserializeObject<OverallResult<List<Results<ItemModel>>>>(await _requestManger.PostRequest($"{WebSitesUrls.EndPoient}api/Products/Purchase", model, "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWFyIFBvZGRhciIsImVtYWlsIjoiYW1hcnBvZGRlcjBAZ21haWwuY29tIiwiZ2VuZGVyIjoiTWFsZSIsImV4cCI6MTU5NDA5MzA3NCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFtYXJwb2RkYXIiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU5NjE2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzUwLyJ9.cn3mkaTx9Yueat9RVOyJx7MBQasyXP3xhwmXaeVR9b9AW-mnPr3Cmj6hu2ESQ8W6zI5wO_MN5R6QpombZKimsg"));
    }
}
