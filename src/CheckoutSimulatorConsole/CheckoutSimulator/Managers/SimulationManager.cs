using CheckoutSimulator.Handlers;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CheckoutSimulator.Managers
{
    public class SimulationManager : ISimulationManager
    {
        private readonly IHeartbeatHandler _heartbeatHandler;
        private readonly IProductHandler _productHandler;

        public SimulationManager(IHeartbeatHandler heartbeatHandler, IProductHandler productHandler)
        {
            _heartbeatHandler = heartbeatHandler;
            _productHandler = productHandler;
        }

        public async Task RunSimulation(IConfiguration configuration)
        {
            var uri = configuration["ShopApiUri"];

            Console.WriteLine($"Start : { DateTime.Now }");
            Console.WriteLine($"---------------------------------------------------------------------------");
            Console.WriteLine();
            await _heartbeatHandler.CheckForHeartbeat(uri);
            Console.WriteLine();
            Console.WriteLine($"---------------------------------------------------------------------------");
            await _productHandler.PostExample(uri);
            Console.WriteLine();
            Console.WriteLine($"---------------------------------------------------------------------------");
            await _productHandler.GetExample(uri);
            Console.WriteLine();
            Console.WriteLine($"---------------------------------------------------------------------------");
            await _productHandler.PatchExample(uri, false);
            Console.WriteLine();
            Console.WriteLine($"---------------------------------------------------------------------------");
            await _productHandler.PatchExample(uri, true);
            Console.WriteLine();
            Console.WriteLine($"---------------------------------------------------------------------------");
            Console.WriteLine($"End : { DateTime.Now }");

            Console.ReadLine();
        }
    }
}