using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.CommonObject
{
    public static class EmailTemplateType
    {
        public static readonly String NotificationMail = "Notification";
        public static readonly String ForgetPasswordMail = "ForgetPassword";
        public static readonly String ResetPasswordMail = "ResetPassword";
        public static readonly String ConfirmationAccountMail = "ConfirmationAccount";
        public static readonly String RejectCourseMail = "RejectCourse";
        public static readonly String ApproveCourseMail = "ApproveCourse";
        public static readonly String ActivateUserMail = "ActivateUser";
        public static readonly String DeactivateUserMail = "DeactivateUser";
    }
}
