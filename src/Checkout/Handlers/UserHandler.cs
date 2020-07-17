using Checkout.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Checkout.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserhelper _userhelper;

        public UserHandler(IUserhelper userhelper)
        {
            _userhelper = userhelper ?? throw new ArgumentNullException(nameof(_userhelper));
        }

        public string Username => _userhelper.CheckCookie() || new JwtSecurityTokenHandler().CanReadToken(_userhelper.CheckUserValidToken()) == false
                ? null
                : new JwtSecurityTokenHandler()
                .ReadJwtToken(_userhelper.CheckUserValidToken()).Claims
                .First(e => e.Type == ClaimsIdentity.DefaultNameClaimType)
                .Value;

        public string UserToken =>
            _userhelper.CheckCookie() || string.IsNullOrWhiteSpace(_userhelper.CheckUserValidToken())
                ? null
                : _userhelper.CheckUserValidToken();
    }
}
