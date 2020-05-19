using Shop.Entities;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Threading.Tasks;

namespace Shop.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserHandler _userHandler;

        public UserManager(IUserHandler userHandler)
        {
            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(_userHandler));
        }
        public Task<Status> CreateUserAsync(SignInViewModel model)
        {
            var result = _userHandler.CreateEmployer(model);
            return _userHandler.SaveAsync(result);
        }

        public async Task<Employer> FindEmployerAsync(string userId, string password) => await _userHandler.FindEmployerAsync(userId, password);

        public string GetUserName(string query)
        {
            try
            {
                if (_userHandler.GetEmployerByNumber(long.Parse(query)) != null)
                    return _userHandler.GetEmployerByNumber(long.Parse(query)).UserName;
                throw new ArgumentNullException();
            }
            catch
            {
                if (_userHandler.GetEmployerByEmail(query) != null)
                    return _userHandler.GetEmployerByEmail(query).UserName;
                if (_userHandler.GetEmployerByUserName(query) != null)
                    return _userHandler.GetEmployerByUserName(query).UserName;
                if (_userHandler.GetEmployerByUnicId(query) != null)
                    return _userHandler.GetEmployerByUnicId(query).UserName;
                return null;
            }
        }
    }
}
