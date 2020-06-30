using DataAccess.Entities;
using DataAccess.Repositories;
using Shop.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public class EmployerHandler : IEmployerHandler
    {
        private readonly IEmployeeReposotory _genericRepository;

        public EmployerHandler(IEmployeeReposotory genericRepository)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(_genericRepository));
        }

        public void BlockEmployer(string name)
        {
            var user = _genericRepository.GetAll().Where(e => e.UserName == name).FirstOrDefault();
            if (user != null)
                if(user.Active == false)
                    user.Active = true;
                else
                    user.Active = false;

            _genericRepository.save();

        }

        public List<Employer> GetAll() =>
            _genericRepository.GetAll().ToList();

        public void lastcheckIn(Employer employer)
        {
            var user = _genericRepository.GetAll().Where(e => e.UserName == employer.UserName && e.PhoneNumber == employer.PhoneNumber && e.Email == employer.Email).FirstOrDefault();
            user.LastLogin = DateTime.UtcNow;
            _genericRepository.save();
        }
    }
}
