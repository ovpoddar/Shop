using System.Net.Http;
using System.Text;

namespace Shop.Builders
{
    public class RequestBuilder : IRequestBuilder
    {
        public HttpRequestMessage BuildRequest(HttpMethod method, string url) => new HttpRequestMessage(method, url);

        public HttpRequestMessage BuildRequest(HttpMethod method, string url, string content) => new HttpRequestMessage(method, url)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
    }
}
