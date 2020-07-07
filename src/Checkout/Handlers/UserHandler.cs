using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Checkout.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserHandler(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dataProtectionProvider = dataProtectionProvider ?? throw new ArgumentNullException(nameof(_dataProtectionProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
        }

        public string Username => string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Request.Cookies[".AspUser"])
                ? null
                : new JwtSecurityTokenHandler()
                .ReadJwtToken(_dataProtectionProvider.CreateProtector(_configuration["dataprotector"])
                    .Unprotect(_httpContextAccessor.HttpContext.Request.Cookies[".AspUser"])).Claims
                .First(e => e.Type == ClaimsIdentity.DefaultNameClaimType)
                .Value;

        public string UserToken => string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Request.Cookies[".AspUser"])
                ? null
                : _dataProtectionProvider
                .CreateProtector(_configuration["dataprotector"])
                .Unprotect(_httpContextAccessor
                    .HttpContext
                    .Request
                    .Cookies[".AspUser"]);
    }
}
