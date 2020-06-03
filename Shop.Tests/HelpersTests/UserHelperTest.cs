using DataAccess.Entities;
using FluentAssertions;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Helpers;
using Shop.Models;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.HelpersTests
{
    public class UserHelperTest
    {
        private readonly Mock<IGenericRepository<Employer>> _mock;
        private readonly Mock<IProtectorHandler> _protectorHandler;
        private readonly UserHelper _userHelper;
        public UserHelperTest()
        {
            _mock = new Mock<IGenericRepository<Employer>>();
            _protectorHandler = new Mock<IProtectorHandler>();
            _userHelper = new UserHelper(_mock.Object, _protectorHandler.Object);

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

            var result = _userHelper.CreateEmployer(model);
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

            var result = await _userHelper.SaveAsync(input);
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

            var result = await _userHelper.SaveAsync(input);
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

            var result = await _userHelper.SaveAsync(input);
            Assert.False(result.Success);
            Assert.Equal("Store to database Fail", result.Error[0]);
        }

        [Fact]
        public void GetEmployerByEmail_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHelper.GetEmployerByEmail("ayanpoddar9@gmail.com");
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Fact]
        public void GetEmployerByNumber_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHelper.GetEmployerByNumber(8436159825);
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Fact]
        public void GetEmployerByUnicId_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHelper.GetEmployerByUnicId("sjvnlisdbsdklfb564");
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Fact]
        public void GetEmployerByUserName_Test()
        {
            _mock
                .Setup(e => e.GetAll())
                .Returns(Getall());

            var result = _userHelper.GetEmployerByUserName("ayan");
            result.Should().BeEquivalentTo(Getall().ToList()[0]);
        }

        [Theory]
        [InlineData("ayan", "2400966653065", 0)]
        [InlineData("loka", "240095465465132", 2)]
        public async Task FindEmployerAsyncTestAsync(string username, string password, int index)
        {
            var output = Getall().ToList()[index];
            _mock
                .Setup(e => e.FindAsync(It.IsAny<Expression<Func<Employer, bool>>>()))
                .Returns(Task.FromResult(output));

            var result = await _userHelper.FindEmployerAsync(username, password);
            _mock.Verify(e => e.FindAsync(It.IsAny<Expression<Func<Employer, bool>>>()), Times.Once);
            result.Should().BeEquivalentTo(output);
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
                }
            }.AsQueryable();
    }
}
