using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Checkout.Builders
{
    public class RequestBuilder : IRequestBuilder
    {
        public HttpRequestMessage BuildRequest(HttpMethod method, string url, string token)
        {
            var message = new HttpRequestMessage(method, url);
            message.Headers.Add("Authorization", $"Bearer {token}");
            return message;
        }

        public HttpRequestMessage BuildRequest(HttpMethod method, string url, string content, string token)
        {
            var message = new HttpRequestMessage(method, url)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
            message.Headers.Add("Authorization", $"Bearer {token}");
            return message;
        }
    }
}
