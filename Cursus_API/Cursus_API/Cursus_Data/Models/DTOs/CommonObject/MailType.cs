using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.CommonObject
{
    public class MailType
    {
        public static readonly string Notification = "Notification";
        public static readonly string ForgetPassword = "ForgetPassword";
        public static readonly string ResetPassword = "ResetPassword";
        public static readonly string ConfirmationAccount = "ConfirmationAccount";
        public static readonly string RejectCourse = "RejectCourse";
        public static readonly string ApproveCourse = "ApproveCourse";
        public static readonly string ActivateUser = "ActivateUser";
        public static readonly string DeactivateUser = "DeactivateUser";
        public static readonly string ActivateCourse = "ActivateCourse";
        public static readonly string DeactivateCourse = "DeactivateCourse";

    }
}
