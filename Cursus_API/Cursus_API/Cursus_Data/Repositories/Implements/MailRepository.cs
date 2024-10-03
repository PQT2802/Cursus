using Cursus_Data.Context;
using Cursus_Data.Models.DTOs.Course;
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
    public class MailRepository : IMailRepository
    {
        private readonly LMS_CursusDbContext _context;
        public MailRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task<UserEmail> GetConfirmationMailByUserIdAsync(string userId, int emailTemplateId)
        {
            return await _context.UserEmails
                .Where(ue => ue.UserID == userId && ue.EmailTemplateId == emailTemplateId)
                .OrderByDescending(ue => ue.CreateDate)
                .FirstOrDefaultAsync();
        }

        public async Task<List<EnrolledStudent>> GetEnrolledStudentOfCourse(int courseVersionId)
        {
            var result = await _context.EnrollCourses
                .Where(ec => ec.CourseVersionId == courseVersionId)
                .Include(ec => ec.User)
                .Select(ec => new EnrolledStudent
                {
                    UserId = ec.UserId,
                    Email = ec.User.Email,
                    FullName = ec.User.FullName
                })
                .ToListAsync();

            return result;
        }
    }
}
