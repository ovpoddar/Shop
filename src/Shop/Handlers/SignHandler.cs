using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class SignHandler : ISignHandler
    {
        private readonly IUserManager _userManager;

        public SignHandler(IUserManager userManager)
        {
            _userManager = userManager ?? throw new System.ArgumentNullException(nameof(_userManager));
        }

        public async Task<LoginStatus> LogInAsync(string user, string password)
        {
            var username = _userManager.GetUserName(user);
            if (string.IsNullOrWhiteSpace(username))
                return new LoginStatus
                {
                    Error = new List<string>()
                        {
                            new string("Use a Valid Username")
                        },
                    Success = false
                };
            else
            {
                var result = await _userManager.FindEmployerAsync(username, password);
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
}
