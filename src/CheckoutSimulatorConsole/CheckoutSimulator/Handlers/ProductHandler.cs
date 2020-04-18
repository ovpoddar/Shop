using CheckoutSimulator.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CheckoutSimulator.Handlers
{
    public class ProductHandler : IProductHandler
    {
        private readonly IRequestHandler _requestHandler;

        public ProductHandler(IRequestHandler requestHandler) => 
            _requestHandler = requestHandler;

        public async Task PostExample(string uri)
        {
            Console.WriteLine("Simulate a POST request");
            Console.WriteLine("Build Correct uri - Base Uri should be in config and never hard coded");

            var apiUri = $"{uri}Products/Brand";

            Console.WriteLine("Create new Brand Object");

            var brand = new Brand { BrandName = Guid.NewGuid().ToString() };

            Console.WriteLine("The code will use Newtonsoft Json to Serialize the Object into Json");
            Console.WriteLine("The Json will be added as content to the post request httpRequestMessage");
            Console.WriteLine("The httpRequestMessage POST is sent using the httpClient send Method");
           
            var json = await _requestHandler.PostRequest(apiUri, brand);

            Console.WriteLine("The code will check for a 200 success messsage if this exists it will retrieve the contents of the return  message");
            Console.WriteLine("The return message should be json and can in this case be converted back to a Brand object as we know this is what is returned");

            var returnBrand = JsonConvert.DeserializeObject<Brand>(json);

            if (returnBrand.BrandName == brand.BrandName) Console.WriteLine(JsonConvert.SerializeObject(returnBrand));
        }

        public async Task GetExample(string uri)
        {
            Console.WriteLine("Simulate a GET request");
            Console.WriteLine("Build Correct uri - Base Uri should be in config and never hard coded");
            Console.WriteLine("This example will retrieve product id 1");

            var productId = 1;
            var apiUri = $"{uri}Products/{productId}";

            Console.WriteLine("The httpRequestMessage GET is sent using the httpClient send Method");
            try
            {
                var json = await _requestHandler.GetRequest(apiUri);

                Console.WriteLine("The code will check for a 200 success message if this exists it will retrieve the contents of the return  message");
                Console.WriteLine("The return message should be json and can in this case be converted back to a Product object as we know this is what is returned");

                var returnProduct = JsonConvert.DeserializeObject<SaleProduct>(json);

                Console.WriteLine(JsonConvert.SerializeObject(returnProduct));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public async Task PatchExample(string uri, bool throwError)
        {
            Console.WriteLine("Simulate a PATCH request");
            Console.WriteLine("Build Correct uri - Base Uri should be in config and never hard coded");


            var apiUri = $"{uri}Products/StockLevel";

            if(throwError)
                apiUri = $"{uri}Products/StockLevels";

            Console.WriteLine("Create new SaleProduct Object");

            var saleProduct = new SaleProduct {ProductId = 1, SaleQuantity = 5};

            Console.WriteLine("The httpRequestMessage PATCH is sent using the httpClient send Method");

            var responseMessage = await _requestHandler.PatchRequest(apiUri, saleProduct);

            Console.WriteLine("The code will check for a 200 success message if this exists it will retrieve the contents of the return  message");

            if (!responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("An Error has occurred");
                Console.WriteLine($"{responseMessage.StatusCode}");
                return;
            }
            
            
            Console.WriteLine("The return message should be json and can in this case be converted back to a SaleProduct object as we know this is what is returned");

            var json = await responseMessage.Content.ReadAsStringAsync();

            var returnSaleProduct = JsonConvert.DeserializeObject<SaleProduct>(json);

            Console.WriteLine(JsonConvert.SerializeObject(returnSaleProduct));
        }
    }
}
