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
                Success = new bool()
            };
            try
            {
                if (_repository.GetAll().Any(_ => _.UserName == model.UserName))
                {
                    status.Success = false;
                    status.Error.Add("username is already in use");
                }
                else
                {
                    status.Success = true;
                }
                if (_repository.GetAll().Any(_ => _.MobileNo == model.MobileNo))
                {
                    status.Success = false;
                    status.Error.Add("mobile No is already in use");
                }
                else
                {
                    status.Success = true;
                }
                if (_repository.GetAll().Any(_ => _.Email == model.Email))
                {
                    status.Success = false;
                    status.Error.Add("email is already in use");
                }
                else
                {
                    status.Success = true;
                }
            }
            finally
            {
                if (status.Success)
                    _userHelper.CreateEmployer(model);
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
            _repository.GetAll().Where(_ => _.Email == email).FirstOrDefault();

        public Employer GetEmployerByNumber(long number) =>
            _repository.GetAll().Where(_ => _.MobileNo == number).FirstOrDefault();

        public Employer GetEmployerByUserName(string username) =>
            _repository.GetAll().Where(_ => _.UserName == username).FirstOrDefault();

        public Employer GetEmployerByUnicId(string username) =>
            _repository.GetAll().Where(_ => _.UnicId == username).FirstOrDefault();

        public async Task<Employer> FindEmployerAsync(string userId, string password) =>
            await _repository.FindAsync(_ => _.UserName == userId && _.Password == password);
    }
}
