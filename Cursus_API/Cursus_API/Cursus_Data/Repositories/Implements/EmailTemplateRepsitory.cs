using Cursus_Data.Context;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class EmailTemplateRepsitory : IEmailTemplateRepsository
    {
        private readonly LMS_CursusDbContext _context;
        public EmailTemplateRepsitory(LMS_CursusDbContext context) 
		{
			_context = context;
		}
        public async Task<EmailTemplate> GetEmailTemplateByType(string type)
        {
			try
			{
				var emailTemplate = await _context.EmailTemplates.FirstOrDefaultAsync(et => et.Type == type);
				if (emailTemplate == null) return null;
				return emailTemplate;
			}
			catch (Exception)
			{

				throw;
			}
        }

        public async Task<dynamic> SaveEmailSending(UserEmail userEmail)
        {
			_context.UserEmails.Add(userEmail);
            return await _context.SaveChangesAsync();
        }
    }
}
