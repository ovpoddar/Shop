using System.Threading.Tasks;

namespace Checkout.Managers
{
    public interface IRequestManger
    {
        Task<string> PatchRequest<T>(string uri, T entity, string token);
        Task<string> PostRequest<T>(string uri, T entity, string token);
        Task<string> GetRequest(string uri, string token);
    }
}
