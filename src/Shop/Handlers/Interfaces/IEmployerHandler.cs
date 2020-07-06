using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface IEmployerHandler
    {
        Task BlockEmployerAsync(string name);
        Task<IdentityResult> LastcheckInAsync(Employer employer);
        bool IsAccessable(Employer employer);
        Employer GetEmployer(string query);
    }
}
