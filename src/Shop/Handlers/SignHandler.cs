using Shop.Handlers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class SignHandler : ISignHandler
    {
        private readonly IUserHandler _userHandler;

        public SignHandler(IUserHandler userHandler)
        {
            _userHandler = userHandler ?? throw new System.ArgumentNullException(nameof(_userHandler));
        }

        public async Task<LoginStatus> LogInAsync(string user, string password)
        {
            var username = _userHandler.GetUserName(user);
            if (string.IsNullOrWhiteSpace(username))
                return new LoginStatus
                {
                    Error = new List<string>()
                        {
                            new string("Use a Valid Username")
                        },
                    Success = false
                };
            var result = await _userHandler.FindEmployerAsync(username, password);
            if (!result.Active)
                return new LoginStatus
                {
                    Employer = null,
                    Error = new List<string>()
                    {
                        new string("Contact To The ShopKeepre")
                    },
                    Success = false
                };
            _userHandler.DetectLogin(result);
            return result == null ?
            new LoginStatus
            {
                Success = false,
                Error = new List<string>()
                {
                    new string("use a valid password")
                },
                Employer = null
            }
            :
            new LoginStatus
            {
                Employer = result,
                Error = null,
                Success = true
            };
        }
    }
}
