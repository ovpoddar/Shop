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
        private readonly IGenericRepository<Employer> _genericRepository;

        public EmployerHandler(IGenericRepository<Employer> genericRepository)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(_genericRepository));
        }

        public void BlockEmployer(string Username)
        {
            var Employee = _genericRepository.GetAll().First(e => e.UserName == Username);
            if (Employee.Active)
                Employee.Active = false;
            else
                Employee.Active = true;
            _genericRepository.Update(Employee);

        }

        public List<Employer> GetAll() =>
            _genericRepository.GetAll().ToList();
    }
}
