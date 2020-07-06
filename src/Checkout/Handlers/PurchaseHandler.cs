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
            JsonConvert.DeserializeObject<OverallResult<List<Results<ItemModel>>>>(await _requestManger.PostRequest($"{WebSitesUrls.EndPoient}api/Products/Purchase",entity: model, token: "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJBbWFyIFBvZGRhciIsImVtYWlsIjoiYW1hcnBvZGRlcjBAZ21haWwuY29tIiwiZ2VuZGVyIjoiTWFsZSIsImV4cCI6MTU5NDEwMTc1OCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFtYXJwb2RkYXIiLCJpYXQiOiI0YjgwMjA4MC1kNGVmLTQ2YWYtYjc4NS0yZjAyMTQwZWNlYzAiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU5NjE2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzUwLyJ9.GBCtVTgO6QxuaS__JTIjFGwHT3C7pychjwhOvapt7R1SGV5uQazHCek-NAQj0-5tYViv2eh4tDIP9fY5hXhwQw"));
    }
}
