using Cursus_Data.Context;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class EnrolledCourseRepository : IEnrolledCourseRepository
    {
        private readonly LMS_CursusDbContext _context;

        public EnrolledCourseRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }


       

        public async Task FinishCourse(string enrollCourseId)
        {
            EnrollCourse enrollCourse = await _context.EnrollCourses.Where(x => x.EnrollCourseId == enrollCourseId).FirstOrDefaultAsync();
            if (enrollCourse.Process == 1) enrollCourse.Status = "Completed";    
            _context.SaveChanges();
        }

        public async Task<string> EnrollInCourseAsync(string userId, string courseId)

        {
            var latestCourseVersion = await _context.EnrollCourses
                .Where(cv => cv.CourseId == courseId && cv.UserId == userId)
                .FirstOrDefaultAsync();

            //var enrollCourse = new EnrollCourse
            //{
            //    EnrollCourseId = await AutoGenerateEnrollID(),
            //    UserId = userId,
            //    CourseId = courseId,
            //    CourseVersionId = latestCourseVersion.CourseVersionId,
            //    StartEnrollDate = DateTime.Now,
            //    EndEnrollDate = DateTime.Now.AddMonths(1),
            //    CreatedDate = DateTime.Now,
            //    Status = "Enrolled",
            //    Process = 0
            //};
            latestCourseVersion.Status = "Enrolled";

           _context.EnrollCourses.Update(latestCourseVersion);
            await _context.SaveChangesAsync();

            return latestCourseVersion.EnrollCourseId;
        }


        public async Task<CourseVersion> GetLatestCourseVersionAsync(string courseId)
        {
            return await _context.CourseVersions
                .Where(cv => cv.CourseId == courseId && cv.IsUsed)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetCourseVersionDetailIdAsync(int courseVersionId)
        {
            var courseVersionDetail = await _context.CourseVersionDetails
                .Where(cvd => cvd.CourseVersionId == courseVersionId)
                .FirstOrDefaultAsync();

            if (courseVersionDetail == null)
            {
                throw new Exception("No course version detail found for the given course version.");
            }

            return courseVersionDetail.CourseVersionDetailId;
        }

        public async Task<List<string>> GetCourseContentIdAsync(string courseVersionDetailId)
        {
            var courseContents = await _context.CourseContents
                .Where(cc => cc.CourseVersionDetailId == courseVersionDetailId)
                .Select(cc => cc.CourseContentId)
                .ToListAsync();
            return courseContents;
        }

        public async Task AddUserProcessesAsync(UserProcess userProcesses)
        {
            _context.UserProcesses.Add(userProcesses);
            await _context.SaveChangesAsync();
        }
        private async Task<string> AutoGenerateEnrollID()
        {
            int count = await _context.EnrollCourses.CountAsync() + 1;
            string Ec = "EC";
            string paddedNumber = count.ToString().PadLeft(8, '0');
            string ECID = Ec + paddedNumber;
            return ECID;
        }



        public async Task<string> GetEnrolledCourseIdByUserId(string userId, int courseVersionId)
        {
            return await _context.EnrollCourses.Where(x => x.UserId == userId && x.CourseVersionId == courseVersionId).Select(x => x.EnrollCourseId).FirstOrDefaultAsync();
        }

        public async Task UpdateProcess(string enrollCourseId, double process)
        {
            EnrollCourse enrollCourse = await _context.EnrollCourses.Where(x => x.EnrollCourseId == enrollCourseId).FirstOrDefaultAsync();
            enrollCourse.Process += process;
            await _context.SaveChangesAsync();
        }

       
    }
}
