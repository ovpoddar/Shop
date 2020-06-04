using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Moq;
using Shop.Handlers;
using Shop.Helpers.Interfaces;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class ProtectorHandlerTests
    {
        private readonly Mock<IDataProtectionProvider> _dataProtectionProvider;
        private readonly Mock<IProtectionHelper> _protectionHelper;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IDataProtector> _dataProtector;
        private readonly ProtectorHandler _protectorHandler;

        public ProtectorHandlerTests()
        {
            _dataProtectionProvider = new Mock<IDataProtectionProvider>();
            _protectionHelper = new Mock<IProtectionHelper>();
            _configuration = new Mock<IConfiguration>();
            _dataProtector = new Mock<IDataProtector>();
            _protectorHandler = new ProtectorHandler(
                _dataProtectionProvider.Object,
                _protectionHelper.Object,
                _configuration.Object);
        }

        [Fact]
        public void ProtectTest()
        {
            _configuration
                .SetupGet(config => config[It.Is<string>(s => s == "dataprotector")])
                .Returns("string");
            _dataProtectionProvider.Setup(method => method.CreateProtector(It.IsAny<string>()))
                .Returns(_dataProtector.Object);

            _protectionHelper.Setup(method => method.BuildToken(It.IsAny<string>())).Returns("AnotherToken");
            _protectorHandler.Protect("test");

            _configuration.Verify(config => config[It.Is<string>(s => s == "dataprotector")], Times.Once);
            _dataProtectionProvider.Verify(method => method.CreateProtector(It.IsAny<string>()), Times.Once);

            _protectionHelper.Verify(method => method.BuildToken(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public void UnProtectTest()
        {
            _configuration.SetupGet(config => config[It.Is<string>(s => s == "dataprotector")])
                .Returns("asvlavsadpoasdnsavasdhasdhapclasp");
            _dataProtectionProvider.Setup(method => method.CreateProtector(It.IsAny<string>()))
                .Returns(_dataProtector.Object);

            var output = _protectorHandler.UnProtect("string");

            _configuration.Verify(config => config[It.Is<string>(s => s == "dataprotector")], Times.Once);
            _dataProtectionProvider.Verify(method => method.CreateProtector(It.IsAny<string>()), Times.Once);
        }
    }
}
