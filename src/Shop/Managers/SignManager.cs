using Microsoft.AspNetCore.Http;
using Shop.Handlers;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Threading.Tasks;
using Shop.Handlers.Interfaces;

namespace Shop.Managers
{
    public class SignManager : ISignManager
    {
        private readonly ISignHandler _signHandler;
        private readonly IProtectorHandler _protectorHandler;
        private readonly ICookieHandler _cookieHandler;

        public SignManager(ICookieHandler cookieHandler, ISignHandler signHandler, IProtectorHandler protectorHandler)
        {
            _signHandler = signHandler ?? throw new ArgumentNullException(nameof(_signHandler));
            _protectorHandler = protectorHandler ?? throw new ArgumentNullException(nameof(_protectorHandler));
            _cookieHandler = cookieHandler ?? throw new ArgumentNullException(nameof(_cookieHandler));
        }


        public async Task<LoginStatus> LoginAsyncMethod(LogInViewModel user, string password)
        {
            var state = await _signHandler.LogInAsync(user.UserName, _protectorHandler.HashMd5(password));
            if (state.Success)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1),
                    HttpOnly = false,
                    Secure = false,
                    SameSite = SameSiteMode.None,
                    Path = "/"
                };
                _cookieHandler.Create("User", state.Employer.UnicId, options);
                _cookieHandler.Create("jd", _protectorHandler.Protect(state.Employer.Password), options);
            }
            return state;
        }

        public void LogOut()
        {
            _cookieHandler.Delete("User");
            _cookieHandler.Delete("jd");
        }
    }
}
