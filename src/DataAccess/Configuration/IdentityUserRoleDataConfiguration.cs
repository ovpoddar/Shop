using DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration
{
    public class IdentityUserRoleDataConfiguration : IEntityTypeConfiguration<IdentityUserRole>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole> builder)
        {
            builder.HasData(new IdentityUserRole()
            {
                RoleId = "1",
                UserId = "1"
            });
        }
    }
}
