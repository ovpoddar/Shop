using CheckoutSimulator.Handlers;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CheckoutSimulator.Managers
{
    public class SimulationManager : ISimulationManager
    {
        private readonly IHeartbeatHandler _heartbeatHandler;

        public SimulationManager(IHeartbeatHandler heartbeatHandler)
        {
            _heartbeatHandler = heartbeatHandler;
        }

        public async Task RunSimulation(IConfiguration configuration)
        {
            var uri = configuration["ShopApiUri"];

            var heartbeat = await _heartbeatHandler.CheckForHeartbeat(uri);
        }
    }
}
