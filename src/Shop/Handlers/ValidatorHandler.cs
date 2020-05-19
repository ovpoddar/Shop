using Shop.Entities;
using Shop.Managers;
using System;
using System.Threading.Tasks;
using Shop.Handlers.Interfaces;

namespace Shop.Handlers
{
    public class ValidatorHandler : IValidatorHandler
    {
        private readonly ICookieHandler _cookie;
        private readonly ISignHandler _signHandler;
        private readonly IProtectorHandler _protectorHandler;
        private readonly IUserHandler _userHandler;
        private readonly IUserManager _userManager;

        public ValidatorHandler(ICookieHandler cookie, ISignHandler signHandler, IProtectorHandler protectorHandler, IUserManager userManager, IUserHandler userHandler)
        {
            _cookie = cookie ?? throw new ArgumentNullException(nameof(_cookie));
            _signHandler = signHandler ?? throw new ArgumentNullException(nameof(_signHandler));
            _protectorHandler = protectorHandler ?? throw new ArgumentNullException(nameof(_protectorHandler));
            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(_userHandler));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
        }


        public async Task<bool> IsMember()
        {
            if (BeasicCheck())
            {
                if ((await _signHandler.LogInAsync(_userManager.GetUserName(_cookie.Get("User")), _protectorHandler.UnProtect(_cookie.Get("jd")))).Success)
                    return true;
            }
            return false;
        }

        public Employer User() =>
            _userHandler.GetEmployerByUnicId(_cookie.Get("User"));

        private bool BeasicCheck() =>
           !string.IsNullOrWhiteSpace(_cookie.Get("User")) &&
           !string.IsNullOrWhiteSpace(_cookie.Get("jd"));
    }
}
