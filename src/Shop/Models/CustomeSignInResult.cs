﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Shop.Models
{
    public class CustomeSignInResult
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string Token { get; set; }
        public List<IdentityError> Errors { get; set; }
    }
}
