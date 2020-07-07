using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Shop.Models
{
    public class CustomeIdentityResult
    {
        public bool Success { get; set; }
        public List<IdentityError> Errors { get; set; }
        public string Token { get; set; }
    }
}
