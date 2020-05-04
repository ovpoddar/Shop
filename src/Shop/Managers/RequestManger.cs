using Shop.Builders;
using Shop.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shop.Manager
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
        

        public async Task<string> PatchRequest<T>(string uri, T entity)
        {
            var request = _builder.BuildRequest(HttpMethod.Patch, uri, JsonConvert.SerializeObject(entity));
            var responce = await _sent.SendAsync(request);
            responce.EnsureSuccessStatusCode();
            return await responce.Content.ReadAsStringAsync();
        }
    }
}
