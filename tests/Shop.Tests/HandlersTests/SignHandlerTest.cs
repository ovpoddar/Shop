using Moq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Handlers.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class SignHandlerTest
    {
        private readonly Mock<IUserHandler> _userHandler;
        private readonly SignHandler _signHandler;
        public SignHandlerTest()
        {
            _userHandler = new Mock<IUserHandler>();
            _signHandler = new SignHandler(_userHandler.Object);
        }

        [Fact]
        public async Task LogInAsync_test_true()
        {
            _userHandler
                .Setup(e => e.GetUserName(It.IsAny<string>()))
                .Returns(Get().UserName);

            _userHandler
                .Setup(e => e.FindEmployerAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(Get()));

            var result = await _signHandler.LogInAsync("ayan", "asdkgvasdvki");

            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.NotNull(result.Employer);
        }

        [Fact]
        public async Task LogInAsync_test_false_2()
        {
            _userHandler
                .Setup(e => e.GetUserName(It.IsAny<string>()))
                .Returns(Get().UserName);

            _userHandler
                .Setup(e => e.FindEmployerAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<Employer>()));

            var result = await _signHandler.LogInAsync("amar", "asdkgvasdvki");

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task LogInAsync_test_false_1()
        {
            _userHandler
                .Setup(e => e.GetUserName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());

            _userHandler
                .Setup(e => e.FindEmployerAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<Employer>()));

            var result = await _signHandler.LogInAsync("amar", "asdkgvasdvki");

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        private Employer Get() => new Employer
        {
            CapatalisedEmail = It.IsAny<string>(),
            CapatalisedFullName = It.IsAny<string>(),
            CapatalisedUserName = It.IsAny<string>(),
            City = It.IsAny<string>(),
            Email = It.IsAny<string>(),
            FirstName = It.IsAny<string>(),
            Gender = It.IsAny<string>(),
            Id = It.IsAny<int>(),
            LastName = It.IsAny<string>(),
            MobileNo = 6565613056,
            Password = "asdkgvasdvki",
            UnicId = It.IsAny<string>(),
            UserName = "ayan"
        };
    }
}
