using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepsository _emailTemplateRepsository;
        private readonly IConfiguration _configuration;

        public EmailTemplateService( IEmailTemplateRepsository emailTemplateRepsository, IConfiguration configuration) 
        {
        _emailTemplateRepsository = emailTemplateRepsository;
            _configuration = configuration;
        }
        public async Task<string> GenerateEmailBody(string emailTemplateType, Dictionary<string, string> placeholders)
        {
            var emailTemplate = await _emailTemplateRepsository.GetEmailTemplateByType(emailTemplateType);
            if (emailTemplate == null)
                throw new Exception("Email template not found");

            string body = emailTemplate.Body;
            foreach (var placeholder in placeholders)
            {
                body = body.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            return body;
        }

        public async Task<dynamic> SendMail(MailObject mailObject)
        {
            try
            {
                string smtpServer = _configuration["SMTP:Server"];
                int port = int.Parse(_configuration["SMTP:Port"]);
                string senderEmail = _configuration["MailService:MailSender"];
                string password = _configuration["MailService:PasswordSender"];
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mailObject.ToMailIds.ForEach(x =>
                    {
                        mail.To.Add(x);
                    }
                        );
                    mail.Subject = mailObject.Subject;
                    mail.Body = mailObject.Body;
                    mail.IsBodyHtml = mailObject.IsBodyHtml;
                    mailObject.Attachments.ForEach(x =>
                    {
                        mail.Attachments.Add(new Attachment(x));
                    });
                    using (SmtpClient smtp = new SmtpClient(smtpServer, port))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(senderEmail, password);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return Result.Success();
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
