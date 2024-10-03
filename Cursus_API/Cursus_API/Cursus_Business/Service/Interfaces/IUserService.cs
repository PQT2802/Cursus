using Cursus_Business.Common;
using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IUserService
    {
        Task<string> HashPassword(string password); 
        Task<string> ConfirmMail(string key);
        Task<Result> SignUpForStudent(RegisterUserDTO userDTO);
        Task<dynamic> SignUpForIntrustor(RegisterInstructorDTO userDTO);
        Task<Result> SignIn(SignInDTO signinDTO);
        Task<dynamic> SignInWithGoogle(string email);
        Task<dynamic> GetGoogleAccount(string firebaseToken);
        Task<Result> ChangePassword(string email, string newPassword,string oldPassword);
        Task<dynamic> ConfirmToChangePassword(string key);
        Task<dynamic> SendMailComfirm(string email);
        Task<dynamic> SendMailChangePassword(string email,string newPassword);
        Task<dynamic> SendMailResetPassword(string email,string newPassword);
        Task<dynamic>UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO);

        Task<Result> GetUserProfileById(string userId);
        Task<Result> ApproveStudent(ApproveStudent approveStudent);
        Task<Result> RejectStudent(RejectStudent reject);
        Task<Result> GetListOfStudent();
    }
}
