using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Shop.Wrappers.Interfaces;
using System;
using System.Threading.Tasks;

namespace Shop.Wrappers
{
    public class SignInManagerWrapper : ISignInManagerWrapper
    {
        private readonly SignInManager<Employer> _signInManager;

        public SignInManagerWrapper(SignInManager<Employer> signInManager)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(_signInManager));
        }

        public async Task<SignInResult> PasswordSignInAsync(Employer employer, string Password, bool IsPersistent, bool LockOutFalier) =>
            await _signInManager.PasswordSignInAsync(employer, Password, IsPersistent, LockOutFalier);

        public async Task SignInAsync(Employer employer, bool isPersistent) =>
            await _signInManager.SignInAsync(employer, isPersistent);

        public async Task SignOutAsync() =>
            await _signInManager.SignOutAsync();
    }
}
