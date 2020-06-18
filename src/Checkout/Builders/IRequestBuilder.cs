using System.Net.Http;

namespace Checkout.Builders
{
    public interface IRequestBuilder
    {
        HttpRequestMessage BuildRequest(HttpMethod method, string url);
        HttpRequestMessage BuildRequest(HttpMethod method, string url, string content);
    }
}
