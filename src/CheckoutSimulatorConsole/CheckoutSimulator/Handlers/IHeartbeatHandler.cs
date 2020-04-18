using System.Threading.Tasks;

namespace CheckoutSimulator.Handlers
{
    public interface IHeartbeatHandler
    {
        Task CheckForHeartbeat(string uri);
    }
}