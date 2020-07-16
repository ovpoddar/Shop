using DataAccess.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shop.Handlers;
using Shop.Wrappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class EmployerHandlerTest
    {
        private readonly Mock<IUserManagerWrapper> _userManager;
        private readonly EmployerHandler _employer;

        public EmployerHandlerTest()
        {
            _userManager = new Mock<IUserManagerWrapper>();
            _employer = new EmployerHandler(_userManager.Object);
        }


        [Fact]
        public async Task BlockEmployerAsync()
        {
            _userManager.Setup(e => e.Users).Returns(new List<Employer>()
            {
                new Employer()
                {
                    UserName = "coco",
                    Active= false                    
                }
            }.AsQueryable());
            _userManager.Setup(e => e.UpdateAsync(It.IsAny<Employer>())).Verifiable();

            await _employer.BlockEmployerAsync("coco");

            _userManager.Verify(e => e.UpdateAsync(It.IsAny<Employer>()), Times.Once);
        }

        [Fact]
        public void GetEmployer()
        {
            _userManager.Setup(e => e.Users).Returns(new List<Employer>()
            {
                new Employer()
                {
                    UserName = "coco",
                    Email = "coco1",
                    PhoneNumber = "coco112"
                }
            }.AsQueryable()).Verifiable();

            var result = _employer.GetEmployer("coco");
            var result1 = _employer.GetEmployer("coco1");
            var result2 = _employer.GetEmployer("coco112");

            result.Should().NotBeNull();
            result1.Should().NotBeNull();
            result2.Should().NotBeNull();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsAccessable(bool check)
        {
            _userManager.Setup(e => e.Users).Returns(new List<Employer>()
            {
                new Employer()
                {
                    UserName = "coco",
                    Email = "coco1",
                    PhoneNumber = "coco112",
                    Active = check
                }
            }.AsQueryable()).Verifiable();

            var result = _employer.IsAccessable(new Employer()
            {
                UserName = "coco",
                Email = "coco1",
                PhoneNumber = "coco112"
            });

            result.Should().Be(check);
        }

        [Fact]
        public void LastcheckInAsync()
        {

            _userManager.Setup(e => e.Users).Returns(new List<Employer>()
            {
                new Employer()
                {
                    UserName = "coco",
                    Email = "coco1",
                    PhoneNumber = "coco112",
                    Active = true,
                    LastLogin = It.IsAny<DateTime>()
                }
            }.AsQueryable()).Verifiable();
            _userManager.Setup(e => e.UpdateAsync(It.IsAny<Employer>())).Returns(Task.FromResult(It.IsAny<IdentityResult>())).Verifiable();

            var result = _employer.LastcheckInAsync(new Employer()
            {
                UserName = "coco",
                Email = "coco1",
                PhoneNumber = "coco112"
            });

            result.Should().NotBeNull();
            _userManager.Verify(e => e.Users,Times.Once);
            _userManager.Verify(e => e.UpdateAsync(It.IsAny<Employer>()), Times.Once);
        }
    }
}
