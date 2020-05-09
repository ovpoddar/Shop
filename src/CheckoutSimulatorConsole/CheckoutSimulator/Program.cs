using CheckoutSimulator.Builders;
using CheckoutSimulator.Handlers;
using CheckoutSimulator.Managers;
using CheckoutSimulator.Services;
using Mi4com.UserManagement.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CheckoutSimulator
{
    class Program
    {
        static void Main(string[] args) =>
            MainAsync(args).Wait();

        static async Task MainAsync(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddHttpClient()
                .AddTransient<ISimulationManager, SimulationManager>()
                .AddTransient<IHeartbeatHandler, HeartbeatHandler>()
                .AddTransient<IRequestHandler, RequestHandler>()
                .AddTransient<IProductHandler, ProductHandler>()
                .AddTransient<IRequestBuilder, RequestBuilder>()
                .AddTransient<IHttpService, HttpService>()
                .BuildServiceProvider();

            var simulationManager = serviceCollection.GetService<ISimulationManager>();
            await simulationManager.RunSimulation(configuration);
        }
    }
}
