using System.Threading.Tasks;

namespace CheckoutSimulator.Handlers
{
    public interface IHeartbeatHandler
    {
        Task<bool> CheckForHeartbeat(string uri);
    }
}