using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Metrics;
using Cursus_Business.Common.Pattern;
using Cursus_Data.Models.DTOs.CommonObject;

namespace Cursus_Business.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IMailService _mailService;
        private readonly IUserDetailService _userDetailService;
        private readonly IWalletService _walletService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly LMS_CursusDbContext _context;
        private readonly IFirebaseService _firebaseService;
        private readonly IInstructorService _instructorService;

        private readonly IMailServiceV2 _mailServiceV2;
        private readonly IMailServiceV3 _mailServiceV3;
        private readonly IEmailTemplateRepsository _emailTemplateRepsository;
        private readonly IMailRepository _mailRepository;

        public UserService(IUserRepository userRepository, IUserDetailRepository userDetailRepository, IMailService mailService, IUserDetailService userDetailService,
              IWalletService walletService, ITokenService tokenService, IRefreshTokenService refreshTokenService,
              LMS_CursusDbContext context, IFirebaseService firebaseService, IInstructorService instructorService, IMailServiceV2 mailServiceV2
            , IMailServiceV3 mailServiceV3, IEmailTemplateRepsository emailTemplateRepsository,IMailRepository mailRepository)
        {
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
            _mailService = mailService;
            _userDetailService = userDetailService;
            _walletService = walletService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _context = context;
            _firebaseService = firebaseService;
            _instructorService = instructorService;
            _mailServiceV2 = mailServiceV2;
            _mailServiceV3 = mailServiceV3;
            _emailTemplateRepsository = emailTemplateRepsository;
            _mailRepository = mailRepository;
        }


        public Task<string> HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            return Task.Run(() =>
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash returns byte array
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Convert byte array to a string
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            });
        }
        public async Task<string> ConfirmMail(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key)) return "Invalid value";
                var decryptEmail = Encryption.Decrypt(key);
                var result = await _userRepository.CheckEmail(decryptEmail);
                if (!result)
                {
                    return Message.InvalidUser;
                }
                else
                {
                    var result2 = await _userRepository.UpdateMailConfirm(decryptEmail);
                }
                return _mailService.ReturnToLoginPage();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<Result> SignUpForStudent(RegisterUserDTO userDTO)
        {
            try
            {
                List<Error> errors = new List<Error>();
                if (await _userRepository.CheckEmail(userDTO.Email))
                {
                    errors.Add(UserErrors.EmailAlreadyUsed(userDTO.Email));
                }
                if (Validator.IsValidPhone(userDTO.Phone))
                {
                    userDTO.Phone = ConvertToInternationalPhoneFormat(userDTO.Phone);
                }
                if (await _userRepository.CheckPhone(userDTO.Phone))
                {
                    errors.Add(UserErrors.PhoneAlreadyUsed(userDTO.Phone));
                }
                //fullname
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.FullName)
                    ? new Error[] { UserErrors.FullnameIsEmpty }//User.FullnameIsEmpty
                    : !Validator.IsValidName(userDTO.FullName)
                        ? new Error[] { UserErrors.FullnameIsInvalid(userDTO.FullName) }//User.FullnameIsInvalid
                        : Enumerable.Empty<Error>()
                    );
                //email
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Email)
                    ? new Error[] { UserErrors.EmailIsEmpty }//User.EmailIsEmpty
                    : !Validator.IsValidEmail(userDTO.Email)
                        ? new Error[] { UserErrors.EmailIsInvalid(userDTO.Email) }//User.EmailIsInvalid
                        : Enumerable.Empty<Error>()
                    );
                //password
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Password)
                    ? new Error[] { UserErrors.PasswordIsEmpty }//User.PasswordIsEmpty
                    : !Validator.IsMinLengthPassword(userDTO.Password)
                        ? new Error[] { UserErrors.PasswordMinLength }//User.PasswordMinLength
                        : !Validator.IsValidPassword(userDTO.Password)
                            ? new Error[] { UserErrors.PasswordIsInvalid }//User.PasswordIsInvalid
                            : Enumerable.Empty<Error>()
                    );
                //DOB
                errors.AddRange(
                    !Validator.IsValidDOB(userDTO.DateOfBirth)
                    ? new Error[] { UserErrors.DOBIsInvalid } //User.DOBIsInvalid
                    : !Validator.IsEnoughAge(userDTO.DateOfBirth)
                        ? new Error[] { UserErrors.DOBIsNotEnogh } //User.DOBIsNotEnogh
                        : Enumerable.Empty<Error>()
                    );
                //phone
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Phone)
                    ? new Error[] { UserErrors.PhoneIsEmpty }//User.PasswordIsEmpty
                    : !Validator.CheckPhoneLength(userDTO.Phone)
                        ? new Error[] { UserErrors.PhoneLength }//User.PhoneLength
                        : !Validator.IsValidPhone(userDTO.Phone)
                            ? new Error[] { UserErrors.PhoneIsInvalid }//User.PhoneIsInvalid
                            : Enumerable.Empty<Error>()
                    );
                //address
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Address)
                    ? new Error[] { UserErrors.AddressIsEmpty }//User.AddressIsEmpty
                   : Enumerable.Empty<Error>()
                   );

                if (errors == null || !errors.Any())
                {
                    var userId = await _userRepository.AutoGenerateUserID();
                    var hashpassword = await HashPassword(userDTO.Password);
                    var userDetailId = await _userDetailRepository.AutoGenerateUserDetailID();
                    User newUser = new User()
                    {
                        UserID = userId,
                        Email = userDTO.Email,
                        FullName = userDTO.FullName,
                        Phone = userDTO.Phone,
                        Password = hashpassword,
                        RoleID = 1,
                        IsBan = false,
                        IsDelete = false,
                        IsMailConfirmed = false,
                        IsGoogleAccount = false,
                    };
                    await _userRepository.AddUserAsync(newUser);
                    var userDetail = await _userDetailService.RegisterUserDetailAsync(userDTO, userId);
                    var userWallet = await _walletService.RegisterUserWalletlAsync(userId);
                    if (userDetail == true && userWallet == true)
                    {
                        return Result.Success();
                    }
                }
                return Result.Failures(errors);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<Result> SignIn(SignInDTO signinDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(signinDTO.Email))
                {
                    return Result.Failure(SignInErrors.InputEmpty());
                }
                if (string.IsNullOrEmpty(signinDTO.Password))
                {
                    return Result.Failure(SignInErrors.InputEmpty());
                }
                if (!await _userRepository.CheckEmail(signinDTO.Email))
                {
                    return Result.Failure(SignInErrors.InputFieldWrong());
                }
                signinDTO.Password = await HashPassword(signinDTO.Password);
                var user = await _userRepository.SignIn(signinDTO);
                if (user == null)
                {
                    return Result.Failure(SignInErrors.InputFieldWrong());
                }
                if (user != null)
                {
                    if (user.IsMailConfirmed == false)
                    {
                        var getUser = await _userRepository.GetUserByEmail(user.Email);
                        var mailTemplate = await _emailTemplateRepsository.GetEmailTemplateByType(MailType.ConfirmationAccount);

                        var getUserEmail = await _mailRepository.GetConfirmationMailByUserIdAsync(getUser.Email, mailTemplate.EmailTemplateId);
                        if (getUserEmail != null && getUserEmail.CreateDate > DateTime.UtcNow.AddHours(-24))
                        {
                            // Mail was created within the last 24 hours
                            return Result.Failure(new Error("Email", "A confirmation email has already been sent in the last 24 hours."));
                        }
                        var mailObject = new MailClass()
                        {
                            Subject = "Mail Confirmation",
                            Body = _mailService.GetMailBody(signinDTO.Email),
                            ToMailIds = new List<string>()
                            {
                                signinDTO.Email,
                            }
                        };
                        var sendMail = _mailService.SendMail(mailObject);
                        return Result.Failure(new Error("Email", "Your mail has not comfirmed yet, please confirm to sign in"));
                    }
                    if (user.IsDelete == true)
                    {
                        return Result.Failure(SignInErrors.AccountIsDelete());
                    }
                    if (user.IsBan == true)
                    {
                        return Result.Failure(SignInErrors.AccountIsBan());
                    }
                }

                var c = new CurrentUserObject
                {
                    UserId = user.UserID,
                    Fullname = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    RoleId = user.RoleID
                };

                var token = await _tokenService.GenerateTokenAsync(c);
                var accessToken = await _tokenService.GenerateAccessTokenAsync(token);
                var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync();
                await _refreshTokenService.SaveRefreshTokenAsync(token.Id, user.UserID, refreshToken);

                var tokenDTO = new TokenDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
                DateTime loginDate = DateTime.Now;
                await _userDetailRepository.SaveLoginDate(loginDate, user.UserID);
                return Result.SuccessWithObject(tokenDTO);

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }
        public async Task<dynamic> SignInWithGoogle(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(email);
                if (user == null)
                {
                    return Result.Failure(SignInErrors.InputFieldWrong());
                }
                if (user != null)
                {
                    if (user.IsDelete == true)
                    {
                        return Result.Failure(SignInErrors.AccountIsDelete());
                    }
                    if (user.IsBan == true)
                    {
                        return Result.Failure(SignInErrors.AccountIsBan());
                    }
                }

                var c = new CurrentUserObject
                {
                    UserId = user.UserID,
                    Fullname = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    RoleId = user.RoleID
                };

                var token = await _tokenService.GenerateTokenAsync(c);
                var accessToken = await _tokenService.GenerateAccessTokenAsync(token);
                var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync();
                await _refreshTokenService.SaveRefreshTokenAsync(token.Id, user.UserID, refreshToken);

                var tokenDTO = new TokenDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    //Email = user.Email,
                    //Fullname = user.FullName,
                    //Avatar = user.UserDetail.Avatar,
                };

                return Result.SuccessWithObject(tokenDTO);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }
        public async Task<dynamic> GetGoogleAccount(string firebaseToken)
        {
            try
            {
                FirebaseToken decryptedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
                string uid = decryptedToken.Uid;
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid); // bien cua firebase
                string email = userRecord.Email;
                string fullName = userRecord.DisplayName;
                // string ImageUrl = userRecord.PhotoUrl.ToString();
                var checkExist = await _userRepository.GetUserByEmail(email);
                if (checkExist != null && checkExist.IsGoogleAccount == true)
                {
                    var result = await SignInWithGoogle(email);
                    return result;
                }
                return Result.SuccessWithObject(new
                {
                    Message = "GoogleAccount",
                    email = email,
                    fullName = fullName,
                    // ImageUrl = ImageUrl,

                });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> SignUpForIntrustor(RegisterInstructorDTO userDTO)
        {
            try
            {
                List<Error> errors = new List<Error>();
                if (await _userRepository.CheckEmail(userDTO.Email))
                {
                    errors.Add(UserErrors.EmailAlreadyUsed(userDTO.Email));
                }
                if (Validator.IsValidPhone(userDTO.Phone))
                {
                    userDTO.Phone = ConvertToInternationalPhoneFormat(userDTO.Phone);
                }
                if (await _userRepository.CheckPhone(userDTO.Phone))
                {
                    errors.Add(UserErrors.PhoneAlreadyUsed(userDTO.Phone));
                }
                //fullname
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.FullName)
                    ? new Error[] { UserErrors.FullnameIsEmpty }//User.FullnameIsEmpty
                    : !Validator.IsValidName(userDTO.FullName)
                        ? new Error[] { UserErrors.FullnameIsInvalid(userDTO.FullName) }//User.FullnameIsInvalid
                        : Enumerable.Empty<Error>()
                    );
                //email
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Email)
                    ? new Error[] { UserErrors.EmailIsEmpty }//User.EmailIsEmpty
                    : !Validator.IsValidEmail(userDTO.Email)
                        ? new Error[] { UserErrors.EmailIsInvalid(userDTO.Email) }//User.EmailIsInvalid
                        : Enumerable.Empty<Error>()
                    );
                //password
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Password)
                    ? new Error[] { UserErrors.PasswordIsEmpty }//User.PasswordIsEmpty
                    : !Validator.IsMinLengthPassword(userDTO.Password)
                        ? new Error[] { UserErrors.PasswordMinLength }//User.PasswordMinLength
                        : !Validator.IsValidPassword(userDTO.Password)
                            ? new Error[] { UserErrors.PasswordIsInvalid }//User.PasswordIsInvalid
                            : Enumerable.Empty<Error>()
                    );
                //DOB
                errors.AddRange(
                    !Validator.IsValidDOB(userDTO.DateOfBirth)
                    ? new Error[] { UserErrors.DOBIsInvalid } //User.DOBIsInvalid
                    : !Validator.IsEnoughAge(userDTO.DateOfBirth)
                        ? new Error[] { UserErrors.DOBIsNotEnogh } //User.DOBIsNotEnogh
                        : Enumerable.Empty<Error>()
                    );
                //phone
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Phone)
                    ? new Error[] { UserErrors.PhoneIsEmpty }//User.PasswordIsEmpty
                    : !Validator.CheckPhoneLength(userDTO.Phone)
                        ? new Error[] { UserErrors.PhoneLength }//User.PhoneLength
                        : !Validator.IsValidPhone(userDTO.Phone)
                            ? new Error[] { UserErrors.PhoneIsInvalid }//User.PhoneIsInvalid
                            : Enumerable.Empty<Error>()
                    );
                //address
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.Address)
                    ? new Error[] { UserErrors.AddressIsEmpty }//User.AddressIsEmpty
                   : Enumerable.Empty<Error>()
                   );
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.TaxNumber)
                    ? new Error[] { InstructorErrors.TaxNumberIsEmpty() }
                   : Enumerable.Empty<Error>()
                   );
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.CardNumber)
                    ? new Error[] { InstructorErrors.CardNumberIsEmpty() }
                   : Enumerable.Empty<Error>()
                   );
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.CardName)
                    ? new Error[] { InstructorErrors.CardNameIsEmpty() }
                   : Enumerable.Empty<Error>()
                   );
                errors.AddRange(
                    string.IsNullOrEmpty(userDTO.CardProvider)
                    ? new Error[] { InstructorErrors.CardProviderIsEmpty() }
                   : Enumerable.Empty<Error>()
                   );
                errors.AddRange(ValidateCertification(userDTO.Certification));


                if (errors == null || !errors.Any())
                {
                    var userId = await _userRepository.AutoGenerateUserID();
                    var hashpassword = await HashPassword(userDTO.Password);
                    var userDetailId = await _userDetailRepository.AutoGenerateUserDetailID();
                    User newUser = new User()
                    {
                        UserID = userId,
                        Email = userDTO.Email,
                        FullName = userDTO.FullName,
                        Phone = userDTO.Phone,
                        Password = hashpassword,
                        RoleID = 2,
                        IsBan = false,
                        IsDelete = false,
                        IsMailConfirmed = false,
                        IsGoogleAccount = false,
                    };
                    await _userRepository.AddUserAsync(newUser);
                    var userDetail = await _userDetailService.RegisterUserDetailAsync(userDTO, userId);
                    var instructor = await _instructorService.RegisterInstructorAsync(userDTO, userId);
                    var userWallet = await _walletService.RegisterUserWalletlAsync(userId);
                    if (userDetail == true && userWallet == true)
                    {
                        return Result.Success();
                    }
                }
                return Result.Failures(errors);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        private IEnumerable<Error> ValidateCertification(IFormFile? certification)
        {
            if (certification == null)
            {
                return new Error[] { InstructorErrors.CertificationIsEmpty() };
            }

            var allowedExtensions = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(certification.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return new Error[] { InstructorErrors.CertificationInvalidType() };
            }

            const long maxFileSizeInBytes = 5 * 1024 * 1024; // 5MB
            if (certification.Length > maxFileSizeInBytes)
            {
                return new Error[] { InstructorErrors.CertificationTooLarge() };
            }

            return Enumerable.Empty<Error>();
        }
        public async Task<Result> ChangePassword(string email, string newPassword, string oldPassword)
        {
            try
            {
                if (!Validator.IsValidEmail(email))
                {
                    return Result.Failure(UserErrors.EmailIsInvalid(email));
                }
                if (!Validator.IsValidPassword(newPassword) || !Validator.IsValidPassword(oldPassword))
                {
                    return Result.Failure(UserErrors.PasswordIsInvalid);
                }
                var checkEmailExist = await _userRepository.GetUserByEmail(email);

                if (checkEmailExist == null)
                {
                    return Result.Failure(UserErrors.UserIsNotExist);
                }
                if (checkEmailExist.IsGoogleAccount == true)
                {
                    return Result.Failure(new Error("Email", "Can not change password of Google account"));
                }
                if (checkEmailExist.Password != await HashPassword(oldPassword))
                {
                    return Result.Failure(new Error("Password", "Your password not match with your account"));
                }
                return Result.Success();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        public async Task<dynamic> ConfirmToChangePassword(string key)
        {
            try
            {
                string[] decyptData = Encryption.DecryptParameters(key);
                string email = decyptData[0];
                string password = decyptData[1];
                if (string.IsNullOrEmpty(email)) return "Invalid Email";
                var result = await _userRepository.CheckEmail(email);
                if (!result)
                {
                    return Message.InvalidUser;
                }
                else
                {

                    var result2 = await _userRepository.UpdatePasswordByEmail(email, await HashPassword(password));
                }
                return _mailService.ReturnToLoginPage();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }



        public string ConvertToInternationalPhoneFormat(string phone)
        {
            if (phone.StartsWith("0"))
            {
                return "+84" + phone.Substring(1);
            }
            else if (phone.StartsWith("+84"))
            {
                return phone;
            }
            else
            {
                throw new ArgumentException("Phone number format is not recognized");
            }
        }


        public async Task<dynamic> SendMailComfirm(string email)
        {
            try
            {
                MailObject mailObject = new MailObject()
                {
                    Subject = MailSubjectPattern.MailComfirmation,
                    Body = _mailServiceV2.MailConfirmationBody(email),
                    ToMailIds = new List<string>()
                        {
                  email,
                     },
                };
                var result = await _mailServiceV2.SendMail(mailObject);
                var type = await _emailTemplateRepsository.GetEmailTemplateByType(MailType.ConfirmationAccount);
                var user = await _userRepository.GetUserByEmail(email);
                UserEmail userEmail = new UserEmail()
                {
                    UserID = user.UserID,
                    EmailTemplateId = type.EmailTemplateId,
                    Description = "None",
                    CreateBy = "System",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    UpdateBy = "System",
                };
                var saveMail = await _emailTemplateRepsository.SaveEmailSending(userEmail);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<dynamic> SendMailChangePassword(string email, string newPassword)
        {
            try
            {
                MailObject mailObject = new MailObject()
                {
                    Subject = MailSubjectPattern.ChangePassword,
                    Body = _mailServiceV2.ChangePasswordConfirmationBody(email, newPassword),
                    ToMailIds = new List<string>()
                        {
                  email,
                     },
                };
                var result = await _mailServiceV2.SendMail(mailObject);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<dynamic> SendMailResetPassword(string email, string newPassword)
        {
            try
            {
                MailObject mailObject = new MailObject()
                {
                    Subject = MailSubjectPattern.RestPassword,
                    Body = _mailServiceV2.RestPasswordConfirmationBody(email, newPassword),
                    ToMailIds = new List<string>()
                        {
                  email,
                     },
                };
                var result = await _mailServiceV2.SendMail(mailObject);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<dynamic> UpdateUserProfile(UpdateUserProfileDTO updateUserProfileDTO)
        {
            if (string.IsNullOrEmpty(updateUserProfileDTO.UserID))
                return Result.Failure(UserErrors.UserIsNotExist);
            if (string.IsNullOrEmpty(updateUserProfileDTO.FullName))
                return Result.Failure(UserErrors.FullnameIsEmpty);
            if (!Validator.IsValidName(updateUserProfileDTO.FullName))
                return Result.Failure(UserErrors.FullnameIsInvalid(updateUserProfileDTO.FullName));
            if (string.IsNullOrEmpty(updateUserProfileDTO.Address))
                return Result.Failure(UserErrors.AddressIsEmpty);
            try
            {
                User u = await _userRepository.GetUserByIdAsync(updateUserProfileDTO.UserID);
                UserDetail ud = await _userDetailRepository.GetUserDetailByIdAsync(updateUserProfileDTO.UserID);
                if (u != null && ud != null)
                {
                    u.FullName = updateUserProfileDTO.FullName;
                    ud.Address = updateUserProfileDTO.Address;
                    ud.Avatar = updateUserProfileDTO.Avatar;
                    ud.DateOfBirth = updateUserProfileDTO.DateOfBirth;
                    ud.UpdatedDate = DateTime.Now;
                    await _userRepository.UpdateUserAsync(u);
                    await _userDetailRepository.UpdateUserDetailAsync(ud);
                    return Result.Success();
                }
                else
                {
                    return Result.Failure(new Error("User null", "User does not exist"));
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<Result> GetUserProfileById(string userId)
        {
            try
            {
                var currentUser = await _userRepository.GetUserProfileById(userId);
                return Result.SuccessWithObject(currentUser);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Result> ApproveStudent(ApproveStudent approveStudent)
        {
            var student = await _userRepository.GetUserIsStudent(approveStudent.StudentId);
            if (student == null)
            {
                return Result.Failure(UserErrors.UserIsNotExist);
            }
            student.UserDetail.IsActive = true;

            await _userRepository.UpdateUserAsync(student);

            return Result.Success();

        }


        public async Task<Result> RejectStudent(RejectStudent reject)
        {
            var student = await _userRepository.GetUserIsStudent(reject.StudentID);
            if (student == null)
            {

                return Result.Failure(UserErrors.UserIsNotExist);
            }

            student.UserDetail.IsActive = false;

            await _userRepository.UpdateUserAsync(student);

            return Result.Success();

        }
        public async Task<Result> GetListOfStudent()
        {
            try
            {
                List<StudentDTO> user = await _userRepository.GetStudentsAsync();
                if (user.Count > 0)
                {
                    return Result.SuccessWithObject(user);
                }
                return Result.Failure(Result.CreateError("NULL", "There is no data"));
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("User null", "User list null"));

            }
        }
    }
}

