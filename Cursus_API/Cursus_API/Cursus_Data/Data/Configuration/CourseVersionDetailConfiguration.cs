using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cursus_Data.Data.Configuration
{
    public class CourseVersionDetailConfiguration : IEntityTypeConfiguration<CourseVersionDetail>
    {
        public void Configure(EntityTypeBuilder<CourseVersionDetail> builder)
        {
            builder.HasData(
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0001",
                     CourseVersionId = 1,
                     Description = "Course description here",
                     Price = 100000,
                     CreatedDate = new DateTime(2024, 06, 11),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 11),
                     UpdatedBy = "LDQ",
                     Rating = 4.0m,
                     AlreadyEnrolled = 12,
                     CourseLearningTime = "10 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0002",
                     CourseVersionId = 2,
                     Description = "Course description here",
                     Price = 200000,
                     CreatedDate = new DateTime(2024, 06, 12),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 12),
                     UpdatedBy = "LDQ",
                     Rating = 1.1m,
                     AlreadyEnrolled = 10,
                     CourseLearningTime = "20 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0003",
                     CourseVersionId = 3,
                     Description = "Course description here",
                     Price = 25000,
                     CreatedDate = new DateTime(2024, 06, 13),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 13),
                     UpdatedBy = "LDQ",
                     Rating = 3.2m,
                     AlreadyEnrolled = 30,
                     CourseLearningTime = "30 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0004",
                     CourseVersionId = 4,
                     Description = "Course description here",
                     Price = 30000,
                     CreatedDate = new DateTime(2024, 06, 13),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 13),
                     UpdatedBy = "LDQ",
                     Rating = 5.0m,
                     AlreadyEnrolled = 400,
                     CourseLearningTime = "40 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0005",
                     CourseVersionId = 5,
                     Description = "Course description here",
                     Price = 40000,
                     CreatedDate = new DateTime(2024, 06, 15),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 15),
                     UpdatedBy = "LDQ",
                     Rating = 4.8m,
                     AlreadyEnrolled = 120,
                     CourseLearningTime = "50 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0006",
                     CourseVersionId = 6,
                     Description = "Course description here",
                     Price = 20000,
                     CreatedDate = new DateTime(2024, 06, 15),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 15),
                     UpdatedBy = "LDQ",
                     Rating = 2.5m,
                     AlreadyEnrolled = 50,
                     CourseLearningTime = "60 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0007",
                     CourseVersionId = 7,
                     Description = "Course description here",
                     Price = 70000,
                     CreatedDate = new DateTime(2024, 06, 15),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 15),
                     UpdatedBy = "LDQ",
                     Rating = 2.7m,
                     AlreadyEnrolled = 83,
                     CourseLearningTime = "70 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0008",
                     CourseVersionId = 8,
                     Description = "Course description here",
                     Price = 90000,
                     CreatedDate = new DateTime(2024, 06, 15),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 15),
                     UpdatedBy = "LDQ",
                     Rating = 4.5m,
                     AlreadyEnrolled = 182,
                     CourseLearningTime = "60 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0009",
                     CourseVersionId = 9,
                     Description = "Course description here",
                     Price = 33000,
                     CreatedDate = new DateTime(2024, 06, 15),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 15),
                     UpdatedBy = "LDQ",
                     Rating = 3.7m,
                     AlreadyEnrolled = 53,
                     CourseLearningTime = "70 hours",
                     IsDelete = false
                 },
                 new CourseVersionDetail
                 {
                     CourseVersionDetailId = "CVD0010",
                     CourseVersionId = 10,
                     Description = "Course description here",
                     Price = 29.99,
                     CreatedDate = new DateTime(2024, 06, 15),
                     CreatedBy = "LDQ",
                     UpdatedDate = new DateTime(2024, 06, 15),
                     UpdatedBy = "LDQ",
                     Rating = 4.2m,
                     AlreadyEnrolled = 95,
                     CourseLearningTime = "15 hours",
                     IsDelete = false
                 },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0011",
                    CourseVersionId = 11,
                    Description = "Course description here",
                    Price = 39.99,
                    CreatedDate = new DateTime(2024, 06, 15),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 15),
                    UpdatedBy = "LDQ",
                    Rating = 3.5m,
                    AlreadyEnrolled = 72,
                    CourseLearningTime = "25 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0012",
                    CourseVersionId = 12,
                    Description = "Course description here",
                    Price = 49.99,
                    CreatedDate = new DateTime(2024, 06, 16),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 16),
                    UpdatedBy = "LDQ",
                    Rating = 4.8m,
                    AlreadyEnrolled = 150,
                    CourseLearningTime = "35 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0013",
                    CourseVersionId = 13,
                    Description = "Course description here",
                    Price = 59.99,
                    CreatedDate = new DateTime(2024, 06, 16),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 16),
                    UpdatedBy = "LDQ",
                    Rating = 3.9m,
                    AlreadyEnrolled = 220,
                    CourseLearningTime = "45 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0014",
                    CourseVersionId = 14,
                    Description = "Course description here",
                    Price = 69.99,
                    CreatedDate = new DateTime(2024, 06, 16),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 16),
                    UpdatedBy = "LDQ",
                    Rating = 2.1m,
                    AlreadyEnrolled = 40,
                    CourseLearningTime = "55 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0015",
                    CourseVersionId = 15,
                    Description = "Course description here",
                    Price = 79.99,
                    CreatedDate = new DateTime(2024, 06, 16),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 16),
                    UpdatedBy = "LDQ",
                    Rating = 4.6m,
                    AlreadyEnrolled = 180,
                    CourseLearningTime = "65 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0016",
                    CourseVersionId = 16,
                    Description = "Course description here",
                    Price = 89.99,
                    CreatedDate = new DateTime(2024, 06, 17),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 17),
                    UpdatedBy = "LDQ",
                    Rating = 3.2m,
                    AlreadyEnrolled = 90,
                    CourseLearningTime = "75 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0017",
                    CourseVersionId = 17,
                    Description = "Course description here",
                    Price = 99.99,
                    CreatedDate = new DateTime(2024, 06, 17),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 17),
                    UpdatedBy = "LDQ",
                    Rating = 4.9m,
                    AlreadyEnrolled = 300,
                    CourseLearningTime = "85 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0018",
                    CourseVersionId = 18,
                    Description = "Course description here",
                    Price = 109.99,
                    CreatedDate = new DateTime(2024, 06, 17),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 17),
                    UpdatedBy = "LDQ",
                    Rating = 3.8m,
                    AlreadyEnrolled = 110,
                    CourseLearningTime = "95 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0019",
                    CourseVersionId = 19,
                    Description = "Course description here",
                    Price = 119.99,
                    CreatedDate = new DateTime(2024, 06, 18),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 18),
                    UpdatedBy = "LDQ",
                    Rating = 2.7m,
                    AlreadyEnrolled = 60,
                    CourseLearningTime = "105 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0020",
                    CourseVersionId = 20,
                    Description = "Course description here",
                    Price = 129.99,
                    CreatedDate = new DateTime(2024, 06, 18),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 18),
                    UpdatedBy = "LDQ",
                    Rating = 4.5m,
                    AlreadyEnrolled = 250,
                    CourseLearningTime = "115 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0021",
                    CourseVersionId = 21,
                    Description = "Course description here",
                    Price = 139.99,
                    CreatedDate = new DateTime(2024, 06, 18),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 18),
                    UpdatedBy = "LDQ",
                    Rating = 3.1m,
                    AlreadyEnrolled = 95,
                    CourseLearningTime = "125 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0022",
                    CourseVersionId = 22,
                    Description = "Course description here",
                    Price = 149.99,
                    CreatedDate = new DateTime(2024, 06, 19),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 19),
                    UpdatedBy = "LDQ",
                    Rating = 4.2m,
                    AlreadyEnrolled = 180,
                    CourseLearningTime = "135 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0023",
                    CourseVersionId = 23,
                    Description = "Course description here",
                    Price = 159.99,
                    CreatedDate = new DateTime(2024, 06, 19),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 19),
                    UpdatedBy = "LDQ",
                    Rating = 2.8m,
                    AlreadyEnrolled = 70,
                    CourseLearningTime = "145 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0024",
                    CourseVersionId = 24,
                    Description = "Course description here",
                    Price = 169.99,
                    CreatedDate = new DateTime(2024, 06, 19),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 19),
                    UpdatedBy = "LDQ",
                    Rating = 4.7m,
                    AlreadyEnrolled = 300,
                    CourseLearningTime = "155 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0025",
                    CourseVersionId = 25,
                    Description = "Course description here",
                    Price = 179.99,
                    CreatedDate = new DateTime(2024, 06, 20),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 20),
                    UpdatedBy = "LDQ",
                    Rating = 3.5m,
                    AlreadyEnrolled = 130,
                    CourseLearningTime = "165 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0026",
                    CourseVersionId = 26,
                    Description = "Course description here",
                    Price = 189.99,
                    CreatedDate = new DateTime(2024, 06, 20),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 20),
                    UpdatedBy = "LDQ",
                    Rating = 4.9m,
                    AlreadyEnrolled = 400,
                    CourseLearningTime = "175 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0027",
                    CourseVersionId = 27,
                    Description = "Course description here",
                    Price = 199.99,
                    CreatedDate = new DateTime(2024, 06, 20),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 20),
                    UpdatedBy = "LDQ",
                    Rating = 3.9m,
                    AlreadyEnrolled = 210,
                    CourseLearningTime = "185 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0028",
                    CourseVersionId = 28,
                    Description = "Course description here",
                    Price = 209.99,
                    CreatedDate = new DateTime(2024, 06, 21),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 21),
                    UpdatedBy = "LDQ",
                    Rating = 2.6m,
                    AlreadyEnrolled = 90,
                    CourseLearningTime = "195 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0029",
                    CourseVersionId = 29,
                    Description = "Course description here",
                    Price = 219.99,
                    CreatedDate = new DateTime(2024, 06, 21),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 21),
                    UpdatedBy = "LDQ",
                    Rating = 4.3m,
                    AlreadyEnrolled = 240,
                    CourseLearningTime = "205 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0030",
                    CourseVersionId = 30,
                    Description = "Course description here",
                    Price = 229.99,
                    CreatedDate = new DateTime(2024, 06, 21),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 21),
                    UpdatedBy = "LDQ",
                    Rating = 3.7m,
                    AlreadyEnrolled = 150,
                    CourseLearningTime = "215 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0031",
                    CourseVersionId = 31,
                    Description = "Course description here",
                    Price = 239.99,
                    CreatedDate = new DateTime(2024, 06, 22),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 22),
                    UpdatedBy = "LDQ",
                    Rating = 2.9m,
                    AlreadyEnrolled = 80,
                    CourseLearningTime = "225 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0032",
                    CourseVersionId = 32,
                    Description = "Course description here",
                    Price = 249.99,
                    CreatedDate = new DateTime(2024, 06, 22),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 22),
                    UpdatedBy = "LDQ",
                    Rating = 4.6m,
                    AlreadyEnrolled = 310,
                    CourseLearningTime = "235 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0033",
                    CourseVersionId = 33,
                    Description = "Course description here",
                    Price = 259.99,
                    CreatedDate = new DateTime(2024, 06, 22),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 22),
                    UpdatedBy = "LDQ",
                    Rating = 3.4m,
                    AlreadyEnrolled = 120,
                    CourseLearningTime = "245 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0034",
                    CourseVersionId = 34,
                    Description = "Course description here",
                    Price = 269.99,
                    CreatedDate = new DateTime(2024, 06, 23),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 23),
                    UpdatedBy = "LDQ",
                    Rating = 4.8m,
                    AlreadyEnrolled = 280,
                    CourseLearningTime = "255 hours",
                    IsDelete = false
                },
                new CourseVersionDetail
                {
                    CourseVersionDetailId = "CVD0035",
                    CourseVersionId = 35,
                    Description = "Course description here",
                    Price = 279.99,
                    CreatedDate = new DateTime(2024, 06, 23),
                    CreatedBy = "LDQ",
                    UpdatedDate = new DateTime(2024, 06, 23),
                    UpdatedBy = "LDQ",
                    Rating = 3.3m,
                    AlreadyEnrolled = 200,
                    CourseLearningTime = "265 hours",
                    IsDelete = false
                }

             );
        }
    }
}