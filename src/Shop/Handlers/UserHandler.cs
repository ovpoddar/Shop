using Shop.Entities;
using Shop.Helpers;
using Shop.Models;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IGenericRepository<Employer> _repository;
        private readonly IUserHelper _userHelper;

        public UserHandler(IGenericRepository<Employer> repository, IUserHelper userHelper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _userHelper = userHelper ?? throw new ArgumentNullException(nameof(_userHelper));
        }

        public Status CreateEmployer(SignInViewModel model)
        {
            var status = new Status
            {
                Error = new List<string>(),
                Success = true
            };
            try
            {
                if (_repository.GetAll().Any(e => e.UserName == model.UserName))
                {
                    status.Success = false;
                    status.Error.Add("username is already in use");
                }
                if (_repository.GetAll().Any(e => e.MobileNo == model.MobileNo))
                {
                    status.Success = false;
                    status.Error.Add("mobile No is already in use");
                }

                if (_repository.GetAll().Any(e => e.Email == model.Email))
                {
                    status.Success = false;
                    status.Error.Add("email is already in use");
                }
            }
            finally
            {
                if (!status.Success)
                    _userHelper.CreateEmployer(model);
                else
                    status.Success = true;
            }
            return status;
        }

        public async Task<Status> SaveAsync(Status status)
        {
            if (status.Success)
            {
                try
                {
                    if (await _repository.SaveAsync() == 1)
                        return new Status
                        {
                            Success = true,
                            Error = null
                        };
                    else
                        throw new Exception("Store to database Fail");
                }
                catch (Exception exceptions)
                {
                    status.Error.Add(exceptions.Message.ToString());
                    return new Status
                    {
                        Error = status.Error,
                        Success = false
                    };
                }
            }
            return status;
        }

        public Employer GetEmployerByEmail(string email) =>
            _repository.GetAll().FirstOrDefault(e => e.Email == email);

        public Employer GetEmployerByNumber(long number) =>
            _repository.GetAll().FirstOrDefault(e => e.MobileNo == number);

        public Employer GetEmployerByUserName(string username) =>
            _repository.GetAll().FirstOrDefault(e => e.UserName == username);

        public Employer GetEmployerByUnicId(string username) =>
            _repository.GetAll().FirstOrDefault(e => e.UnicId == username);

        public async Task<Employer> FindEmployerAsync(string userId, string password) =>
            await _repository.FindAsync(e => e.UserName == userId && e.Password == password);
    }
}
