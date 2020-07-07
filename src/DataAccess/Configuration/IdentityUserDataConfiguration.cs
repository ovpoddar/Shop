using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Configuration
{
    public class IdentityUserDataConfiguration : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.HasData(new Employer()
            {
                Id = "1",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                // Admin123.
                PasswordHash = "AQAAAAEAACcQAAAAENy1MasO3pTdOMRvDS7vQ6H0hs5NL9hOIgMbPIvj8WeSPwHB4D3C7BlwU6QzJ+JHCA==",
                PhoneNumber = "8436159825",
                LockoutEnabled = true,
                FirstName = "Shop",
                LastName = "Keeper",
                Gender = "Male",
                City = "Home",
                Active = true,
                LastLogin = DateTime.Now
            });
        }
    }
}
