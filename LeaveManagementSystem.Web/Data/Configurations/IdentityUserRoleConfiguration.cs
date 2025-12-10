using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e8bfb61c-23f4-4a03-9534-a4efdc4ab6fa",
                    UserId = "ba873cf8-6060-41dc-9495-05fb9966e273"
                });
        }
    }
}
