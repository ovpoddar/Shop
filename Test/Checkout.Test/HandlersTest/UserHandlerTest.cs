using Checkout.Handlers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Checkout.Test.HandlersTest
{
    public class UserHandlerTest
    {
        private readonly Mock<IDataProtectionProvider> _Data;
        private readonly Mock<IHttpContextAccessor> _Context;
        private readonly Mock<IDataProtector> _dataProtector;
        private readonly Mock<IConfiguration> _Config;
        private readonly UserHandler _userHandler;

        public UserHandlerTest()
        {
            _Data = new Mock<IDataProtectionProvider>();
            _Config = new Mock<IConfiguration>();
            _Context = new Mock<IHttpContextAccessor>();
            _dataProtector = new Mock<IDataProtector>();
            _userHandler = new UserHandler(_Data.Object, _Config.Object, _Context.Object);
        }


        //did not check
    }
}
