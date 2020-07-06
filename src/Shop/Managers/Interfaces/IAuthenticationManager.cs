using DataAccess.Entities;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Managers.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<CustomeIdentityResult> SignUpUserAsync(SignInViewModel model);
        Task<CustomeSignInResult> LogInUserAsync(Employer user, string password);
    }
}
