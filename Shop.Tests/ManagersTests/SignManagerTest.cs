using DataAccess.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.Models;
using Shop.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class SignManagerTest
    {
        private readonly Mock<ISignHandler> _signHandler;
        private readonly Mock<IProtectorHandler> _protectorHandler;
        private readonly Mock<ICookieHandler> _cookieHandler;
        private readonly SignManager _signInManager;
        public SignManagerTest()
        {
            _signHandler = new Mock<ISignHandler>();
            _protectorHandler = new Mock<IProtectorHandler>();
            _cookieHandler = new Mock<ICookieHandler>();
            _signInManager = new SignManager(_cookieHandler.Object, _signHandler.Object, _protectorHandler.Object);
        }
        [Fact]
        public async Task LoginAsyncMethodTestAsync()
        {
            var user = new LogInViewModel()
            {
                UserName = "a",
                Password = "p"
            };
            var password = "p";
            _signHandler
                .Setup(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new LoginStatus() { Employer = new Employer() { FirstName = "a", LastName = "p" }, Error = null, Success = true })
                .Verifiable();
            _protectorHandler
                .Setup(e => e.HashMd5(It.IsAny<string>()))
                .Returns("hashpassword")
                .Verifiable();
            _protectorHandler
                .Setup(e => e.Protect(It.IsAny<string>()))
                .Returns("another recoraverable enctipted string")
                .Verifiable();
            _cookieHandler
                .Setup(e => e.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()))
                .Verifiable();

            var result = await _signInManager
                .LoginAsyncMethod(user, password);
            result.Success.Should().BeTrue();
            result.Employer.Should().BeEquivalentTo(new Employer() { FirstName = "a", LastName = "p" });
            result.Error.Should().BeNullOrEmpty();
            _signHandler
                .Verify(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            _protectorHandler
                .Verify(e => e.HashMd5(It.IsAny<string>()), Times.AtLeastOnce);
            _protectorHandler
                .Verify(e => e.Protect(It.IsAny<string>()), Times.AtLeastOnce);
            _cookieHandler
                .Verify(e => e.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Exactly(2));
        }

        [Fact]
        public void LogOutTest()
        {
            _cookieHandler
                .Setup(e => e.Delete(It.IsAny<string>()))
                .Verifiable();

            _signInManager.LogOut();

            _cookieHandler.Verify(e => e.Delete(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
