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
    public class CourseCommentRepository : ICourseCommentRepository
    {
        private readonly LMS_CursusDbContext _context;

        public CourseCommentRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }
        public async Task AddCourseComment(CourseComment courseComment)
        {
            await _context.CourseComments.AddAsync(courseComment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckCourseComment(int courseCommentId)
        {
            return await _context.CourseComments.AnyAsync(x => x.CourseCommentId == courseCommentId);
        }

        public async Task<List<CourseComment>> GetAllCourseComments()
        {
            return await _context.CourseComments.ToListAsync();
        }

        public async Task<List<CourseComment>> GetAllCourseCommentsOfAdmin()
        {
            return await _context.CourseComments.Where(x => x.IsAdmin == true).ToListAsync();
        }

        public async Task<bool> HideCourseComment(int courseCommnetId)
        {
            CourseComment c = _context.CourseComments.FirstOrDefault(x => x.CourseCommentId == courseCommnetId);
            c.IsHide = !c.IsHide;
            await _context.SaveChangesAsync();
            if (c.IsHide == true) return true;
            return false;
        }

        public async Task<List<CourseReviewDTO>> GetCourseReviewByCourseVersionId(int courseVersionId)
        {
            List<CourseComment> courseVersionList = await _context.CourseComments
                .Where(x => x.ToCourseVersionId == courseVersionId && x.IsHide == false).ToListAsync();
            var courseReviewList = new List<CourseReviewDTO>();
            foreach (var courseReview in courseVersionList)
            {
                var userId = courseReview.FromUserId;
                var email = await GetEmailByUserId(userId);
                courseReviewList.Add(new CourseReviewDTO
                {
                    UserId = userId,
                    Email = email,
                    Comment = courseReview.Description,
                    Rating = 0
                });
            }
            return courseReviewList;
        }

        private async Task<string> GetEmailByUserId(string userId)
        {
            return await _context.Users.Where(x => x.UserID == userId).Select(x => x.Email).FirstOrDefaultAsync();
        }
       public async Task<bool> CheckIfEnrolled(string userId, int courseVersionId)
        {
            return await _context.EnrollCourses.AnyAsync(ec => ec.UserId == userId && ec.CourseVersionId == courseVersionId && ec.Status == "Enrolled");
        }

    }
}
