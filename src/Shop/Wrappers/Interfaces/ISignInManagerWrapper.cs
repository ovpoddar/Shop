using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Wrappers.Interfaces
{
    public interface ISignInManagerWrapper
    {
        Task<SignInResult> PasswordSignInAsync(Employer employer, string Password, bool IsPersistent, bool LockOutFalier);
        Task SignOutAsync();
        Task SignInAsync(Employer employer, bool isPersistent);
    }
}
