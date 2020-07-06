using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public class loginManager : IloginManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IConfiguration _configuration;
        private readonly IRequestManger _requestManger;

        public loginManager(IHttpContextAccessor httpContextAccessor, IRequestManger requestManger, IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
            _requestManger = requestManger ?? throw new ArgumentNullException(nameof(_requestManger));
            _dataProtectionProvider = dataProtectionProvider ?? throw new ArgumentNullException(nameof(_dataProtectionProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }

        public async Task<Status> LogMeIn(LogInViewModel logInView)
        {
            var responce = JsonConvert.DeserializeObject<Results<CustomeSignInResult>>(await _requestManger.PostRequest($"{WebSitesUrls.EndPoient}api/Authentication/Login", logInView));
            var returnobject = new Status()
            {
                Error = new List<string>(),
                Success = true
            };
            if (responce.Success)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append(".AspUser", _dataProtectionProvider.CreateProtector(_configuration["dataprotector"])
                                                                                        .Protect(responce.Result.Token));
                return returnobject;
            }

            foreach(var err in responce.Result.Errors)
            {
                returnobject.Error.Add(err.Description);
            }
            returnobject.Success = false;
            returnobject.Error.Add(responce.Exception);
            return returnobject;
        }

        public async Task LogMeOutAsync()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspUser", new CookieOptions()
            {
                Expires = DateTime.Now
            });
            await _httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
    }
}
