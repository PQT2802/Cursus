using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
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
    public class CourseVersionDetailRepository : ICourseVersionDetailRepository
    {
        private readonly LMS_CursusDbContext _context;

        public CourseVersionDetailRepository(LMS_CursusDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> CheckCourseVersionDetailId(string courseVersionDetailId)
        {
            return await _context.CourseVersionDetails.AnyAsync(x => x.CourseVersionDetailId == courseVersionDetailId);
        }

        public async Task<CourseVersionDetail> GetCourseVersionDetailById(string courseVersionDetailId)
        {
            return await _context.CourseVersionDetails.FirstOrDefaultAsync(x => x.CourseVersionDetailId == courseVersionDetailId);
        }

        public async Task<int> GetCourseVersionIdByCvdId(string courseVersionDetailId)
        {
            var cvd = await _context.CourseVersionDetails.FirstOrDefaultAsync(x => x.CourseVersionDetailId == courseVersionDetailId);
            return cvd.CourseVersionId;
        }

        public async Task<CourseVersionDetail> GetLatestCourseVersionDetailById(int courseVersionId)
        {
            var latestCourseVersionDetail = await _context.CourseVersionDetails
            .Where(cvd => cvd.CourseVersionId == courseVersionId)
            .OrderByDescending(cvd => cvd.UpdatedDate)
            .FirstOrDefaultAsync();

            if (latestCourseVersionDetail == null)
            {
                throw new Exception("No CourseVersionDetail found for the provided CourseVersionId.");
            }

            return latestCourseVersionDetail;
        }

        public async Task SaveChangesCourseVersionDetailAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateCourseVersionDetail(CourseVersionDetail courseVersionDetail)
        {
            _context.CourseVersionDetails.Update(courseVersionDetail);
            SaveChangesCourseVersionDetailAsync();
        }

        public async Task<List<CourseDTO>> GetTopPurchasedCourse(int year, int? month = null, int? quarter = null)
        {
            var query = _context.CourseVersionDetails
         .Include(cvd => cvd.CourseVersion)
             .ThenInclude(cv => cv.Course)
         .AsQueryable();

            if (month.HasValue)
            {
                query = query.Where(cvd => cvd.CreatedDate.Year == year && cvd.CreatedDate.Month == month.Value);
            }
            else if (quarter.HasValue)
            {
                int startMonth = (quarter.Value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                query = query.Where(cvd => cvd.CreatedDate.Year == year && cvd.CreatedDate.Month >= startMonth && cvd.CreatedDate.Month <= endMonth);
            }
            else
            {
                query = query.Where(cvd => cvd.CreatedDate.Year == year);
            }

            return await query
       .OrderByDescending(cvd => cvd.AlreadyEnrolled)
       .Take(5)
       .Select(cvd => new CourseDTO
       {
           CategoryId = cvd.CourseVersion.Course.CategoryId,
           Title = cvd.CourseVersion.Course.Title,
           Description = cvd.Description,
           Price = cvd.Price
       })
       .ToListAsync();
        }
            public async Task<bool> YearExists(int year)
        {
            return await _context.CourseVersionDetails.AnyAsync(cvd => cvd.CreatedDate.Year == year);
      
        }
        public async Task<bool> MonthExists(int month)
        {
            return await _context.CourseVersionDetails.AnyAsync(cvd => cvd.CreatedDate.Month == month);
        }
        public async Task<bool> QuarterExists(int year, int quarter)
        {
            int startMonth = (quarter - 1) * 3 + 1;
            int endMonth = startMonth + 2;
            return await _context.CourseVersionDetails.AnyAsync(cvd => cvd.CreatedDate.Year == year && cvd.CreatedDate.Month >= startMonth && cvd.CreatedDate.Month <= endMonth);
        }
        public async Task<List<CourseDTO>> GetTopBadCourse(int year, int? month = null, int? quarter = null)
        {
            var query = _context.Courses
                .Include(c => c.CourseVersions).ThenInclude(cv => cv.CourseRatings)
                .AsQueryable();

            if (month.HasValue)
            {
                query = query.Where(c => c.CourseVersions
                    .Any(cv => cv.CourseRatings
                        .Any(cr => cr.CreateDate.Year == year && cr.CreateDate.Month == month.Value)));
            }
            else if (quarter.HasValue)
            {
                int startMonth = (quarter.Value - 1) * 3 + 1;
                int endMonth = startMonth + 2;
                query = query.Where(c => c.CourseVersions
                    .Any(cv => cv.CourseRatings
                       .Any(cr => cr.CreateDate.Year == year && cr.CreateDate.Month >= startMonth && cr.CreateDate.Month <= endMonth)));
            }
            else {
                query = query.Where(c => c.CourseVersions
                .Any(cv => cv.CourseRatings
                    .Any(cr => cr.CreateDate.Year == year)));
            }
            return await query
                .OrderByDescending(c => c.CourseRating)
                .Take(5)
                .Select(c => new CourseDTO
                {
                    CategoryId = c.CategoryId,
                    Title = c.Title,
                    Description = c.CourseVersions.FirstOrDefault().CourseVersionDetails.Description, 
                    Price = c.CourseVersions.FirstOrDefault().CourseVersionDetails.Price 
                })
                .ToListAsync();
        }

        public async Task<double> GetPriceByCourseVersionId(int courseVersionId)
        {
            return await _context.CourseVersionDetails.Where(x => x.CourseVersionId == courseVersionId).Select(x => x.Price * x.AlreadyEnrolled).FirstOrDefaultAsync();
        }

        public async Task<string> GetCourseVersionDetailIdByCourseVersionId(int courseVersionId)
        {
            return await _context.CourseVersionDetails.Where(x => x.CourseVersionId == courseVersionId).Select(x => x.CourseVersionDetailId).FirstOrDefaultAsync();
        }

        public async Task<string> GetCourseVersionDetailIdByCourseContentId(string courseContentId)
        {
            return await _context.CourseContents.Where(x => x.CourseContentId == courseContentId).Select(x => x.CourseVersionDetailId).FirstOrDefaultAsync();
        }

        public async Task TrackingAlreadyEnrolled(int courseVersionId)
        {
            var courseVersion = await _context.CourseVersions
                .Include(cv => cv.CourseVersionDetails)
                .FirstOrDefaultAsync(cv => cv.CourseVersionId == courseVersionId);

            if (courseVersion != null && courseVersion.CourseVersionDetails != null)
            {
                courseVersion.CourseVersionDetails.AlreadyEnrolled += 1;

                _context.CourseVersions.Update(courseVersion); // This ensures the course version is tracked as modified
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the course version or its details are not found
                throw new Exception("Course version or course version details not found.");
            }
        }
    }
}
