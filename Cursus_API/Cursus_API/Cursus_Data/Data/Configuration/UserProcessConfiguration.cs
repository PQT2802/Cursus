using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Data.Configuration
{
    public class UserProcessConfiguration : IEntityTypeConfiguration<UserProcess>
    {
        public void Configure(EntityTypeBuilder<UserProcess> builder)
        {
            builder.HasData(
                new UserProcess
                {
                    UserProcessId = 1,
                    EnrollCourseId = "EC00000001",
                    CourseContentId = "CC00000001",
                    IsComplete = false,
                },
                new UserProcess
                {
                    UserProcessId = 2,
                    EnrollCourseId = "EC00000001",
                    CourseContentId = "CC00000002",
                    IsComplete = false,
                },
                new UserProcess
                {
                    UserProcessId = 3,
                    EnrollCourseId = "EC00000001",
                    CourseContentId = "CC00000003",
                    IsComplete = true,
                },
                new UserProcess
                {
                    UserProcessId = 4,
                    EnrollCourseId = "EC00000002",
                    CourseContentId = "CC00000004",
                    IsComplete = false,
                },
                new UserProcess
                {
                    UserProcessId = 5,
                    EnrollCourseId = "EC00000002",
                    CourseContentId = "CC00000005",
                    IsComplete = true,
                }
                );
        }
    }
}
