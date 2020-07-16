using DataAccess.Entities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Shop.Handlers;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class TokenHandlerTest
    {
        private readonly Mock<IConfiguration> _configuration;
        private readonly TokenHandler _tokenHandler;

        public TokenHandlerTest()
        {
            _configuration = new Mock<IConfiguration>();
            _tokenHandler = new TokenHandler(_configuration.Object);
        }

        [Fact]
        public void GenerateToken()
        {
            _configuration.Setup(e => e.GetSection(It.IsAny<string>())[It.IsAny<string>()]).Returns("some value which store in application.json file").Verifiable();

            var token = _tokenHandler.GenerateToken(new Employer()
            {
                FirstName = "c",
                LastName = "d",
                Email = "cd.",
                Gender = "male",
                UserName = "cola",
                Id = "asv56v56453dav45643165a4651",
            });

            token.Should().NotBeNullOrEmpty();
            _configuration.Verify(e => e.GetSection(It.IsAny<string>())[It.IsAny<string>()], Times.Exactly(3));
        }
    }
}
