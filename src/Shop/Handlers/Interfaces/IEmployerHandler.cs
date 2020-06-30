using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface IEmployerHandler
    {
        List<Employer> GetAll();
        void BlockEmployer(string name);
        void lastcheckIn(Employer employer);
    }
}
