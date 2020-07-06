using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dataaccess.configuration
{
    public class identityuserroledataconfiguration : IEntityTypeConfiguration<IdentityUserRole>
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
