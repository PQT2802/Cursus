using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class MailServiceV3 : IMailServiceV3
    {
        private readonly IMailRepository _mailRepository;
        private readonly IEmailTemplateRepsository _emailTemplateRepsository;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ICourseRepository _courseRepository;
        public MailServiceV3(IMailRepository mailRepository, IEmailTemplateRepsository emailTemplateRepsository,
            IEmailTemplateService emailTemplateService, ICourseRepository courseRepository)
        {
            _mailRepository = mailRepository;
            _emailTemplateRepsository = emailTemplateRepsository;
            _emailTemplateService = emailTemplateService;
            _courseRepository = courseRepository;
        }

        public async Task<string> SendApproveCourseMail(string courseId)
        {
            var tempalte = await _emailTemplateRepsository.GetEmailTemplateByType(MailType.ApproveCourse);
            var course = await  _courseRepository.GetUICoruseById(courseId);
            var placeHolders = new Dictionary<string, string>
        {
            { "UserName", course.InstructorName },
            { "CourseTitle", course.Title },

        };
            var body = await _emailTemplateService.GenerateEmailBody(tempalte.Type, placeHolders);

            // Create a mail object
            MailObject mailObject = new MailObject()
            {
                ToMailIds = new List<string> { course.InstructorEmail }, // Assuming user.Email is available
                Subject = tempalte.Subject,
                Body = body,
            };

            // Send the email
            await _emailTemplateService.SendMail(mailObject);
            UserEmail ue = new UserEmail
            {
                UserID = course.InstructorId,
                EmailTemplateId = tempalte.EmailTemplateId,
                Description = "Course Approval",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateBy = "System",
                UpdateBy = "System",
            };
            var result = await _emailTemplateRepsository.SaveEmailSending(ue);
            return "Emails sent successfully.";
        }
            


        public async Task<string> SendCloseCourseMail(int id, string reason, TimeSpan duration)
        {
            // Get the list of enrolled students' emails
            var listEmail = await _mailRepository.GetEnrolledStudentOfCourse(id);

            // Get the email template for deactivating the course
            var template = await _emailTemplateRepsository.GetEmailTemplateByType(MailType.DeactivateCourse);

            foreach (var user in listEmail)
            {
                // Placeholder values for the email template
                var placeHolders = new Dictionary<string, string>
        {
            { "DeactivationReason", reason },
            { "Duration", duration.ToString(@"dd\:hh\:mm")  },
            { "UserName", user.FullName } // Assuming user.FullName is available
        };

                // Generate the email body
                var body = await _emailTemplateService.GenerateEmailBody(template.Type, placeHolders);

                // Create a mail object
                MailObject mailObject = new MailObject()
                {
                    ToMailIds = new List<string> { user.Email }, // Assuming user.Email is available
                    Subject = template.Subject,
                    Body = body,
                };

                // Send the email
                await _emailTemplateService.SendMail(mailObject);
                UserEmail ue = new UserEmail
                {
                    UserID = user.UserId,
                    EmailTemplateId = template.EmailTemplateId,
                    Description = reason,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System",
                };
                var result = await _emailTemplateRepsository.SaveEmailSending(ue);
            }
            return "Emails sent successfully.";
        }

        public Task<string> SendForgetPasswordEmail(string toEmail, string userName, string resetLink)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SendRejectCourseMail(string courseId, string reason)
        {
            var tempalte = await _emailTemplateRepsository.GetEmailTemplateByType(MailType.RejectCourse);
            var course = await _courseRepository.GetUICoruseById(courseId);
            var placeHolders = new Dictionary<string, string>
        {
            { "UserName", course.InstructorName },
            { "CourseTitle", course.Title },
            { "RejectionReason", reason },

        };
            var body = await _emailTemplateService.GenerateEmailBody(tempalte.Type, placeHolders);

            // Create a mail object
            MailObject mailObject = new MailObject()
            {
                ToMailIds = new List<string> { course.InstructorEmail }, // Assuming user.Email is available
                Subject = tempalte.Subject,
                Body = body,
            };

            // Send the email
            await _emailTemplateService.SendMail(mailObject);
            UserEmail ue = new UserEmail
            {
                UserID = course.InstructorId,
                EmailTemplateId = tempalte.EmailTemplateId,
                Description = $"Course Reject: {reason}",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CreateBy = "System",
                UpdateBy = "System",
            };
            var result = await _emailTemplateRepsository.SaveEmailSending(ue);
            return "Emails sent successfully.";
        }
    }
}
