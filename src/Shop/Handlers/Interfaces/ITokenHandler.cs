using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface ITokenHandler
    {
        string GenerateToken(Employer employer);
    }
}
