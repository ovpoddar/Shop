using FluentAssertions;
using Moq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Helpers;
using Shop.Models;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace test_for_all
{
    public class UserHandlerTest
    {
        private readonly Mock<IGenericRepository<Employer>> _mock;
        private readonly Mock<IUserHelper> _userHelper;
        private readonly UserHandler _userHandler;

        public UserHandlerTest()
        {
            _mock = new Mock<IGenericRepository<Employer>>();
            _userHelper = new Mock<IUserHelper>();
            _userHandler = new UserHandler(_mock.Object, _userHelper.Object);
        }

        [Theory]
        [InlineData("sarati", "sarati.45gmail.com", 9768565226, true, null)]
        [InlineData("ayan", "loak.50@gmail.com", 6543216741, false, "username is already in use")]
        [InlineData("rohine", "syman9@gmail.com", 8436159825, false, "mobile No is already in use")]
        [InlineData("koran", "ayanpoddar9@gmail.com", 6532165235, false, "email is already in use")]
        public void CreateEmployer_test(string userName, string email, long no, bool success, string compare)
        {
            var model = new SignInViewModel()
            {
                UserName = userName,
                MobileNo = no,
                Email = email,
                City = It.IsAny<string>(),
                FirstName = "ayan",
                LastName = "poddar",
                Password = "2400966653065",
                Gender = It.IsAny<Gender>(),
                ConfirmPassword = "545113744357413"
            };

            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            _userHelper
                .Setup(e => e.CreateEmployer(It.IsAny<SignInViewModel>()));
            var result = _userHandler.CreateEmployer(model);
            if (success)
            {
                Assert.True(result.Success);
            }
            else
            {
                Assert.False(result.Success);
                Assert.Equal(result.Error[0], compare);
            }
        }

        [Fact]
        public async Task SaveAsync_test_False()
        {
            var input = new Status
            {
                Error = null,
                Success = false
            };

            var result = await _userHandler.SaveAsync(input);
            Assert.False(result.Success);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task SaveAsync_test_True()
        {
            var input = new Status
            {
                Error = null,
                Success = true
            };

            _mock
                .Setup(e => e.SaveAsync())
                .Returns(Task.FromResult(1));

            var result = await _userHandler.SaveAsync(input);
            Assert.True(result.Success);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task SaveAsync_test_False_With_Err()
        {
            var input = new Status
            {
                Error = new List<string>(),
                Success = true
            };

            _mock
                .Setup(e => e.SaveAsync())
                .Returns(Task.FromResult(0));

            var result = await _userHandler.SaveAsync(input);
            Assert.False(result.Success);
            Assert.Equal("Store to database Fail", result.Error[0]);
        }

        [Fact]
        public void GetEmployerByEmail_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHandler.GetEmployerByEmail("ayanpoddar9@gmail.com");
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Fact]
        public void GetEmployerByNumber_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHandler.GetEmployerByNumber(8436159825);
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Fact]
        public void GetEmployerByUnicId_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHandler.GetEmployerByUnicId("sjvnlisdbsdklfb564");
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Fact]
        public void GetEmployerByUserName_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHandler.GetEmployerByUserName("ayan");
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Theory]
        [InlineData("ayan", "2400966653065",0)]
        [InlineData("amar", "24056466357", 1)]
        public async Task FindEmployerAsync_test(string userId, string password, int index)
        {
            var expected = Getall().ToList()[index];
            _mock
                .Setup(e => e.FindAsync(e => e.UserName == userId && e.Password == password))
                .Returns(Task.FromResult(expected));

            var result = await _userHandler.FindEmployerAsync(userId, password);
            expected.Should().BeEquivalentTo(result);
        }

        private IQueryable<Employer> Getall() =>
            new List<Employer>() {
                new Employer
            {
                Id =001,
                UserName = "ayan",
                CapatalisedEmail = It.IsAny<string>(),
                MobileNo = 8436159825,
                Email = "ayanpoddar9@gmail.com",
                CapatalisedFullName = It.IsAny<string>(),
                CapatalisedUserName = It.IsAny<string>(),
                City = It.IsAny<string>(),
                FirstName = "ayan",
                Gender = "male",
                LastName = "poddar",
                Password = "2400966653065",
                UnicId = "sjvnlisdbsdklfb564"
            },new Employer
            {
                Id =002,
                UserName = "amar",
                CapatalisedEmail = It.IsAny<string>(),
                MobileNo = 6295598234,
                Email = "amarpoddar9@gmail.com",
                CapatalisedFullName = It.IsAny<string>(),
                CapatalisedUserName = It.IsAny<string>(),
                City = It.IsAny<string>(),
                FirstName = "amar",
                Gender = "male",
                LastName = "poddar",
                Password = "24056466357+",
                UnicId = It.IsAny<string>()
            },new Employer
            {
                Id =003,
                UserName = "loka",
                CapatalisedEmail = It.IsAny<string>(),
                MobileNo = 4588663954,
                Email = "lokapoddar9@gmail.com",
                CapatalisedFullName = It.IsAny<string>(),
                CapatalisedUserName = It.IsAny<string>(),
                City = It.IsAny<string>(),
                FirstName = "loka",
                Gender = "male",
                LastName = "poddar",
                Password = "240095465465132",
                UnicId = It.IsAny<string>()
            }, }.AsQueryable();
    }
}
