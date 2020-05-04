using ApiWorker.Builders;
using ApiWorker.Manager;
using ApiWorker.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ApiWorker
{
    class Program
    {
        static void Main() =>
            MainAsync().Wait();

        static async Task MainAsync()
        {

            var serviceCollection = new ServiceCollection()
                .AddHttpClient()
                .AddTransient<IRunApplication, RunApplication>()
                .AddTransient<IRequestBuilder, RequestBuilder>()
                .AddTransient<IRequestManger, RequestManger>()
                .AddTransient<ISentRequestService, SentRequestService>()
                .BuildServiceProvider();

            var simulationManager = serviceCollection.GetService<IRunApplication>();
            await simulationManager.RunSimulation();
        }
    }
}
