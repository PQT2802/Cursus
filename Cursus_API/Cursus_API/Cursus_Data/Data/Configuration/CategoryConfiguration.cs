using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cursus_Data.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    CategoryId = "CT0001",
                    Name = "C#",
                    Description = "A modern, object-oriented programming language developed by Microsoft.",
                    ParentId = null,
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 01, 10, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 01, 10, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0002",
                    Name = "Java",
                    Description = "A high-level, class-based, object-oriented programming language.",
                    ParentId = null,
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 02, 11, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 02, 11, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0003",
                    Name = "Python",
                    Description = "An interpreted, high-level, general-purpose programming language.",
                    ParentId = null,
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 03, 12, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 03, 12, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0004",
                    Name = "C++",
                    Description = "A general-purpose programming language created as an extension of the C programming language.",
                    ParentId = "CT0005",
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0005",
                    Name = "C",
                    Description = "A general-purpose programming language created as an extension of the C programming language.",
                    ParentId = null,
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0006",
                    Name = "Go",
                    Description = "A general-purpose programming language created as an extension of the C programming language.",
                    ParentId = "CT0004",
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0007",
                    Name = "Java Spring boost",
                    Description = "A general-purpose programming language created as an extension of the Java programming language.",
                    ParentId = "CT0002",
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0008",
                    Name = "Java themleaf",
                    Description = "A general-purpose programming language created as an extension of the Java programming language.",
                    ParentId = "CT0002",
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0009",
                    Name = "NodeJs",
                    Description = "A general-purpose programming language created as an extension of the Java programming language.",
                    ParentId = null,
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0010",
                    Name = "ReactJs",
                    Description = "A general-purpose programming language created as an extension of the Java programming language.",
                    ParentId = "CT0009",
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                }, 
                new Category
                {
                    CategoryId = "CT0011",
                    Name = "ReactJs Native",
                    Description = "A general-purpose programming language created as an extension of the Java programming language.",
                    ParentId = "CT0010",
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                },
                new Category
                {
                    CategoryId = "CT0012",
                    Name = "ReactJs Native Child",
                    Description = "A general-purpose programming language created as an extension of the Java programming language.",
                    ParentId = "CT0011",
                    Status = "Active",
                    IsDelete = false,
                    CreateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    UpdateDate = new DateTime(2023, 01, 04, 13, 00, 00),
                    CreateBy = "Admin",
                    UpdateBy = "Admin"
                }
            );
        }
    }
}
