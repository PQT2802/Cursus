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
    public class CourseContentRepository : ICourseContentRepository
    {
        private readonly LMS_CursusDbContext _context;

        public CourseContentRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task AddCourseContentAsync(CourseContent courseContent)
        {
            await _context.CourseContents.AddAsync(courseContent);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckCourseContentId(string ccId)
        {
            return await _context.CourseContents.AnyAsync(x => x.CourseContentId == ccId);
        }

        public async Task<CourseContent> GetCourseContentByCcId(string ccId)
        {
            return await _context.CourseContents.FirstOrDefaultAsync(x => x.CourseContentId == ccId);
        }

        public async Task<string> GetCourseVersionDetailIdByCcId(string ccId)
        {
            var cc = await _context.CourseContents.FirstOrDefaultAsync(x => x.CourseContentId == ccId);
            return cc.CourseVersionDetailId;
        }

        public async Task<List<string>> GetCourseContentIdByCourseId(string courseVersionDetailId)
        {
            return await _context.CourseContents.Where(x => x.CourseVersionDetailId == courseVersionDetailId).Select(x => x.CourseContentId).ToListAsync();
        }
    }
}
