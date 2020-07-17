using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Helpers
{
    public class UserHelper : IUserhelper
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserHelper(IDataProtectionProvider dataProtectionProvider, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public bool CheckCookie() =>
            string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Request.Cookies[".AspUser"]);

        public string CheckUserValidToken()
        {
            try
                {
                    return _dataProtectionProvider
                    .CreateProtector(_configuration["dataprotector"])
                    .Unprotect(_httpContextAccessor
                        .HttpContext
                        .Request
                        .Cookies[".AspUser"]);
                }
                catch
                {
                    return null;
                }
        }
    }
}
