using System.Net.Http;

namespace ApiWorker.Builders
{
    public interface IRequestBuilder
    {
        HttpRequestMessage BuildRequest(HttpMethod method, string url);
        HttpRequestMessage BuildRequest(HttpMethod method, string url, string content);
    }
}
