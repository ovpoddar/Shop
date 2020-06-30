using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public interface IEmployeeReposotory
    {
        IEnumerable<Employer> GetAll();
        void save();
    }
}
