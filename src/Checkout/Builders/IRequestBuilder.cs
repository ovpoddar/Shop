using System.Net.Http;

namespace Checkout.Builders
{
    public interface IRequestBuilder
    {
        HttpRequestMessage BuildRequest(HttpMethod method, string url, string token);
        HttpRequestMessage BuildRequest(HttpMethod method, string url, string token, string content);
    }
}
