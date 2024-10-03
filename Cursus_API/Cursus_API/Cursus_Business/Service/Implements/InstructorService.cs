using Cursus_Business.Common;
using Cursus_Business.Common.Pattern;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMailServiceV2 _mailServiceV2;
        private readonly IFirebaseService _firebaseService;
        private readonly IUserRepository _userRepository;

        private readonly IUserDetailRepository _userDetailRepository;

        public InstructorService(IInstructorRepository instructorRepository, IFirebaseService firebaseService, IMailServiceV2 mailServiceV2, IUserRepository userRepository, LMS_CursusDbContext context, IUserDetailRepository userDetailRepository)
        {
            _instructorRepository = instructorRepository;
            _firebaseService = firebaseService;
            _mailServiceV2 = mailServiceV2;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
        }

        public async Task<bool> RegisterInstructorAsync(RegisterInstructorDTO registerInstructorDTO, string UserId)
        {
            var insid = await _instructorRepository.AutoGenerateInstructorID();
            var instructor = new Instructor
            {
                InstructorId = insid,
                UserId = UserId,
                TaxNumber = registerInstructorDTO.TaxNumber,
                CardNumber = registerInstructorDTO.CardNumber,
                CardName = registerInstructorDTO.CardName,
                CardProvider = registerInstructorDTO.CardProvider,
                Certification = await _firebaseService.UploadImage(registerInstructorDTO.Certification, FireBaseFolder.InstructorCertification),

            };
            if (instructor != null)
            {
                await _instructorRepository.AddInstructorAsync(instructor);
                return true;
            }
            return false;
        }


        public async Task<Result> ApproveInstructorAsync(string instructorID)
        {
            var instructor = await _instructorRepository.FindInstructorByid(instructorID);
            var user = await _userRepository.GetUserByIdAsync(instructorID);
            if (instructor == null)
            {
                return Result.Failure(Result.CreateError("Instructor", "Instructor not found"));

            }
            instructor.IsAccepted = true;
            await _instructorRepository.UpdateInstructorIsAcceptedAsync(instructor);

            // Logic to send approval email
            //await _mailService.SendMessageToEmail(user.Email, "Instructor Application Approved", $"Dear {user.FullName},\n\nCongratulations! Your application has been approved.\n\nBest Regards,\nYour Team");

            return Result.Success();
        }
        public async Task<Result> RejectInstructorAsync(RejectInstructorDTO rejectDTO)
        {
            var instructor = await _instructorRepository.FindInstructorByid(rejectDTO.instructorId);
            var user = await _userRepository.GetUserByIdAsync(rejectDTO.instructorId);
            if (instructor == null)
            {
                return Result.Failure(Result.CreateError("Instructor", "Instructor not found"));

            }
            instructor.IsAccepted = false;
            await _instructorRepository.UpdateInstructorIsAcceptedAsync(instructor);

            //await _mailService.SendMessageToEmail(user.Email, "Instructor Application Rejected", $"Dear {user.FullName},\n\nWe regret to inform you that your application has been rejected.\n\nReason: {reason}\n\nBest Regards,\nYour Team");

            return Result.Success();
        }

        public async Task<Result> GetInstructor(GetListDTO getListDTO)
        {
            try
            {
                List<InstructorDetailDTO> instructors = await _instructorRepository.GetAllInstructorsAsync(getListDTO);
                if (instructors.Count > 0)
                {
                    return Result.SuccessWithObject(instructors);
                }
                return Result.Failure(Result.CreateError("NULL", "There is no data"));
            }
            catch (Exception ex)
            {
                return Result.Failure(Result.CreateError("EXCEPTION", ex.Message));
            }
        }
        public async Task<List<InstructorDetailDTO>> GetInstructorForExcel(GetListDTO getListDTO)
        {
            try
            {
                List<InstructorDetailDTO> instructors = await _instructorRepository.GetAllInstructorsAsync(getListDTO);
                return instructors.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Result> DeactivateActivateInstructor(string instructorid)
        {
            try
            {
                if (!await _instructorRepository.CheckInstructorId(instructorid))
                {
                    return Result.Failure(InstructorErrors.InstructorIdNotExist);
                }
                User user = await _instructorRepository.GetUserByInstructorId(instructorid);
                if (user == null) return Result.Failure(InstructorErrors.InstructorIdNotExist);
                user.IsBan = !user.IsBan;
                await _userRepository.UpdateUserAsync(user);
                return Result.SuccessWithObject(new
                {
                    Message = (user.IsBan ?? false) ? "Instructor has been deactivated." : "Instructor has been activated."
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Result.CreateError("EXCEPTION", ex.Message));
            }
        }
        
        //public async Task<dynamic> GetInstructorDetail(string instructorID)
        //{
        //    try
        //    {
        //        var instructor = await _context.Instructors
        //        .Include(i => i.User)
        //        .Where(i => i.InstructorId == instructorID)
        //        .Select(i => new InstructorDetail
        //        {
        //            InstructorId = i.InstructorId,
        //            UserId = i.UserId,
        //            FullName = i.User.FullName,
        //            RoleId = i.User.RoleId,
        //            Email = i.User.Email,
        //            Phone = i.User.Phone,
        //            IsBan = i.User.IsBan,
        //            IsDelete = i.User.IsDelete,
        //            IsMailConfirmed = i.User.IsMailConfirmed,
        //            IsGoogleAccount = i.User.IsGoogleAccount,
        //            TaxNumber = i.TaxNumber,
        //            CardNumber = i.CardNumber,
        //            CardName = i.CardName,
        //            CardProvider = i.CardProvider,
        //            IsAccepted = i.IsAccepted,
        //            Certification = i.Certification
        //        })
        //        .FirstOrDefaultAsync();

        //        if (instructor == null)
        //        {
        //            return Result.Failure(InstructorErrors.InstructorIdNotExist);
        //        }

        //        return Result.Success(instructor);
        //    }
        //    catch
        //    {
        //        return Result.Failure(InstructorErrors.InstructorIdNotExist);
        //    }
        //}

        public async Task<Result> UpdateInfomationInstructor(UpdateInstructorDTO updateInstructorDTO)
        {
            try
            {
                if (!await _instructorRepository.CheckInstructorId(updateInstructorDTO.InstructorId))
                {
                    return Result.Failure(InstructorErrors.InstructorIdNotExist);
                }
                User user = await _instructorRepository.GetUserByInstructorId(updateInstructorDTO.InstructorId);
                if (user == null) return Result.Failure(InstructorErrors.InstructorIdNotExist);
                user.FullName = updateInstructorDTO.FullName;
                await _userRepository.UpdateUserAsync(user);
                return Result.SuccessWithObject(new
                {
                    Message = "Update successfully"
                });
            }
            catch (Exception ex)
            {
                return Result.Failure(Result.CreateError("EXCEPTION", ex.Message));
            }
        }




        public async Task<dynamic> SendApprovedInstructorMail(string id)
        {
            try
            {
                var instructor = await _instructorRepository.GetUserByInstructorId(id);
                MailObject mailObject = new MailObject()
                {
                    Subject = MailSubjectPattern.MailNotification,
                    Body = _mailServiceV2.ApprovedMailNotificationBody(instructor.FullName),
                    ToMailIds = new List<string>()
                        {
                  instructor.Email,
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

        public async Task<dynamic> SendRejectedInstructorMail(RejectInstructorDTO reject)
        {
            try
            {
                var instructor = await _instructorRepository.GetUserByInstructorId(reject.instructorId);
                MailObject mailObject = new MailObject()
                {
                    Subject = MailSubjectPattern.MailNotification,
                    Body = _mailServiceV2.RejectedMailNotificationBody(instructor.FullName, reject.Reason),
                    ToMailIds = new List<string>()
                        {
                  instructor.Email,
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


        public async Task<dynamic> GetAllInstructors()
        {
            try
            {
                List<InstructorDetailDTO> listInstructor = await _instructorRepository.GetAllInstructorsAsync();

                return Result.SuccessWithObject(listInstructor);
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task<string> GetInstructorId(string userid)
        {
            return await _instructorRepository.GetInstructorIdByUserId(userid);
        }

        public async Task<Result> GetCourseListFillterForInstructor(string userId, CourseListConfigForInstrucor config)
        {
            var result =  await _instructorRepository.GetCourseListFillterForInstructor(userId, config);
            return Result.SuccessWithObject(result);
        }
    }

}
