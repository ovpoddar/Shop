using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shop.Controllers;
using Shop.Handlers;
using System.Web.Mvc;
using Xunit;
using static test_for_all.CookieHandlerTest;

namespace test_for_all
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
            _mock.Setup(_ => _.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));
            _cookieHandler.Create(It.IsAny<string>(), It.IsAny<string>());
            _mock.Verify(_ => _.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public void Delete_Check()
        {
            _mock.Setup(_ => _.HttpContext.Response.Cookies.Delete(It.IsAny<string>()));
            _cookieHandler.Delete(It.IsAny<string>());
            _mock.Verify(_ => _.HttpContext.Response.Cookies.Delete(It.IsAny<string>()));
        }

        [Fact]
        public void Create_Check_with_outher_parameter()
        {
            _mock.Setup(_ => _.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()));
            _cookieHandler.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>());
            _mock.Verify(_ => _.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()));
        }
        //no idea how to mock the rest of the code

    }
}
