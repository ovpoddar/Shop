using Shop.Entities;
using Shop.Handlers;
using Shop.Repositories;
using Shop.ViewModels;
using System;

namespace Shop.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly IGenericRepository<Employer> _repository;
        private readonly IProtectorHandler _protector;

        public UserHelper(IGenericRepository<Employer> repository, IProtectorHandler protector)
        {

            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _protector = protector ?? throw new ArgumentNullException(nameof(_protector));
        }
        public void CreateEmployer(SignInViewModel model) =>
            _repository.Add(new Employer
            {
                CapatalisedEmail = model.Email.ToUpper(),
                Email = model.Email,
                CapatalisedFullName = $"{model.FirstName} {model.LastName}",
                CapatalisedUserName = model.UserName.ToUpper(),
                UserName = model.UserName,
                City = model.City,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender.ToString(),
                MobileNo = model.MobileNo,
                Password = _protector.HashMd5(model.ConfirmPassword),
                UnicId = _protector.Hashsha512(model.UserName + model.Password)
            });
    }
}
