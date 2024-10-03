using Cursus_Data.Context;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly LMS_CursusDbContext _context;

        public UserDetailRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }
        public async Task AddUserDetailAsync(UserDetail userDetail)
        {
            await _context.UserDetails.AddAsync(userDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<string> AutoGenerateUserDetailID()
        {
            int count = await _context.UserDetails.CountAsync() + 1;
            string Ud = "UD";
            string paddedNumber = count.ToString().PadLeft(8, '0');
            string UserDetailId = Ud + paddedNumber;
            return UserDetailId;
        }

        public bool CheckDOB(DateTime dob)
        {
            return dob <= DateTime.Now.AddYears(-5);
        }

        public async Task<bool> CheckPhone(string phone)
        {
          // return await _context.UserDetails.AnyAsync(x => x.Phone == phone);
          return false;
        }

        public async Task<UserDetail> GetUserDetailByIdAsync(string userId)
        {
            return await _context.UserDetails.Include(c => c.User).SingleOrDefaultAsync(x => x.UserID == userId && x.User.RoleID == 1);
        }

        public async Task SaveLoginDate(DateTime loginDate, string userId)
        {
            UserDetail userDetail = await _context.UserDetails.Where(x => x.UserID == userId).FirstOrDefaultAsync();
            userDetail.UpdatedDate = loginDate;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserDetailAsync(UserDetail userDetail)
        {
            _context.UserDetails.Update(userDetail);
            await _context.SaveChangesAsync();
        }


    }
}
