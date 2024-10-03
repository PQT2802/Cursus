using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IMailServiceV2
    {
        Task<dynamic> SendMail(MailObject mailObject);
        string MailConfirmationBody(string email);     
        string CourseClosedNotificationBody(string name);
        string ApprovedMailNotificationBody(string instructorName);
        string RejectedMailNotificationBody(string instructorName, string reason);
        string ChangePasswordConfirmationBody(string email,string newPassword);
        string RestPasswordConfirmationBody(string email,string newPassword);
    }
}
