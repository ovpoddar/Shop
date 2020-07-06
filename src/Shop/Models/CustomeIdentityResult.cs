using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class CustomeIdentityResult
    {
        public bool Success { get; set; }
        public List<IdentityError> Errors { get; set; }
        public string Token { get; set; }
    }
}
