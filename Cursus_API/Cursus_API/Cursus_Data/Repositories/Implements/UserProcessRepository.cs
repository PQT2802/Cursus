using Cursus_Data.Context;
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
    public class UserProcessRepository : IUserProcessRepository
    {
        private readonly LMS_CursusDbContext _context;

        public UserProcessRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task CompleteUserProcess(string enrolledCourseId, string courseContentId)
        {
            UserProcess userProcess = await _context.UserProcesses.Where(x => x.EnrollCourseId == enrolledCourseId && x.CourseContentId == courseContentId).FirstOrDefaultAsync();
            userProcess.IsComplete = true;
            _context.Update(userProcess);
            await _context.SaveChangesAsync();
        }

        public async Task<double> CountContentByEnrollCourseId(string enrolledCourseId)
        {
            int a = await _context.UserProcesses.Where(x => x.EnrollCourseId == enrolledCourseId).CountAsync();
            double b = a != 0 ? 1.0 / a : 0.0;
            return b;
        }

        public async Task<int> GetUserProcessId(string enrolledCourseId, string courseContentId)
        {
            return await _context.UserProcesses.Where(x => x.EnrollCourseId == enrolledCourseId && x.CourseContentId == courseContentId)
                .Select(x => x.UserProcessId).FirstOrDefaultAsync();
        }

        public async Task<int> CheckProcess(string enrolledCourseId)
        {
            return await _context.UserProcesses.Where(x => x.EnrollCourseId == enrolledCourseId && !x.IsComplete).Select(x => x.UserProcessId).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckFinishProcess(string enrolledCourseId, string courseContentId)
        {
            UserProcess userProcess = await _context.UserProcesses.Where(x => x.EnrollCourseId == enrolledCourseId && x.CourseContentId == courseContentId).FirstOrDefaultAsync();
            if (userProcess.IsComplete) return true;
            return false;
        }
    }
}
