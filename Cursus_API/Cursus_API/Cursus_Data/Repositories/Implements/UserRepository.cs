using Azure.Core;
using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly LMS_CursusDbContext _context;
        public UserRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> AutoGenerateUserID()
        {
            int count = await _context.Users.CountAsync() + 1;
            string Us = "US";
            string paddedNumber = count.ToString().PadLeft(8, '0');
            string UserId = Us + paddedNumber;
            return UserId;
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }


        public async Task<int> UpdateMailConfirm(string email)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync(
        $"UPDATE [User] SET IsMailConfirmed = 1 WHERE Email = {email}");
        }
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserID == userId);

        }

        public async Task<bool> CheckPhone(string phone)
        {
            return await _context.Users.AnyAsync(x => x.Phone == phone);
        }

        public async Task<bool> CheckName(string fullName)
        {
            return await _context.Users.AnyAsync(x => x.FullName == fullName);
        }

        public async Task<int> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<User> SignIn(SignInDTO request)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Password == request.Password && x.Email == request.Email);

        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        //public async Task<dynamic> GetUserByEmail(string email)
        //{
        //    return await _context.Users
        //        .Where(x => x.Email == email)
        //        .Select(x => new
        //        {
        //            UserID = x.UserID,
        //            FullName = x.FullName,
        //            RoleID = x.RoleID,
        //            Email = x.Email,
        //            Phone = x.Phone,
        //            IsBan = x.IsBan,
        //            IsDelete = x.IsDelete,
        //            IsMailConfirmed = x.IsMailConfirmed
        //        })
        //        .FirstOrDefaultAsync();
        //}

        public async Task<bool> CheckUserExistByFullName(string fullName)
        {
            return await _context.Users.AnyAsync(x => x.FullName == fullName);
        }

        public async Task<int> UpdatePasswordByEmail(string email, string password)
        {
            return await _context.Database.ExecuteSqlInterpolatedAsync(
        $"UPDATE [User] SET Password = {password} WHERE Email = {email}");
        }


        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task<dynamic> GetUserProfileById(string userId)
        {
            try
            {
                var currentUser = await _context.Users
                    .Include(x => x.UserDetail)
                    .Select(user => new
                    {
                        user.UserID,
                        user.IsBan,
                        user.IsDelete,
                        user.IsMailConfirmed,
                        user.FullName,
                        user.Email,
                        user.Phone,
                        user.RoleID,
                        // Include other necessary fields
                        UserDetail = user.UserDetail != null ? new
                        {
                            user.UserDetail.UserDetailID,
                            // Include other necessary fields
                            user.UserDetail.Address,
                            user.UserDetail.DateOfBirth,
                            user.UserDetail.Avatar,

                        } : null
                    })
                    .FirstOrDefaultAsync(u => u.UserID == userId);

                if (currentUser == null)
                {
                    // Handle the case where the user is not found
                    return null; // or throw an exception
                }

                return currentUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> GetNameByUserId(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            return user.FullName;

        }
        public async Task<User> GetUserIsStudent(string userID)
        {
            var user = await _context.Users
                .Include(c => c.UserDetail) // Include the UserDetail navigation property
                .Where(u => u.UserDetail.UserID == userID && u.RoleID == 1) // Filter by UserID and RoleID
                .FirstOrDefaultAsync();
            return user;
        }
        public async Task<int> GetEnrollCourseCountAsync(string userId)
        {
            return await _context.EnrollCourses
                                 .Include(c => c.User)
                                 .Where(e => e.UserId == userId && e.Process == 0 && e.User.RoleID == 1)
                                 .CountAsync();
        }

        public async Task<int> GetNumberProgressCourseAsync(string userId)
        {    
            return await _context.EnrollCourses
                                 .Include(c => c.User)
                                 .Where(e => e.UserId == userId && e.Process > 0 && e.User.RoleID == 1)
                                 .CountAsync();
        }


        public async Task<List<StudentDTO>> GetStudentsAsync()
        {
            var users = await _context.Users
          .Where(u => u.RoleID == 1)
          .ToListAsync();

            var studentDTOs = new List<StudentDTO>();

            foreach (var user in users)
            {
                var studentDTO = new StudentDTO
                {
                    FullName = user.FullName,
                    Phone = user.Phone,
                    Email = user.Email,
                    NumberJoinCourse = await GetEnrollCourseCountAsync(user.UserID),
                    NumberProgressCourse = await GetNumberProgressCourseAsync(user.UserID)
                };

                studentDTOs.Add(studentDTO);
            }

            return studentDTOs;
        }

        private async Task<List<User>> GetListOfUserNotSignin()
        {
            DateTime dateTime = DateTime.Now.AddMonths(-3); 
            var list = await _context.Users.Include(x => x.UserDetail).Where(x => x.UserDetail.UpdatedDate <= dateTime ).ToListAsync();
            return list;
        }

        public async Task DeleteUser()
        {
            List<User> users = await GetListOfUserNotSignin();
            foreach (var user in users)
            {
                user.IsDelete = true;
            }
            await _context.SaveChangesAsync();
        }
    }

}