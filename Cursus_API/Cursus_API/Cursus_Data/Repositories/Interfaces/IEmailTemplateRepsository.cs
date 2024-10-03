using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IEmailTemplateRepsository
    {
        Task<EmailTemplate> GetEmailTemplateByType(string type);
        Task<dynamic> SaveEmailSending(UserEmail userEmail);
    }
}
