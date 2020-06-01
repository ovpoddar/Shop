using Newtonsoft.Json;
using Shop.Builders;
using Shop.Managers.Interfaces;
using Shop.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shop.Managers
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


        public async Task<string> PatchRequest<T>(string uri, T entity) =>
            await (await _sent.SendAsync(_builder.BuildRequest(HttpMethod.Patch, uri, JsonConvert.SerializeObject(entity)))).Content.ReadAsStringAsync();
    }
}
