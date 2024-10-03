using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IUserBehaviorRepository
    {
        Task<bool> CheckUserBehaviorExist(string UserId);
        Task AddUserBehavior(string UserId, string Key, DateTime dateTime);
        Task UpdateUserBehavior(string UserId, string Key, DateTime dateTime);
        Task<UserBehavior> GetUserBehavior(string UserId);
        Task<List<string>> GetBehaviorOfUser(string UserId);
    }
}

/*
    GetBehaviorOfUser(id) => List cac key behavior
=>  GetCategoryByUserBehavior(List cac key behavior) => List cac Category
=>  GetCoursesByCategory(List cac Category) => List cac course
 */
