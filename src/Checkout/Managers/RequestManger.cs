using Checkout.Builders;
using Checkout.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public class RequestManger : IRequestManger
    {
        private readonly IRequestBuilder _builder;
        private readonly ISentRequestService _sent;

        public RequestManger(IRequestBuilder builder, ISentRequestService sent)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(_builder));
            _sent = sent ?? throw new ArgumentNullException(nameof(_sent));
        }

        public async Task<string> GetRequest(string uri, string token) =>
            await (await _sent.SendAsync(_builder.BuildRequest(HttpMethod.Get, uri, token))).Content.ReadAsStringAsync();

        public async Task<string> PatchRequest<T>(string uri, T entity, string token) =>
            await (await _sent.SendAsync(_builder.BuildRequest(HttpMethod.Patch, uri, token, JsonConvert.SerializeObject(entity)))).Content.ReadAsStringAsync();

        public async Task<string> PostRequest<T>(string uri, T entity) =>
            await (await _sent.SendAsync(_builder.BuildRequest(HttpMethod.Post, uri, JsonConvert.SerializeObject(entity)))).Content.ReadAsStringAsync();
    }
}
