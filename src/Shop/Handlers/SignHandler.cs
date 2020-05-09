using Shop.Managers;
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
                if (result == null)
                    return new LoginStatus
                    {
                        Success = false,
                        Error = new List<string>()
                        {
                            new string("use a valid password")
                        },
                        Employer = null
                    };
                else
                    return new LoginStatus
                    {
                        Employer = result,
                        Error = null,
                        Success = true
                    };
            }
        }
    }
}
