using Mi4com.UserManagement.Api.Services;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CheckoutSimulator.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;
        
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(2));

            var response = new HttpResponseMessage();

            await retryPolicy.ExecuteAsync(async () =>
            {
                response = await httpClient.SendAsync(httpRequestMessage);
            });

            return response;
        }
    }
}