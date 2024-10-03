using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
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
    public class CourseVersionRepository : ICourseVersionRepository
    {
        private readonly LMS_CursusDbContext _context;
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        public CourseVersionRepository(LMS_CursusDbContext context, ICourseVersionDetailRepository courseVersionDetailRepository)
        {
            _context = context;
            _courseVersionDetailRepository = courseVersionDetailRepository;
        }
        public async Task<CourseVersion> GetCourseVersionById(int courseVersionId)
        {
            return await _context.CourseVersions.FirstOrDefaultAsync(x => x.CourseVersionId == courseVersionId);
        }

        public async Task UpdateCourseVersion(CourseVersion courseVersion)
        {
            _context.CourseVersions.Update(courseVersion);  
            await _context.SaveChangesAsync();
        }

        public async Task<CourseVersionDetailDTO> GetCourseVersionDetail(int courseVersionId)
        {
            var courseVersion = await _context.CourseVersions
               .Where(cv => cv.CourseVersionId == courseVersionId)
               .Select(cv => new CourseVersionDetailDTO
               {
                   CourseVersionId = cv.CourseVersionId,
                   CourseId = cv.CourseId,
                   Status = cv.Status,
                   Version = cv.Version

               })
               .FirstOrDefaultAsync();

            return courseVersion;
        }
        public async Task<bool> CheckExistCourseVersion(int courseVersionId)
        {
            return await _context.CourseVersions.AnyAsync(x => x.CourseVersionId == courseVersionId);
        }

        public async Task<List<CourseVersionDTO>> GetCourseVersionListByCourseId(string courseId)
        {
            return await _context.CourseVersions.Where(x => x.CourseId == courseId).Select(x => new CourseVersionDTO
            {
                CourseVersionId = x.CourseVersionId,
                Version = x.Version,
                Status = x.Status
            }).ToListAsync();
        }

        public Task<CourseVersionDetail> GetCourseVersionDetailById(int courseVersionId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetCourseIdFromCourseVersionId(int courseVersionId)
        {
            var courseVersion = await _context.CourseVersions
            .FirstOrDefaultAsync(cv => cv.CourseVersionId == courseVersionId && cv.IsApproved);

            if (courseVersion == null)
            {
                return null;
            }

            return courseVersion.CourseId;
        }

        public async Task<decimal> NewVersionCalculate(int cvId, decimal version)
        {
            CourseVersion latestCourseVersion = await GetLatestVersionById(cvId);
            // Increment the version number
            decimal newVersionNumber = latestCourseVersion != null
                ? latestCourseVersion.Version + 0.01m
                : 1.00m;
            return newVersionNumber;
        }

        public async Task<CourseVersion> GetLatestVersionById(int cvId)
        {
            return await _context.CourseVersions
        .Where(cv => cv.CourseVersionId == cvId)
        .OrderByDescending(cv => cv.Version)
        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UICourseContentList>> GetUICourseContentListsByCourseVerion(int courseVersionId)
        {
            var courseContents = await _context.CourseContents
               .Include(cc => cc.CourseVersionDetail)
               .ThenInclude(cc => cc.CourseVersion)
               .Where(cc => cc.CourseVersionDetail.CourseVersion.CourseVersionId == courseVersionId)
               .ToListAsync();

            return courseContents.Select(cc => new UICourseContentList
            {
                CourseContentId = cc.CourseContentId,
                Title = cc.Title,
                Url = cc.Url,
                Time = cc.Time,
                Type = cc.Type,
                CreatedDate = cc.CreatedDate,
                UpdatedDate = cc.UpdatedDate,
            }).ToList();
        }

        public async Task<string> GetInstructorIdByCourseVersionId(int courseVersionId)
        {
            string courseId = await _context.CourseVersions.Where(x => x.CourseVersionId == courseVersionId).Select(x => x.CourseId).FirstOrDefaultAsync();
            string instructorId = await _context.Courses.Where(x => x.CourseId == courseId).Select(x => x.InstructorId).FirstOrDefaultAsync();
            return instructorId;

        }
        public async Task AddThumbImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

        }

        public async Task<List<int>> GetCourseVersionByCourseId(string coursesId)
        {
            return await _context.CourseVersions.Where(x => x.CourseId == coursesId).Select(x => x.CourseVersionId).ToListAsync();
        }


        public async Task<int> GetCourseVersionIdInUseByCourseId(string courseId)
        {
            var cv = await _context.CourseVersions.FirstOrDefaultAsync(cv => cv.CourseId == courseId && cv.IsUsed);
            return cv != null ? cv.CourseVersionId : 0;
        }
        public async Task<int> GetCourseVersionIdByCourseId(string CourseId)
        {
            return await _context.CourseVersions.Where(x => x.CourseId == CourseId && x.IsUsed == true).Select(x => x.CourseVersionId).FirstOrDefaultAsync();
        }

        public async Task<int> GetCourseVersionIdByCourseVersionDetailId(string courseVersionDetailId)
        {
            return await _context.CourseVersionDetails.Where(x => x.CourseVersionDetailId == courseVersionDetailId).Select(x => x.CourseVersionId).FirstOrDefaultAsync();
        }
    }
}
