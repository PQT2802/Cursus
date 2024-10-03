using Cursus_Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Data.Configuration
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasData(
                 new EmailTemplate
                 {
                     EmailTemplateId = 1,
                     Subject = "You Have a New Notification",
                     Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>You have a new notification:</p>
                    <p>{{NotificationMessage}}</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
                     Type = "Notification",
                     IsDelete = false,
                     CreateDate = DateTime.Now,
                     CreateBy = "System",
                     UpdateDate = DateTime.Now,
                     UpdateBy = "System"
                 },
        new EmailTemplate
        {
            EmailTemplateId = 2,
            Subject = "Password Reset Request",
            Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>We received a request to reset your password. Click the link below to reset your password:</p>
                    <p><a href='{{ResetLink}}'>Reset Password</a></p>
                    <p>If you did not request a password reset, please ignore this email.</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
            Type = "ForgetPassword",
            IsDelete = false,
            CreateDate = DateTime.Now,
            CreateBy = "System",
            UpdateDate = DateTime.Now,
            UpdateBy = "System"
        },
        new EmailTemplate
        {
            EmailTemplateId = 3,
            Subject = "Your Password Has Been Reset",
            Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>Your password has been successfully reset. You can now log in with your new password.</p>
                    <p>If you did not request this change, please contact our support team immediately.</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
            Type = "ResetPassword",
            IsDelete = false,
            CreateDate = DateTime.Now,
            CreateBy = "System",
            UpdateDate = DateTime.Now,
            UpdateBy = "System"
        },
        new EmailTemplate
        {
            EmailTemplateId = 4,
            Subject = "Account Confirmation",
            Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>Thank you for registering with us. Please confirm your account by clicking the link below:</p>
                    <p><a href='{{ConfirmationLink}}'>Confirm Account</a></p>
                    <p>If you did not register, please ignore this email.</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
            Type = "ConfirmationAccount",
            IsDelete = false,
            CreateDate = DateTime.Now,
            CreateBy = "System",
            UpdateDate = DateTime.Now,
            UpdateBy = "System"
        },
        new EmailTemplate
        {
            EmailTemplateId = 5,
            Subject = "Course Rejection Notification",
            Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>We regret to inform you that your course titled '{{CourseTitle}}' has been rejected for the following reason:</p>
                    <p>{{RejectionReason}}</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
            Type = "RejectCourse",
            IsDelete = false,
            CreateDate = DateTime.Now,
            CreateBy = "System",
            UpdateDate = DateTime.Now,
            UpdateBy = "System"
        },
        new EmailTemplate
        {
            EmailTemplateId = 6,
            Subject = "Course Approval Notification",
            Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>Congratulations! Your course titled '{{CourseTitle}}' has been approved and is now live.</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
            Type = "ApproveCourse",
            IsDelete = false,
            CreateDate = DateTime.Now,
            CreateBy = "System",
            UpdateDate = DateTime.Now,
            UpdateBy = "System"
        },
        new EmailTemplate
        {
            EmailTemplateId = 7,
            Subject = "Account Activation",
            Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>Your account has been successfully activated. You can now log in and start using our services.</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
            Type = "ActivateUser",
            IsDelete = false,
            CreateDate = DateTime.Now,
            CreateBy = "System",
            UpdateDate = DateTime.Now,
            UpdateBy = "System"
        },
        new EmailTemplate
        {
            EmailTemplateId = 8,
            Subject = "Account Deactivation",
            Body = @"
                <html>
                <body>
                    <p>Hello {{UserName}},</p>
                    <p>Your account has been deactivated. If you think this is a mistake, please contact our support team.</p>
                    <p>Best regards,<br>Your Company</p>
                </body>
                </html>",
            Type = "DeactivateUser",
            IsDelete = false,
            CreateDate = DateTime.Now,
            CreateBy = "System",
            UpdateDate = DateTime.Now,
            UpdateBy = "System"
        },
         new EmailTemplate
         {
             EmailTemplateId = 9,
             Subject = "Course Activation Notification",
             Body = @"
            <html>
            <body>
                <p>Dear {{UserName}},</p>
                <p>We are pleased to inform you that your course titled <strong>'{{CourseTitle}}'</strong> has been successfully reactivated after the maintenance period.</p>
                
                <p>We appreciate your patience and understanding during this time. If you have any questions or need further assistance, please feel free to reach out to us.</p>
                
                <p>Best regards,<br>
                The [Your Company] Team</p>
            </body>
            </html>",
             Type = "ActivateCourse",
             IsDelete = false,
             CreateDate = DateTime.Now,
             CreateBy = "System",
             UpdateDate = DateTime.Now,
             UpdateBy = "System"
         },
          new EmailTemplate
          {
              EmailTemplateId = 10,
              Subject = "Course Deactivation Notification",
              Body = @"
            <html>
            <body>
                <p>Hello {{UserName}},</p>
                <p>Your course titled '{{CourseTitle}}' has been deactivated.</p>
                <p>Reason for deactivation: {{DeactivationReason}}</p>
                {{#if Duration}}
                <p>Duration: {{Duration}}</p>
                {{/if}}
                <p>Best regards,<br>Your Company</p>
            </body>
            </html>",
              Type = "DeactivateCourse",
              IsDelete = false,
              CreateDate = DateTime.Now,
              CreateBy = "System",
              UpdateDate = DateTime.Now,
              UpdateBy = "System"
          }



                );
        }

    }
}
