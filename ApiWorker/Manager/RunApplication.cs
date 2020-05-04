using CheckoutSimulator.Models;
using System;
using System.Threading.Tasks;

namespace ApiWorker.Manager
{
    public class RunApplication : IRunApplication
    {
        private readonly IRequestManger _manger;

        public RunApplication(IRequestManger manger)
        {
            _manger = manger ?? throw new ArgumentNullException(nameof(_manger));
        }
        public async Task RunSimulation()
        {
            var m = new SaleProduct()
            {
                BarCode = "0000010101010255",
                Brand = "Coca Cola",
                BrandId = 1,
                CategoriesId = 1,
                Category = "Drinks",
                OrderLevel = 2,
                Price = (decimal)0.75,
                ProductId = 3,
                ProductName = "Cola",
                SaleQuantity = 2,
            };
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("running patch");
            var x = await _manger.PatchRequest("http://localhost:59616/api/Products/StockLevel", m);
            Console.WriteLine(x);
            Console.WriteLine("stop patch");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(DateTime.Now);
            Console.ReadLine();
        }
    }
}
