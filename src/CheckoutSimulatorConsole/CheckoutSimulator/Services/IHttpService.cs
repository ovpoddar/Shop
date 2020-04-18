using System.Net.Http;
using System.Threading.Tasks;

namespace Mi4com.UserManagement.Api.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);
    }
}