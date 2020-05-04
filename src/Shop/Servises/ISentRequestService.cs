using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services
{
    public interface ISentRequestService
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
