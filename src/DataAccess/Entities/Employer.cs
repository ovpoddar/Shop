using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Employer : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Balance> Balances { get; set; }
    }
}
