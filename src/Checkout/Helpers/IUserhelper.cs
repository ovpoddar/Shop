using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Helpers
{
    public interface IUserhelper
    {
        string CheckUserValidToken();
        bool CheckCookie();
    }
}
