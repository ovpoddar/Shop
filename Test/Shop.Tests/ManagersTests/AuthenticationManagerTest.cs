using AutoMapper;
using DataAccess.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Managers;
using Shop.ViewModels;
using Shop.Wrappers.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class AuthenticationManagerTest
    {
        private readonly Mock<IUserManagerWrapper> _userManager;
        private readonly Mock<ISignInManagerWrapper> _signInManager;
        private readonly Mock<IEmployerHandler> _employerHandler;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly Mock<IDataProtectionProvider> _dataProtectionProvider;
        private readonly Mock<IDataProtector> _dataProtection;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<ITokenHandler> _tokenHandler;
        private readonly Mock<IMapper> _mapper;
        private readonly AuthenticationManager _authenticationManager;


        public AuthenticationManagerTest()
        {
            _userManager = new Mock<IUserManagerWrapper>();
            _signInManager = new Mock<ISignInManagerWrapper>();
            _employerHandler = new Mock<IEmployerHandler>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _dataProtectionProvider = new Mock<IDataProtectionProvider>();
            _configuration = new Mock<IConfiguration>();
            _tokenHandler = new Mock<ITokenHandler>();
            _dataProtection = new Mock<IDataProtector>();
            _mapper = new Mock<IMapper>();
            _authenticationManager = new AuthenticationManager(_userManager.Object,
                                                               _signInManager.Object,
                                                               _mapper.Object,
                                                               _employerHandler.Object,
                                                               _tokenHandler.Object,
                                                               _httpContextAccessor.Object,
                                                               _dataProtectionProvider.Object,
                                                               _configuration.Object);
        }
        [Fact]
        public async Task LogInUserUsernotAccessableAsyncTestAsync()
        {
            _signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<Employer>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(Task.FromResult(It.IsAny<SignInResult>())).Verifiable();
            _employerHandler.Setup(e => e.IsAccessable(It.IsAny<Employer>())).Returns(false);
            _tokenHandler.Setup(e => e.GenerateToken(It.IsAny<Employer>())).Returns("it is a secrate token").Verifiable();
            _httpContextAccessor.Setup(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>())).Verifiable();
            _dataProtectionProvider.Setup(e => e.CreateProtector(It.IsAny<string>())).Returns(_dataProtection.Object).Verifiable();
            _configuration.Setup(e => e[It.IsAny<string>()]).Returns("the secrate key which store in applicationjson file").Verifiable();

            var result = await _authenticationManager.LogInUserAsync(It.IsAny<Employer>(), It.IsAny<string>());

            result.Should().NotBeNull();
            result.Errors.Should().NotBeNull();
            result.Token.Should().BeNullOrWhiteSpace();
            result.Succeeded.Should().BeFalse();
            _signInManager.Verify(e => e.PasswordSignInAsync(It.IsAny<Employer>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
            _employerHandler.Verify(e => e.IsAccessable(It.IsAny<Employer>()), Times.Once);
            _tokenHandler.Verify(e => e.GenerateToken(It.IsAny<Employer>()), Times.Never);
            _httpContextAccessor.Verify(e => e.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Never);
            _dataProtectionProvider.Verify(e => e.CreateProtector(It.IsAny<string>()), Times.Never);
            _configuration.Verify(e => e[It.IsAny<string>()], Times.Never);

        }

        [Fact]
        public async Task LogInUserResultAsyncFoundsomethingAsync()
        {
            _employerHandler.Setup(e => e.GetEmployer(It.IsAny<string>())).Returns(new Employer()
            {
                Active = true,
                Gender = "male"
            }).Verifiable();
            var result = await _authenticationManager.LogInUserResultAsync(new LogInViewModel()
            {
            Password = "coco",
            UserName = "sola"
            });
            result.Exception.Should().BeNull();
            _employerHandler.Verify(e => e.GetEmployer(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public async Task LogInUserResultAsyncFoundNothingAsync()
        {
            _employerHandler.Setup(e => e.GetEmployer(It.IsAny<string>())).Returns((Employer)null).Verifiable();
            var result = await _authenticationManager.LogInUserResultAsync(new LogInViewModel()
            {
            Password = "coco",
            UserName = "sola"
            });
            result.Exception.Should().Be("Provide a Valid Credentials");
            _employerHandler.Verify(e => e.GetEmployer(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task SignOutUserTestAsync()
        {
            _httpContextAccessor.Setup(e => e.HttpContext.Response.Cookies.Delete(It.IsAny<string>(), It.IsAny<CookieOptions>())).Verifiable();
            _signInManager.Setup(e => e.SignOutAsync()).Verifiable();

            await _authenticationManager.SignOutUserAsync();

            _httpContextAccessor.Verify(e => e.HttpContext.Response.Cookies.Delete(It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once);
            _signInManager.Verify((e => e.SignOutAsync()), Times.Once);
        }

        [Fact]
        public async Task SignUpUserTestAsync()
        {
            _mapper.Setup(e => e.Map<Employer>(It.IsAny<SignInViewModel>())).Returns(new Employer()
            {
                AccessFailedCount = 2,
                Active = true,
                City = "acsm"
            }).Verifiable();
            _userManager.Setup(e => e.CreateAsync(It.IsAny<Employer>(), It.IsAny<string>())).Returns(Task.FromResult(new IdentityResult()));

            var result = await _authenticationManager.SignUpUserAsync(new SignInViewModel()
            {
                ConfirmPassword = It.IsAny<string>()
            });

            _mapper.Verify(e => e.Map<Employer>(It.IsAny<SignInViewModel>()), Times.Once);
            result.Should().NotBeNull();
        }
    }

}
