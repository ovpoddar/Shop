using System.Net.Http;
using System.Threading.Tasks;

namespace Shop.Services
{
    public interface ISentRequestService
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
