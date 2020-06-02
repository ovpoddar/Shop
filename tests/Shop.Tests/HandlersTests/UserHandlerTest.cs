using FluentAssertions;
using Moq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Helpers.Interfaces;
using Shop.Models;
using Shop.Repositories;
using Shop.ViewModels;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class UserHandlerTest
    {
        private readonly Mock<IGenericRepository<Employer>> _repository;
        private readonly Mock<IUserHelper> _userHelper;
        private readonly UserHandler _userHandler;
        public UserHandlerTest()
        {
            _repository = new Mock<IGenericRepository<Employer>>();
            _userHelper = new Mock<IUserHelper>();
            _userHandler = new UserHandler(_repository.Object, _userHelper.Object);
        }

        [Fact]
        public async Task FindEmployerAsyncTestAsync()
        {
            var input = new Employer() { FirstName = "a", LastName = "p" };
            _userHelper
                .Setup(e => e.FindEmployerAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(input)
                .Verifiable();

            var employer = await _userHandler.FindEmployerAsync("user", "password");
            employer.Should().NotBeNull();
            _userHelper
                .Verify(e => e.FindEmployerAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsyncTestAsync()
        {
            _userHelper
                .Setup(e => e.CreateEmployer(It.IsAny<SignInViewModel>()))
                .Returns(new Status()
                {
                    Error = null,
                    Success = true
                })
                .Verifiable();
            _userHelper
                .Setup(e => e.SaveAsync(It.IsAny<Status>()))
                .ReturnsAsync(new Status()
                {
                    Error = null,
                    Success = true
                })
                .Verifiable();

            var result = await _userHandler.CreateUserAsync(new SignInViewModel()
            {
                FirstName = "ak",
                Email = "do"
            });

            result.Should().NotBeNull();
            _userHelper
                .Verify(e => e.CreateEmployer(It.IsAny<SignInViewModel>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.SaveAsync(It.IsAny<Status>()), Times.AtLeastOnce);
        }

        [Fact]
        public void GetUserNameWithNumberTest()
        {
            var emp = new Employer()
            {
                FirstName = "a",
                LastName = "p",
                UserName = "ap"
            };

            _userHelper
                .Setup(e => e.GetEmployerByNumber(It.IsAny<long>()))
                .Returns(emp)
                .Verifiable();



            var result = _userHandler.GetUserName("843624435445");

            result.Should().NotBeNullOrEmpty();
            _userHelper
                .Verify(e => e.GetEmployerByNumber(It.IsAny<long>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.GetEmployerByNumber(It.IsAny<long>()), Times.AtMost(2));
        }

        [Fact]
        public void GetUserNameWithEmailTest()
        {
            var emp = new Employer()
            {
                FirstName = "a",
                LastName = "p",
                UserName = "ap"
            };

            _userHelper
                .Setup(e => e.GetEmployerByEmail(It.IsAny<string>()))
                .Returns(emp)
                .Verifiable();


            var result = _userHandler.GetUserName("ap");

            result.Should().NotBeNullOrEmpty();
            _userHelper
                .Verify(e => e.GetEmployerByEmail(It.IsAny<string>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.GetEmployerByEmail(It.IsAny<string>()), Times.AtMost(2));
        }

        [Fact]
        public void GetUserNameWithuserNameTest()
        {
            var emp = new Employer()
            {
                FirstName = "a",
                LastName = "p",
                UserName = "ap"
            };

            _userHelper
                .Setup(e => e.GetEmployerByEmail(It.IsAny<string>()))
                .Returns(It.IsAny<Employer>())
                .Verifiable();
            _userHelper
                .Setup(e => e.GetEmployerByUserName(It.IsAny<string>()))
                .Returns(emp)
                .Verifiable();


            var result = _userHandler.GetUserName("ayanpoddar9@gmail.com");

            result.Should().NotBeNullOrEmpty();
            _userHelper
                .Verify(e => e.GetEmployerByEmail(It.IsAny<string>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.GetEmployerByUserName(It.IsAny<string>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.GetEmployerByUserName(It.IsAny<string>()), Times.AtMost(2));
        }

        [Fact]
        public void GetUserNameWithUnicidTest()
        {
            var emp = new Employer()
            {
                FirstName = "a",
                LastName = "p",
                UserName = "ap"
            };

            _userHelper
                .Setup(e => e.GetEmployerByEmail(It.IsAny<string>()))
                .Returns(It.IsAny<Employer>())
                .Verifiable();
            _userHelper
                .Setup(e => e.GetEmployerByUserName(It.IsAny<string>()))
                .Returns(It.IsAny<Employer>())
                .Verifiable();
            _userHelper
                .Setup(e => e.GetEmployerByUnicId(It.IsAny<string>()))
                .Returns(emp)
                .Verifiable();


            var result = _userHandler.GetUserName("ayanpoddar");

            result.Should().NotBeNullOrEmpty();
            _userHelper
                .Verify(e => e.GetEmployerByEmail(It.IsAny<string>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.GetEmployerByUserName(It.IsAny<string>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.GetEmployerByUnicId(It.IsAny<string>()), Times.AtLeastOnce);
            _userHelper
                .Verify(e => e.GetEmployerByUnicId(It.IsAny<string>()), Times.AtMost(2));
        }
    }
}
