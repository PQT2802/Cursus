using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cursus_Data.Data.Configuration
{
    public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasData(
                new UserDetail
                {
                    UserDetailID = "UD00000001",
                    UserID = "US00000001",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "123 TP HCM",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                },
                new UserDetail
                {
                    UserDetailID = "UD00000002",
                    UserID = "US00000002",
                    DateOfBirth = new DateTime(1991, 2, 2),
                    Address = "456 TP HN",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                },
                new UserDetail
                {
                    UserDetailID = "UD00000003",
                    UserID = "US00000003",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "123 TP NT",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                }, new UserDetail
                {
                    UserDetailID = "UD00000004",
                    UserID = "US00000004",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "1234 TP HCM",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                },
                new UserDetail
                {
                    UserDetailID = "UD00000005",
                    UserID = "US00000005",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "1234 TP HCM",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                },
                new UserDetail
                {
                    UserDetailID = "UD00000006",
                    UserID = "US00000006",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "1234 TP HCM",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                },
                new UserDetail
                {
                    UserDetailID = "UD00000007",
                    UserID = "US00000007",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "1234 TP HCM",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                },
                new UserDetail
                {
                    UserDetailID = "UD00000008",
                    UserID = "US00000008",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "1234 TP HCM",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true
                }
            );
        }
    }
}
