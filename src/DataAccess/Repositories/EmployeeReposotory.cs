using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class EmployeeReposotory : IEmployeeReposotory
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeReposotory(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(_applicationDbContext));
        }

        public IEnumerable<Employer> GetAll() =>
            _applicationDbContext.Users;

        public void save() =>
            _applicationDbContext.SaveChanges();
    }
}
