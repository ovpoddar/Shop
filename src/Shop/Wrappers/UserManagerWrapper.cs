using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Shop.Wrappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Wrappers
{
    public class UserManagerWrapper : IUserManagerWrapper
    {
        private readonly UserManager<Employer> _userManager;

        public UserManagerWrapper(UserManager<Employer> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
        }

        public IQueryable<Employer> Users =>
            _userManager.Users;

        public async Task<IdentityResult> CreateAsync(Employer employer, string Password) =>
            await _userManager.CreateAsync(employer, Password);

        public Task<IdentityResult> UpdateAsync(Employer employer) => 
            _userManager.UpdateAsync(employer);
    }
}
