using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using Shop.Helpers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class ValidatorHandlerTest
    {
        private readonly Mock<ICookieHandler> _cookie;
        private readonly Mock<ISignHandler> _signHandler;
        private readonly Mock<IProtectorHandler> _protectorHandler;
        private readonly Mock<IUserHandler> _userHandler;
        private readonly Mock<IUserHelper> _userHelper;
        private readonly ValidatorHandler _validatorHandler;
        public ValidatorHandlerTest()
        {
            _cookie = new Mock<ICookieHandler>();
            _signHandler = new Mock<ISignHandler>();
            _protectorHandler = new Mock<IProtectorHandler>();
            _userHandler = new Mock<IUserHandler>();
            _userHelper = new Mock<IUserHelper>();
            _validatorHandler = new ValidatorHandler(_cookie.Object, _signHandler.Object, _protectorHandler.Object, _userHandler.Object, _userHelper.Object);
        }

        [Fact]
        public void UserTest()
        {
            var emp = new Employer() { FirstName = "a", LastName = "p" };
            _userHelper
                .Setup(e => e.GetEmployerByUnicId(It.IsAny<string>()))
                .Returns(emp)
                .Verifiable();
            _cookie
                .Setup(e => e.Get(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            var result = _validatorHandler.User();
            result.Should().NotBeNull();
            _cookie
                .Verify(e => e.Get(It.IsAny<string>()), Times.Once);
            _userHelper
                .Verify(e => e.GetEmployerByUnicId(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void IsMemberNotpassTheBasictestTest()
        {
            var status1 = new LoginStatus()
            {
                Success = false,
                Error = It.IsAny<List<string>>(),
                Employer = null
            };
            _cookie
                .Setup(e => e.Get(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _signHandler
                .Setup(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(status1)
                .Verifiable();
            _protectorHandler
                .Setup(e => e.UnProtect(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _userHandler
                .Setup(e => e.GetUserName(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            var result = _validatorHandler.IsMember();
            result.Should().NotBeNull();
            _cookie
               .Verify(e => e.Get(It.IsAny<string>()), Times.AtLeast(2));
            _signHandler
                .Verify(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _protectorHandler
                .Verify(e => e.UnProtect(It.IsAny<string>()), Times.Never);
            _userHandler
                .Verify(e => e.GetUserName(It.IsAny<string>()), Times.Never);

        }

        [Fact]
        public void IsMemberPassTheBasictestTest()
        {
            var status1 = new LoginStatus()
            {
                Success = false,
                Error = It.IsAny<List<string>>(),
                Employer = null
            };
            _cookie
                .Setup(e => e.Get(It.IsAny<string>()))
                .Returns("cc")
                .Verifiable();
            _signHandler
                .Setup(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(status1)
                .Verifiable();
            _protectorHandler
                .Setup(e => e.UnProtect(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _userHandler
                .Setup(e => e.GetUserName(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            var result = _validatorHandler.IsMember();
            result.Should().NotBeNull();
            _cookie
               .Verify(e => e.Get(It.IsAny<string>()), Times.AtLeast(2));
            _signHandler
                .Verify(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _protectorHandler
                .Verify(e => e.UnProtect(It.IsAny<string>()), Times.Once);
            _userHandler
                .Verify(e => e.GetUserName(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task IsMemberPassTheLoginTestTestAsync()
        {
            var status = new LoginStatus()
            {
                Success = true,
                Error = null,
                Employer = new Employer() { FirstName = "a", LastName = "p" }
            };
            _cookie
                .Setup(e => e.Get(It.IsAny<string>()))
                .Returns("cc")
                .Verifiable();
            _signHandler
                .Setup(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(status)
                .Verifiable();
            _protectorHandler
                .Setup(e => e.UnProtect(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _userHandler
                .Setup(e => e.GetUserName(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            var result = await _validatorHandler.IsMember();
            result.Should().BeTrue();
            _cookie
               .Verify(e => e.Get(It.IsAny<string>()), Times.AtLeast(2));
            _signHandler
                .Verify(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _protectorHandler
                .Verify(e => e.UnProtect(It.IsAny<string>()), Times.Once);
            _userHandler
                .Verify(e => e.GetUserName(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task IsMemberFailTheLoginTestTestAsync()
        {
            var status = new LoginStatus()
            {
                Success = false,
                Error = It.IsAny<List<string>>(),
                Employer = null
            };
            _cookie
                .Setup(e => e.Get(It.IsAny<string>()))
                .Returns("cc")
                .Verifiable();
            _signHandler
                .Setup(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(status)
                .Verifiable();
            _protectorHandler
                .Setup(e => e.UnProtect(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _userHandler
                .Setup(e => e.GetUserName(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            var result = await _validatorHandler.IsMember();
            result.Should().BeFalse();
            _cookie
               .Verify(e => e.Get(It.IsAny<string>()), Times.AtLeast(2));
            _signHandler
                .Verify(e => e.LogInAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _protectorHandler
                .Verify(e => e.UnProtect(It.IsAny<string>()), Times.Once);
            _userHandler
                .Verify(e => e.GetUserName(It.IsAny<string>()), Times.Once);

        }
    }
}
