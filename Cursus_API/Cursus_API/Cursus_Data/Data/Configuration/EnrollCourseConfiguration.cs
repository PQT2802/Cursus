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
    public class EnrollCourseConfiguration : IEntityTypeConfiguration<EnrollCourse>
    {
        public void Configure(EntityTypeBuilder<EnrollCourse> builder)
        {
            builder.HasData(
                new EnrollCourse
                {
                    EnrollCourseId = "EC00000001",
                    UserId = "US00000005",
                    CourseId = "CS0001",
                    CourseVersionId = 8,
                    StartEnrollDate = DateTime.Now,
                    EndEnrollDate = DateTime.Now.AddDays(5),
                    CreatedDate = DateTime.Now,
                    Status = "Enrolled",
                    Process = 0
                },
                new EnrollCourse
                {
                    EnrollCourseId = "EC00000002",
                    UserId = "US00000005",
                    CourseId = "CS0002",
                    CourseVersionId = 9,
                    StartEnrollDate = DateTime.Now,
                    EndEnrollDate = DateTime.Now.AddDays(5),
                    CreatedDate = DateTime.Now,
                    Status = "Enrolled",
                    Process = 0
                },
                 new EnrollCourse
                 {
                     EnrollCourseId = "EC00000003",
                     UserId = "US00000006",
                     CourseId = "CS0002",
                     CourseVersionId = 9,
                     StartEnrollDate = DateTime.Now,
                     EndEnrollDate = DateTime.Now.AddDays(5),
                     CreatedDate = DateTime.Now,
                     Status = "Enrolled",
                     Process = 0
                 },
                 new EnrollCourse
                 {
                     EnrollCourseId = "EC00000004",
                     UserId = "US00000008",
                     CourseId = "CS0001",
                     CourseVersionId = 8,
                     StartEnrollDate = DateTime.Now,
                     EndEnrollDate = DateTime.Now.AddDays(5),
                     CreatedDate = DateTime.Now,
                     Status = "Enrolled",
                     Process = 0
                 }


                );
        }
    }
}
