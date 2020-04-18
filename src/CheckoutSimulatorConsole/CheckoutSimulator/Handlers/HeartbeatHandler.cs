using System;
using System.Threading.Tasks;

namespace CheckoutSimulator.Handlers
{
    public class HeartbeatHandler : IHeartbeatHandler
    {
        private readonly IRequestHandler _requestHandler;

        public HeartbeatHandler(IRequestHandler requestHandler) =>
            _requestHandler = requestHandler;

        public async Task<bool> CheckForHeartbeat(string uri)
        {
            //var returnedUser = initialRequest
            //    ? JsonConvert.DeserializeObject<User>(
            //        await _requestHandler.PostRequest($"{_configuration["IdentityServerUri"]}/api/User", token, user))

            var count = 0;
            var test = true;

            do
            {
                var response = await _requestHandler.GetRequestResponse($"{uri}Heartbeat");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"No heartbeat for {uri}Heartbeat. Is the application running?");
                    continue;
                }

                count++;
                Console.WriteLine($"Heartbeat for {uri} found. Count: {count}");
                test = count <= 10;

            } while (test);

            Console.WriteLine($"Criteria met to continue simulation");

            return true;
        }
    }
}