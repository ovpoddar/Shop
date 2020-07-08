using Microsoft.AspNetCore.Authorization;

namespace Shop.Utilities
{
    public class AuthorizeCookieAttribute : AuthorizeAttribute
    {
        public AuthorizeCookieAttribute()
        {
            AuthenticationSchemes = "Identity.Application";
            Roles = "Admin";
        }
    }
}
