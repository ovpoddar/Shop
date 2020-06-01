using Shop.Entities;
using Shop.Handlers.Interfaces;
using Shop.Helpers.Interfaces;
using Shop.Models;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IGenericRepository<Employer> _repository;
        private readonly IUserHelper _userHelper;

        public UserHandler(IGenericRepository<Employer> repository, IUserHelper userHelpers)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _userHelper = userHelpers ?? throw new ArgumentNullException(nameof(_userHelper));
        }


        public async Task<Employer> FindEmployerAsync(string userId, string password) =>
            await _userHelper.FindEmployerAsync(userId, password);

        public Task<Status> CreateUserAsync(SignInViewModel model)
        {
            var adding = _userHelper.CreateEmployer(model);
            return _userHelper.SaveAsync(adding);
        }

        public string GetUserName(string query)
        {
            try
            {
                if (_userHelper.GetEmployerByNumber(long.Parse(query)) != null)
                    return _userHelper.GetEmployerByNumber(long.Parse(query)).UserName;
                throw new ArgumentNullException();
            }
            catch
            {
                if (_userHelper.GetEmployerByEmail(query) != null)
                    return _userHelper.GetEmployerByEmail(query).UserName;
                if (_userHelper.GetEmployerByUserName(query) != null)
                    return _userHelper.GetEmployerByUserName(query).UserName;
                if (_userHelper.GetEmployerByUnicId(query) != null)
                    return _userHelper.GetEmployerByUnicId(query).UserName;
                return null;
            }
        }

    }
}
