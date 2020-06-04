using Microsoft.Extensions.Configuration;
using Moq;
using Shop.Helpers;
using Xunit;

namespace Shop.Tests.HelpersTests
{
    public class ProtectionHelperTest
    {
        private readonly Mock<IConfiguration> _configuration;
        private readonly ProtectionHelper _protectionHelper;
        public ProtectionHelperTest()
        {
            _configuration = new Mock<IConfiguration>();
            _protectionHelper = new ProtectionHelper(_configuration.Object);
        }
        [Fact]
        public void BuildTokenTest()
        {
            _configuration
                .SetupGet(e => e.GetSection(It.IsAny<string>())[It.Is<string>(s => s == "expDate")])
                .Returns("22");

            _configuration
                .SetupGet(e => e.GetSection(It.IsAny<string>())[It.Is<string>(s => s == "secret")])
                .Returns("asvoiavnapiouhjvjnavojnhn");

            _protectionHelper.BuildToken("token");

            _configuration.Verify(e => e.GetSection(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
