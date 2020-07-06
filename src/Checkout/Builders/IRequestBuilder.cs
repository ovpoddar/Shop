using System.Net.Http;
using System.Security.Policy;

namespace Checkout.Builders
{
    public interface IRequestBuilder
    {
        HttpRequestMessage BuildRequest(HttpMethod method, string url, string content);
        HttpRequestMessage BuildRequest(HttpMethod method, Url url, string token);
        HttpRequestMessage BuildRequest(HttpMethod method, string url, string token, string content);
    }
}
