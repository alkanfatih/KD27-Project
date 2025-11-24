using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.HasData(
                new IdentityRole<int>() { Name = "User", NormalizedName = "USER", Id = 1 },
                new IdentityRole<int>() { Name = "Editor", NormalizedName = "EDITOR", Id = 2 },
                new IdentityRole<int>() { Name = "Admin", NormalizedName = "ADMIN", Id = 3 }
            );
        }
    }
}
