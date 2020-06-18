using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Services
{
    public interface ISentRequestService
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
