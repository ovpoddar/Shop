using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CheckoutSimulator.Managers
{
    public interface ISimulationManager
    {
        Task RunSimulation(IConfiguration configuration);
    }
}