using System.Net.Http;
using System.Threading.Tasks;

namespace CheckoutSimulator.Handlers
{
    public interface IRequestHandler
    {
        Task<string> GetRequest(string uri);
        Task<HttpResponseMessage> GetRequestResponse(string uri);
        Task<string> InsertRequest<T>(string uri, T entity, HttpMethod httpMethod);
        Task<string> PostRequest<T>(string uri, T entity);
        Task<string> DeleteRequest(string uri);
        Task<string> DeleteRequest<T>(string uri, T entity);
        Task<string> PatchRequest(string uri);
        Task<HttpResponseMessage> PatchRequest<T>(string uri, T entity);
    }
}