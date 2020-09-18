using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Services
{
    public interface IHttpServices
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
