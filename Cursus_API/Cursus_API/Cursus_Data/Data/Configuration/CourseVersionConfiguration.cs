using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cursus_Data.Data.Configuration
{
    public class CourseVersionConfiguration : IEntityTypeConfiguration<CourseVersion>
    {
        public void Configure(EntityTypeBuilder<CourseVersion> builder)
        {
            builder.HasData(
                new CourseVersion
                {
                    CourseVersionId = 1,
                    CourseId = "CS0001",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = false,

                },
                new CourseVersion
                {
                    CourseVersionId = 2,
                    CourseId = "CS0002",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = false
                },
                new CourseVersion
                {
                    CourseVersionId = 3,
                    CourseId = "CS0003",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = false
                },
                new CourseVersion
                {
                    CourseVersionId = 4,
                    CourseId = "CS0004",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = false
                },
                new CourseVersion
                {
                    CourseVersionId = 5,
                    CourseId = "CS0005",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = false
                },
                new CourseVersion
                {
                    CourseVersionId = 6,
                    CourseId = "CS0006",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = false
                },
                new CourseVersion
                {
                    CourseVersionId = 7,
                    CourseId = "CS0007",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = false
                },
                new CourseVersion
                {
                    CourseVersionId = 8,
                    CourseId = "CS0001",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.02m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 9,
                    CourseId = "CS0002",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.02m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 10,
                    CourseId = "CS0003",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.02m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 11,
                    CourseId = "CS0004",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.02m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 12,
                    CourseId = "CS0005",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.02m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 13,
                    CourseId = "CS0006",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.02m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 14,
                    CourseId = "CS0007",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.02m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 15,
                    CourseId = "CS0008",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 16,
                    CourseId = "CS0009",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 17,
                    CourseId = "CS0010",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 18,
                    CourseId = "CS0011",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 19,
                    CourseId = "CS0012",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 20,
                    CourseId = "CS0013",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 21,
                    CourseId = "CS0014",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 22,
                    CourseId = "CS0015",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 23,
                    CourseId = "CS0016",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 24,
                    CourseId = "CS0017",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 25,
                    CourseId = "CS0018",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 26,
                    CourseId = "CS0019",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 27,
                    CourseId = "CS0020",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 28,
                    CourseId = "CS0021",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 29,
                    CourseId = "CS0022",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 30,
                    CourseId = "CS0023",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 31,
                    CourseId = "CS0024",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 32,
                    CourseId = "CS0025",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 33,
                    CourseId = "CS0026",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 34,
                    CourseId = "CS0027",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                },
                new CourseVersion
                {
                    CourseVersionId = 35,
                    CourseId = "CS0028",
                    Status = "Activate",
                    IsApproved = true,
                    Version = 1.01m,
                    IsUsed = true
                }

            );
        }
    }
}