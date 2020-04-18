using System.Net.Http;

namespace CheckoutSimulator.Builders
{
    public interface IRequestBuilder
    {
        HttpRequestMessage BuildGetRequest(string uri);
        HttpRequestMessage BuildInsertRequest(string uri, string content, HttpMethod httpMethod);
        HttpRequestMessage BuildDeleteRequest(string uri);
        HttpRequestMessage BuildDeleteRequest(string uri, string content);
        HttpRequestMessage BuildPatchRequest(string uri);
    }
}