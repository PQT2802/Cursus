using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Cursus_Data.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    UserID = "US00000001",
                    FullName = "Admin 1",
                    RoleID = 3,
                    Email = "admin1@gmail.com",
                    Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                    Phone = "+84111111111",
                    IsBan = false,
                    IsDelete = false,
                    IsMailConfirmed = true,
                    IsGoogleAccount = false
                },
                new User
                {
                    UserID = "US00000002",
                    FullName = "Admin 2",
                    RoleID = 3,
                    Email = "admin2@gmail.com",
                    Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                    Phone = "+84222222222",
                    IsBan = false,
                    IsDelete = false,
                    IsMailConfirmed = true,
                    IsGoogleAccount = false
                },
                new User
                {
                    UserID = "US00000003",
                    FullName = "Ins1",
                    RoleID = 2,
                    Email = "instructor1@gmail.com",
                    Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                    Phone = "+84111111113",
                    IsBan = false,
                    IsDelete = false,
                    IsMailConfirmed = true,
                    IsGoogleAccount = false
                },
                new User
                {
                    UserID = "US00000004",
                    FullName = "Ins2",
                    RoleID = 2,
                    Email = "instructor2@gmail.com",
                    Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                    Phone = "+84111111114",
                    IsBan = false,
                    IsDelete = false,
                    IsMailConfirmed = true,
                    IsGoogleAccount = false
                },
                new User
                {
                    UserID = "US00000005",
                    FullName = "Stu1",
                    RoleID = 1,
                    Email = "student1@gmail.com",
                    Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                    Phone = "+84111111114",
                    IsBan = false,
                    IsDelete = false,
                    IsMailConfirmed = true,
                    IsGoogleAccount = false
                },
                new User
                {
                    UserID = "US00000006",
                    FullName = "Stu2",
                    RoleID = 1,
                    Email = "student2@gmail.com",
                    Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                    Phone = "+84111111114",
                    IsBan = false,
                    IsDelete = false,
                    IsMailConfirmed = true,
                    IsGoogleAccount = false
                },
                new User
                {
                    UserID = "US00000007",
                    FullName = "Stu3",
                    RoleID = 1,
                    Email = "student3@gmail.com",
                    Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                    Phone = "+84111111114",
                    IsBan = false,
                    IsDelete = false,
                    IsMailConfirmed = true,
                    IsGoogleAccount = false
                },
                            new User
                            {
                                UserID = "US00000008",
                                FullName = "Stu4",
                                RoleID = 1,
                                Email = "quocthangjk@gmail.com",
                                Password = "ee77430e13b232f2e054ab0f5a281019bb81d76e7f8bb18bf96fdd0a242d72ef",
                                Phone = "+84111111114",
                                IsBan = false,
                                IsDelete = false,
                                IsMailConfirmed = true,
                                IsGoogleAccount = false
                            }
            );

            


        }
    }
}
