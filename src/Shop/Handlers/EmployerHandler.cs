using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Shop.Handlers.Interfaces;
using Shop.Wrappers.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class EmployerHandler : IEmployerHandler
    {
        private readonly IUserManagerWrapper _userManager;

        public EmployerHandler(IUserManagerWrapper userManager)
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
            var user = _userManager.Users.Where(e => e.UserName == employer.UserName
                                                     && e.PhoneNumber == employer.PhoneNumber
                                                     && e.Email == employer.Email)
                .FirstOrDefault();
            user.LastLogin = DateTime.Now;
            return await _userManager.UpdateAsync(user);
        }
    }
}
