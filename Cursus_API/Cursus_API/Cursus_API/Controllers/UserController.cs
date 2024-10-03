using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Cursus_Data.Context;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Cursus_Data.Repositories.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Business.Service.Implements;
using Microsoft.AspNetCore.Cors;
using Cursus_Business.Common;
using Microsoft.AspNetCore.Authorization;
using Cursus_Data.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Http.HttpResults;
using Cursus_Business.Exceptions.ErrorHandler;

namespace Cursus_API.Controllers
{
    [ApiController]
    [Route("api/v1.0/")]
    [EnableCors]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserBehaviorService _userBehaviorService;
        private readonly IMailServiceV2 _mailServiceV2;
        private readonly IUserDetailService _userDetailService;
        private readonly IExcelExportService _excelExportService;

        public UserController(IConfiguration configuration, IUserService userService,
            IUserDetailService userDetailService, ITokenService tokenService, IUserBehaviorService userBehaviorService,
            IRefreshTokenService refreshTokenService,
            IMailServiceV2 mailServiceV2, IExcelExportService excelExportService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _mailServiceV2 = mailServiceV2;
            _userBehaviorService = userBehaviorService;
            _userDetailService = userDetailService;
            _excelExportService = excelExportService;
        }

        [HttpPost("User/sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            Result result = await _userService.SignIn(signInDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("User/sign-up-for-student")]
        public async Task<IActionResult> SignUpForStudent([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                var result = await _userService.SignUpForStudent(registerUserDTO);
                if (result.IsSuccess)
                {
                    Result resultMail = await _userService.SendMailComfirm(registerUserDTO.Email);
                    if (resultMail.IsSuccess)
                    {
                        return Ok(Result.SuccessWithObject(new { Message = "Sign up successfully,please check and confirm your mail" }));
                    }
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("User/sign-up-for-instructor")]
        public async Task<IActionResult> SignUpForInstructor([FromForm] RegisterInstructorDTO registerUserDTO)
        {
            try
            {
                var result = await _userService.SignUpForIntrustor(registerUserDTO);
                if (result.IsSuccess)
                {
                    Result resultMail = await _userService.SendMailComfirm(registerUserDTO.Email);
                    if (resultMail.IsSuccess)
                    {
                        return Ok(Result.SuccessWithObject(new { Message = "Sign up successfully,please check and confirm your mail" }));
                    }
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("user/mail-coinfirm")]
        public async Task<IActionResult> MailConfirmation(string key)
        {
            string sMessage = await _userService.ConfirmMail(key);
            return Content(sMessage, "text/html");
        }

        [HttpPost("User/google-account")]
        public async Task<IActionResult> GoogleAccount(string firebaseToken)
        {

            try
            {
                var result = await _userService.GetGoogleAccount(firebaseToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("User/change-password")]
        public async Task<IActionResult> ChangePassword(string newPassword, string oldPassword)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _userService.ChangePassword(c.Email, newPassword, oldPassword);
            if (result.IsSuccess)
            {
                Result resultMail = await _userService.SendMailChangePassword(c.Email, newPassword);
                if (resultMail.IsSuccess)
                {
                    return Ok(Result.SuccessWithObject(new { Message = "Please check and confirm your changing" }));
                }
            }
            return BadRequest(result);
        }
        [Authorize]
        [HttpPut("User/reset-password")]
        public async Task<IActionResult> ResetPassword(ChangePasswordDTO changePasswordDTO)
        {
            Result result = await _userService.ChangePassword(changePasswordDTO.Email, changePasswordDTO.NewPassword, changePasswordDTO.OldPassword);
            if (result.IsSuccess)
            {
                Result resultMail = await _userService.SendMailChangePassword(changePasswordDTO.Email, changePasswordDTO.NewPassword);
                if (resultMail.IsSuccess)
                {
                    return Ok(Result.SuccessWithObject(new { Message = "Please check and confirm your changing" }));
                }
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("User/changeConfirmed")]
        public async Task<IActionResult> ChangePasswordMailConfirmed(string key)
        {
            string sMessage = await _userService.ConfirmToChangePassword(key);
            return Content(sMessage, "text/html");
        }

        [Authorize]
        [HttpPost("User/profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDTO updateUserProfileDTO)
        {
            Result result = await _userService.UpdateUserProfile(updateUserProfileDTO);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("User/profile/{userId}")]
        public async Task<IActionResult> GetUserProfileById(string userId)
        {           
            Result result = await _userService.GetUserProfileById(userId);
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("User/edit")]
        public async Task<IActionResult> UpdateUserDetail(UpdateUserDetailDTOcs user)
        {
            Result result = await _userDetailService.UpdateUserDetailAsync(user);
            if (!result.IsSuccess && result.Error.Code == UserErrors.UserIsNotExist.Code)
            {
                return NotFound(result);
            }
            return Ok(result);

        }

        //[Authorize(Policy = "RequireAdminRole")]
        [HttpGet("User/student-detail-list")]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _userService.GetListOfStudent();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("User/active")]
        public async Task<IActionResult> ApproveUserIsStudent(ApproveStudent approveStudent)
        {
            Result result = await _userService.ApproveStudent(approveStudent);
            if (result.IsFailure)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("User/deactive")]
        public async Task<IActionResult> RejectUserIsStudent(RejectStudent reject)
        {
            Result result = await _userService.RejectStudent(reject);
            return Ok(result);
        }

        
        [HttpPost("User/export")]
        public async Task<IActionResult> ExportUsers()
        {
            var result = await _userService.GetListOfStudent();
            if (result.IsSuccess)
            {
                var users = (List<StudentDTO>)result.Object;
                var exportResult = await _excelExportService.ExportToExcelAsync(users, "Users");
                return Ok(exportResult);
            }
            else
            {
                return BadRequest(new
                {
                    error = result.Error
                });
            }
        }

    }


}