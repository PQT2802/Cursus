using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IUserDetailRepository
    {
        Task AddUserDetailAsync(UserDetail userDetail);
        Task<string> AutoGenerateUserDetailID();
        Task<bool> CheckPhone(string phone);
        bool CheckDOB(DateTime dob);
        Task UpdateUserDetailAsync(UserDetail userDetail);
        Task<UserDetail> GetUserDetailByIdAsync(string userId);
        Task SaveLoginDate(DateTime loginDate, string userId);
    }
}
