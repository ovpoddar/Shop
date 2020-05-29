using Shop.Entities;
using Shop.Handlers.Interfaces;
using Shop.Helpers.Interfaces;
using System;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class ValidatorHandler : IValidatorHandler
    {
        private readonly ICookieHandler _cookie;
        private readonly ISignHandler _signHandler;
        private readonly IProtectorHandler _protectorHandler;
        private readonly IUserHandler _userHandler;
        private readonly IUserHelper _userHelper;

        public ValidatorHandler(ICookieHandler cookie, ISignHandler signHandler, IProtectorHandler protectorHandler, IUserHandler userHandler, IUserHelper userHelper)
        {
            _cookie = cookie ?? throw new ArgumentNullException(nameof(_cookie));
            _signHandler = signHandler ?? throw new ArgumentNullException(nameof(_signHandler));
            _protectorHandler = protectorHandler ?? throw new ArgumentNullException(nameof(_protectorHandler));
            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(_userHandler));
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(_userHelper));
        }

        public async Task<bool> IsMember()
        {
            if (BeasicCheck())
            {
                if ((await _signHandler.LogInAsync(_userHandler.GetUserName(_cookie.Get("User")), _protectorHandler.UnProtect(_cookie.Get("jd")))).Success)
                    return true;
            }
            return false;
        }

        public Employer User() =>
            _userHelper.GetEmployerByUnicId(_cookie.Get("User"));

        private bool BeasicCheck() =>
           !string.IsNullOrWhiteSpace(_cookie.Get("User")) &&
           !string.IsNullOrWhiteSpace(_cookie.Get("jd"));
    }
}
