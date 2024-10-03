

using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cursus_Data.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData
                (
                new Role
                {
                    RoleId = 1,
                    Name = "Student"
                },
                 new Role
                 {
                     RoleId = 2,
                     Name = "Instructor"
                 },
                  new Role
                  {
                      RoleId = 3,
                      Name = "Admin"
                  }
                );
        }
    }
}
