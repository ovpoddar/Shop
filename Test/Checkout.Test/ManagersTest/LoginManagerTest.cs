using Checkout.Managers;
using FluentAssertions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Shop.Models;
using Shop.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Test.ManagersTest
{
    public class LoginManagerTest
    {
        private readonly Mock<IHttpContextAccessor> _ContextMock;
        private readonly Mock<IDataProtectionProvider> _DataMock;
        private readonly Mock<IConfiguration> _ConfigMock;
        private readonly Mock<IRequestManger> _RequestMock;
        private readonly Mock<IDataProtector> _dataProtector;
        private readonly loginManager _loginManager;

        public LoginManagerTest()
        {
            _ConfigMock = new Mock<IConfiguration>();
            _ContextMock = new Mock<IHttpContextAccessor>();
            _DataMock = new Mock<IDataProtectionProvider>();
            _RequestMock = new Mock<IRequestManger>();
            _dataProtector = new Mock<IDataProtector>();
            _loginManager = new loginManager(_ContextMock.Object, _RequestMock.Object, _DataMock.Object, _ConfigMock.Object);
        }

        [Fact]
        public async Task LogMeInTestWithSuccessAsync()
        {
            var responce = new Results<CustomeSignInResult>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = new CustomeSignInResult()
                {
                    Errors = null,
                    IsLockedOut = false,
                    IsNotAllowed = true,
                    RequiresTwoFactor = false,
                    Succeeded = true,
                    Token = "it will be stored in browser cookie"
                },
                Success = true
            };
            _RequestMock.Setup(e => e.PostRequest(It.IsAny<string>(), It.IsAny<LogInViewModel>())).Returns(Task.FromResult(JsonConvert.SerializeObject(responce)));
            _ContextMock.Setup(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>())).Verifiable();
            _ConfigMock.SetupGet(e => e[It.IsAny<string>()]).Returns("it is a secreat key store in application.js file");
            _DataMock.Setup(e => e.CreateProtector(It.IsAny<string>())).Returns(_dataProtector.Object);

            var result = await _loginManager.LogMeIn(It.IsAny<LogInViewModel>());

            Assert.NotNull(result);
            _ContextMock.Verify(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.AtLeastOnce);
            new Status()
            {
                Error = new List<string>(),
                Success = true
            }.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task LogMeInTestWithUnsuccessAsync()
        {
            var responce = new Results<CustomeSignInResult>()
            {
                Exception = "another error",
                HttpStatusCode = HttpStatusCode.BadRequest,
                Result = new CustomeSignInResult()
                {
                    Errors = new List<IdentityError>()
                    {
                       new IdentityError()
                       {
                           Code = "200",
                           Description = "something"
                       },new IdentityError()
                       {
                           Code = "400",
                           Description = "something"
                       }
                    },
                    IsLockedOut = false,
                    IsNotAllowed = true,
                    RequiresTwoFactor = false,
                    Succeeded = false,
                    Token = "no token for this request"
                },
                Success = false
            };
            _RequestMock.Setup(e => e.PostRequest(It.IsAny<string>(), It.IsAny<LogInViewModel>())).Returns(Task.FromResult(JsonConvert.SerializeObject(responce)));
            _ContextMock.Setup(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>())).Verifiable();
            _ConfigMock.SetupGet(e => e[It.IsAny<string>()]).Returns("it is a secreat key store in application.js file");
            _DataMock.Setup(e => e.CreateProtector(It.IsAny<string>())).Returns(_dataProtector.Object);

            var result = await _loginManager.LogMeIn(It.IsAny<LogInViewModel>());

            Assert.NotNull(result);
            _ContextMock.Verify(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Never);
            result.Success.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void LogMeOut()
        {
            _ContextMock.Setup(e => e.HttpContext.Response.Cookies.Delete(It.IsAny<string>(), It.IsAny<CookieOptions>())).Verifiable();

            _loginManager.LogMeOutAsync();

            _ContextMock.Verify(e => e.HttpContext.Response.Cookies.Delete(It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.AtLeastOnce);
        }
    }
}
