using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cursus_Business.Service.Implements
{
    public class MailServiceV2 : IMailServiceV2
    {
        private readonly IConfiguration _configuration;
        public MailServiceV2(IConfiguration configuration)
        {
            _configuration = configuration;
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
        public string MailConfirmationBody(string email)
        {
            string encryptEmail = Encryption.Encrypt(email);
            string url = $"{Global.DomainName}api/v1.0/user/mail-coinfirm?key={HttpUtility.UrlEncode(encryptEmail)}";

            // Name of your HTML file as an embedded resource
            string resourceName = "Cursus_Business.Common.MailTemplate.MailConfirmation.html";

            // Read the embedded resource content
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource '{resourceName}' not found in assembly.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string htmlContent = reader.ReadToEnd();

                    // Replace the placeholder in the HTML with the actual URL
                    string updatedHtmlContent = htmlContent.Replace("{url_link}", url);

                    return updatedHtmlContent;
                }
            }
        }


        public string CourseClosedNotificationBody(string name)
        {
            try
            {
                string resourceName = "Cursus_Business.Common.MailTemplate.CourseClosedNotification.html";


                // Read the embedded resource content
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new Exception($"Resource '{resourceName}' not found in assembly.");
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string htmlContent = reader.ReadToEnd();

                        // Replace the placeholder in the HTML with the actual URL
                        string updatedHtmlContent = htmlContent.Replace("[Course_Name]", name);
                        DateTime closingDate = DateTime.Now.AddDays(30);
                        updatedHtmlContent = htmlContent.Replace("[Closing_Date]", closingDate.ToString("dd/mm/yyyy"));
                        return updatedHtmlContent;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public string ApprovedMailNotificationBody(string instructorName)
        {
            try
            {
                string resourceName = "Cursus_Business.Common.MailTemplate.ApprovedMailNotification.html";
                // Read the embedded resource content
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new Exception($"Resource '{resourceName}' not found in assembly.");
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string htmlContent = reader.ReadToEnd();

                        // Replace the placeholder in the HTML with the actual URL
                        string updatedHtmlContent = htmlContent.Replace("[Instructor_Name]", instructorName);
                       updatedHtmlContent = updatedHtmlContent.Replace("{url_link}", Global.SignInPage);
                        return updatedHtmlContent;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public string RejectedMailNotificationBody(string instructorName, string reason)
        {
            try
            {
                string resourceName = "Cursus_Business.Common.MailTemplate.RejectedMailNotification.html";
                // Read the embedded resource content
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new Exception($"Resource '{resourceName}' not found in assembly.");
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string htmlContent = reader.ReadToEnd();

                        // Replace the placeholder in the HTML with the actual URL
                        string updatedHtmlContent = htmlContent.Replace("[Instructor_Name]", instructorName);
                        updatedHtmlContent = updatedHtmlContent.Replace("[Reason]", reason);
                        updatedHtmlContent = updatedHtmlContent.Replace("{url_link}", Global.SignInPage);
                        return updatedHtmlContent;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public string ChangePasswordConfirmationBody(string email, string newPassword)
        {
            var encryptData = Encryption.EncryptParameters(email, newPassword);
            string url = $"{Global.DomainName}api/v1.0/User/change-password?key={HttpUtility.UrlEncode(encryptData)}";

            // Name of your HTML file as an embedded resource
            string resourceName = "Cursus_Business.Common.MailTemplate.ChangePasswordTemplate.html";


            // Read the embedded resource content
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource '{resourceName}' not found in assembly.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string htmlContent = reader.ReadToEnd();

                    // Replace the placeholder in the HTML with the actual URL
                    string updatedHtmlContent = htmlContent.Replace("{url_link}", url);

                    return updatedHtmlContent;
                }
            }
        }

        public string RestPasswordConfirmationBody(string email, string newPassword)
        {
            var encryptData = Encryption.EncryptParameters(email, newPassword);
            string url = $"{Global.DomainName}api/v1.0/User/change-password?key={HttpUtility.UrlEncode(encryptData)}";

            // Name of your HTML file as an embedded resource
            string resourceName = "Cursus_Business.Common.MailTemplate.ResetPasswordTemplate.html";


            // Read the embedded resource content
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource '{resourceName}' not found in assembly.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string htmlContent = reader.ReadToEnd();

                    // Replace the placeholder in the HTML with the actual URL
                    string updatedHtmlContent = htmlContent.Replace("{url_link}", url);

                    return updatedHtmlContent;
                }
            }
        }
    }
}
