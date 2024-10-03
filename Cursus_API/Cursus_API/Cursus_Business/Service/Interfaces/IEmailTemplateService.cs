using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<string> GenerateEmailBody(string emailTemplateType, Dictionary<string, string> placeholders);
        Task<dynamic> SendMail(MailObject mailObject);
    }
}
