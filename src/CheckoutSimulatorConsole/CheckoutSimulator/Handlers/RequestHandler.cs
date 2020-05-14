using CheckoutSimulator.Builders;
using Mi4com.UserManagement.Api.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CheckoutSimulator.Handlers
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IHttpService _httpService;

        public RequestHandler(IRequestBuilder requestBuilder, IHttpService httpService)
        {
            _requestBuilder = requestBuilder;
            _httpService = httpService;
        }

        public async Task<string> GetRequest(string uri)
        {
            var request = _requestBuilder.BuildGetRequest(uri);
            var result = await _httpService.SendAsync(request);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> GetRequestResponse(string uri)
        {
            var request = _requestBuilder.BuildGetRequest(uri);
            return await _httpService.SendAsync(request);
        }

        public async Task<string> InsertRequest<T>(string uri, T entity, HttpMethod httpMethod)
        {
            var content = JsonConvert.SerializeObject(entity);
            var request = _requestBuilder.BuildInsertRequest(uri, content, httpMethod);
            var result = await _httpService.SendAsync(request);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PostRequest<T>(string uri, T entity)
        {
            var content = JsonConvert.SerializeObject(entity);
            var request = _requestBuilder.BuildInsertRequest(uri, content, HttpMethod.Post);
            var result = await _httpService.SendAsync(request);

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteRequest(string uri)
        {
            var request = _requestBuilder.BuildDeleteRequest(uri);
            var result = await _httpService.SendAsync(request);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteRequest<T>(string uri, T entity)
        {
            var content = JsonConvert.SerializeObject(entity);
            var request = _requestBuilder.BuildDeleteRequest(uri, content);
            var result = await _httpService.SendAsync(request);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> PatchRequest(string uri)
        {
            var request = _requestBuilder.BuildPatchRequest(uri);
            var result = await _httpService.SendAsync(request);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PatchRequest<T>(string uri, T entity)
        {
            var content = JsonConvert.SerializeObject(entity);
            var request = _requestBuilder.BuildInsertRequest(uri, content, HttpMethod.Patch);
            return await _httpService.SendAsync(request);
        }
    }
}