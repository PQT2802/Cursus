using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cursus_Business.Service.Implements
{
    public class MailService : IMailService
    {
        public string GetMailBody(string  email)
        {
            //    string encryptEmail = Encryption.Encrypt(email);
            //    string url = $"{Global.DomainName}api/v1.0/users/comfirmMail?key={HttpUtility.UrlEncode(encryptEmail)}";
            //    return $@"
        //    < div style = 'text-align:center;' >
        //        < h1 > Welcome to Cursus</ h1 >
        //        < h3 > Click the button below to verify your Email Id</ h3 >
        //        < form method = 'post' action = '{url}' style = 'display:inline;' >
        //            < button type = 'submit' style = 'display:block;
        //                                         text - align:center;
        //    font - weight:bold;
        //    background - color:#008CBA;
        //                                         font - size:16px;
        //    border - radius:10px;
        //color:#ffffff;
        //                                         cursor: pointer;
        //width: 100 %;
        //padding: 10px; '>
        //                Confirm Mail
        //            </ button >
        //        </ form >
        //    </ div > ";

            string encryptEmail = Encryption.Encrypt(email);
            string url = $"{Global.DomainName}api/v1.0/users/MailConfirmation?key={HttpUtility.UrlEncode(encryptEmail)}";

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




        public  string ChangePasswordMailConfirmed(string email, string password)
        {
            //var hashPassword = await Encryption.HashPassword(password);
            var encryptData = Encryption.EncryptParameters(email, password);
            string url = $"{Global.DomainName}api/v1.0/users/changeConfirmed?key={HttpUtility.UrlEncode(encryptData)}";
            return $@"
        <div style='text-align:center;'>
            <h1>Welcome to Cursus</h1>
            <h3>Click the button below to confirm to your changing</h3>
            <form method='post' action='{url}' style='display:inline;'>
                <button type='submit' style='display:block;
                                             text-align:center;
                                             font-weight:bold;
                                             background-color:#008CBA;
                                             font-size:16px;
                                             border-radius:10px;
                                             color:#ffffff;
                                             cursor:pointer;
                                             width:100%;
                                             padding:10px;'>
                    Confirm Mail
                </button>
            </form>
        </div>";
        }

        public string ReturnToLoginPage()
        {
            return $@"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Confirmation Page</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
                display: flex;
                justify-content: center;
                align-items: center;
                height: 100vh;
            }}
            .container {{
                background-color: #fff;
                padding: 20px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                border-radius: 10px;
                text-align: center;
            }}
            h1 {{
                color: #333;
            }}
            h3 {{
                color: #666;
                margin-bottom: 20px;
            }}
            .button {{
                display: inline-block;
                text-align: center;
                font-weight: bold;
                background-color: #008CBA;
                font-size: 16px;
                border: none;
                border-radius: 10px;
                color: #ffffff;
                cursor: pointer;
                width: 100%;
                padding: 15px;
                text-decoration: none;
                transition: background-color 0.3s ease;
            }}
            .button:hover {{
                background-color: #005f75;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Welcome to Cursus,thank for your registration</h1>
            <h3>Click the button below to return to the Login Page</h3>
            <form method='post' action='{Global.SignInPage}' style='display:inline;'>
                <button type='submit' class='button'>
                    Confirm Mail
                </button>
            </form>
        </div>
    </body>
    </html>";
        }

        public async Task<string> SendMail(MailClass mailClass)
        {
            try
            {
                using(MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailClass.FromMailId);
                    mailClass.ToMailIds.ForEach(x =>
                    {
                        mail.To.Add(x);
                    }

                        );
                    mail.Subject = mailClass.Subject;
                    mail.Body = mailClass.Body;
                    mail.IsBodyHtml = mailClass.IsBodyHtml;
                    mailClass.Attachments.ForEach(x =>
                    {
                        mail.Attachments.Add(new Attachment(x));
                    });
                    using(SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(mailClass.FromMailId,mailClass.FromMailIdPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return Message.MailSent;
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public async Task SendMessageToEmail(string toEmailAddress, string subjectMessage, string bodyMessage)
        {
            // Email account information
            string smtpServer = "smtp.gmail.com";
            int port = 587;
            string senderEmail = "PQT2802@gmail.com";
            string password = "jbip lkqj utmh pnmt";

            // Create an SmtpClient object to send email
            SmtpClient smtpClient = new SmtpClient(smtpServer, port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, password);
            smtpClient.EnableSsl = true;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            mailMessage.To.Add(toEmailAddress);
            mailMessage.Subject = subjectMessage;
            mailMessage.Body = bodyMessage;

            // Send email
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending confirmation email: " + ex.Message);
            }
        }
    }
}
