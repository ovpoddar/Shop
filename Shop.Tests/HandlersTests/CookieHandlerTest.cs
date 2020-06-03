using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Shop.Handlers;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class CookieHandlerTest
    {
        public readonly Mock<IHttpContextAccessor> _mock;
        private readonly CookieHandler _cookieHandler;
        public CookieHandlerTest()
        {
            _mock = new Mock<IHttpContextAccessor>();
            _cookieHandler = new CookieHandler(_mock.Object);
        }
        [Fact]
        public void Create_Check()
        {
            _mock.Setup(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));
            _cookieHandler.Create(It.IsAny<string>(), It.IsAny<string>());
            _mock.Verify(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public void Delete_Check()
        {
            _mock.Setup(e => e.HttpContext.Response.Cookies.Delete(It.IsAny<string>()));
            _cookieHandler.Delete(It.IsAny<string>());
            _mock.Verify(e => e.HttpContext.Response.Cookies.Delete(It.IsAny<string>()));
        }

        [Fact]
        public void Create_Check_with_outher_parameter()
        {
            _mock.Setup(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()));
            _cookieHandler.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>());
            _mock.Verify(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()));
        }

        [Fact]
        public void GetTest()
        {
            _mock
                .Setup(e => e.HttpContext.Request.Cookies[It.IsAny<string>()])
                .Returns("something");

            var output = _cookieHandler.Get("xx");
            output.Should().NotBeNullOrEmpty();
        }
    }
}
