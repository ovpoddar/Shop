using System.Net.Http;
using System.Text;

namespace CheckoutSimulator.Builders
{
    public class RequestBuilder : IRequestBuilder
    {
        public HttpRequestMessage BuildGetRequest(string uri) =>
            new HttpRequestMessage(HttpMethod.Get, uri);

        public HttpRequestMessage BuildInsertRequest(string uri, string content, HttpMethod httpMethod) =>
            new HttpRequestMessage(httpMethod, uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json"),
            };

        public HttpRequestMessage BuildDeleteRequest(string uri) =>
            new HttpRequestMessage(HttpMethod.Delete, uri);

        public HttpRequestMessage BuildDeleteRequest(string uri, string content) =>
            new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json"),
            };

        public HttpRequestMessage BuildPatchRequest(string uri) =>
            new HttpRequestMessage(HttpMethod.Patch, uri);
    }
}
