using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task <int> CreateUserAsync(User user);
        Task<string> AutoGenerateUserID();
        Task<bool> CheckEmail(string email);
        Task<bool> CheckPhone(string phone);
        Task<bool> CheckName(string name);
        Task<int> UpdateMailConfirm (string email);
        Task<User> GetUserByEmail(string email);
      //  Task<dynamic> GetUserByEmail(string email);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> SignIn(SignInDTO request);
        Task<bool> CheckUserExistByFullName(string fullName);
        Task<int> UpdatePasswordByEmail(string email, string password);
        Task<string> GetNameByUserId(string userId);
        Task UpdateUserAsync(User user);


        Task<dynamic> GetUserProfileById(string userId);

        Task<User> GetUserIsStudent(string userID);
        Task<List<StudentDTO>> GetStudentsAsync();
        Task DeleteUser();
    }
}
