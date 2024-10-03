using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IMailService
    {
        Task<string> SendMail(MailClass mailClass);
        string GetMailBody(string email);
        // string ResetPasswordMailConfirmed(string email,string password);
        string ChangePasswordMailConfirmed(string email, string password);
        Task SendMessageToEmail(string toEmailAddress, string subjectMessage, string bodyMessage);
        string ReturnToLoginPage();
    }
}
