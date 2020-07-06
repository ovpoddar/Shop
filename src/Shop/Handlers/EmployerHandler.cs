using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Shop.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class EmployerHandler : IEmployerHandler
    {
        private readonly UserManager<Employer> _userManager;

        public EmployerHandler(UserManager<Employer> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
        }

        public async Task BlockEmployerAsync(string name)
        {
            var user = _userManager.Users.Where(e => e.UserName == name).FirstOrDefault();
            if (user != null)
                user.Active = user.Active == false;
            await _userManager.UpdateAsync(user);
        }

        public Employer GetEmployer(string query) =>
            _userManager.Users
            .Where(e => e.UserName == query || e.Email == query || e.PhoneNumber == query)
            .FirstOrDefault();

        public bool IsAccessable(Employer employer) =>
            _userManager.Users.Where(e => e.UserName == employer.UserName && e.PhoneNumber == employer.PhoneNumber && e.Email == employer.Email).FirstOrDefault().Active;

        public async Task<IdentityResult> LastcheckInAsync(Employer employer)
        {
            var user = _userManager.Users.Where(e => e.UserName == employer.UserName && e.PhoneNumber == employer.PhoneNumber && e.Email == employer.Email).FirstOrDefault();
            user.LastLogin = DateTime.Now;
            return await _userManager.UpdateAsync(user);
        }
    }
}
