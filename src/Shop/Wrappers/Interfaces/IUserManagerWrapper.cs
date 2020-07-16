using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Wrappers.Interfaces
{
    public interface IUserManagerWrapper
    {
        Task<IdentityResult> CreateAsync(Employer employer, string Password);
        public IQueryable<Employer> Users { get; }
        Task<IdentityResult> UpdateAsync(Employer employer);
    }
}
