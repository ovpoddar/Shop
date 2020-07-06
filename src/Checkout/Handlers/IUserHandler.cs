using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Handlers
{
    public interface IUserHandler
    {
        string Username { get; }
        string UserToken { get; }
    }
}
