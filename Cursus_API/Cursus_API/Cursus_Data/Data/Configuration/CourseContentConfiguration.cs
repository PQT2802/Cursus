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
    public class CourseContentConfiguration : IEntityTypeConfiguration<CourseContent>
    {
        public void Configure(EntityTypeBuilder<CourseContent> builder)
        {
            builder.HasData(
                
                new CourseContent
                {
                    CourseContentId = "CC00000001",
                    CourseVersionDetailId = "CVD0008",
                    Title = "Introduction",
                    Url = "Link file",
                    Time = 2,
                    Type = "Document",
                    CreatedBy = "INS00000001",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "INS00000001",
                    UpdatedDate = DateTime.Now,
                    IsDelete = false,
                },
                new CourseContent
                {
                    CourseContentId = "CC00000002",
                    CourseVersionDetailId = "CVD0008",
                    Title = "Introduction",
                    Url = "Link file",
                    Time = 2,
                    Type = "Video",
                    CreatedBy = "INS00000001",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "INS00000001",
                    UpdatedDate = DateTime.Now,
                    IsDelete = false,
                },
                new CourseContent
                {
                    CourseContentId = "CC00000003",
                    CourseVersionDetailId = "CVD0008",
                    Title = "Introduction",
                    Url = "Link file",
                    Time = 2,
                    Type = "Silde",
                    CreatedBy = "INS00000001",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "INS00000001",
                    UpdatedDate = DateTime.Now,
                    IsDelete = false,
                },
                new CourseContent
                {
                    CourseContentId = "CC00000004",
                    CourseVersionDetailId = "CVD0009",
                    Title = "Introduction",
                    Url = "Link file",
                    Time = 2,
                    Type = "Silde",
                    CreatedBy = "INS00000001",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "INS00000001",
                    UpdatedDate = DateTime.Now,
                    IsDelete = false,
                },
                new CourseContent
                {
                    CourseContentId = "CC00000005",
                    CourseVersionDetailId = "CVD0009",
                    Title = "Introduction",
                    Url = "Link file",
                    Time = 2,
                    Type = "Silde",
                    CreatedBy = "INS00000001",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "INS00000001",
                    UpdatedDate = DateTime.Now,
                    IsDelete = false,
                }
                );
        }
    }
}
