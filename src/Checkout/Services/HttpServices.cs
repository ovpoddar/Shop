using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Services
{
    public class HttpServices : IHttpServices
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpServices(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(_httpClientFactory));

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) =>
            await _httpClientFactory.CreateClient().SendAsync(request);
    }
}
