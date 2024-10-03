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
    public class BookmarkedRepository:IBookmarkedRepository
    {
        private readonly LMS_CursusDbContext _context;

        public BookmarkedRepository(LMS_CursusDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<CourseForGuestDTO>> GetListBookMark(string userId)
        {
            var bookmarkedCourses = await _context.Bookmarks
                .Include(b => b.Course)
                    .ThenInclude(c => c.CourseVersions)
                        .ThenInclude(cv => cv.CourseVersionDetails)
                            .ThenInclude(cvd => cvd.Images)
                .Include(b => b.Course.Category)
                .Include(b => b.Course.Instructor)
                .Where(b => b.UserId == userId)
                .Select(bookmark => new CourseForGuestDTO
                {
                    CourseName = bookmark.Course.Title,
                    ImageCourse = "image",
                    Price = bookmark.Course.CourseVersions.Select(x => x.CourseVersionDetails.AlreadyEnrolled * x.CourseVersionDetails.Price).FirstOrDefault(),
                    Rating = bookmark.Course.CourseRating ?? 0,
                    Category = bookmark.Course.Category.Name,
                    Instructor = bookmark.Course.InstructorId,
                })
            .ToListAsync();

            return bookmarkedCourses;
            
        }
        public async Task AddBookMark(Bookmark mark)
        {
            await _context.Bookmarks.AddAsync(mark);
            await _context.SaveChangesAsync();


        }

        public async Task RemoveBookMark(Bookmark mark)
        {
            _context.Bookmarks.Remove(mark);
            await _context.SaveChangesAsync();
        }


        public async Task<Bookmark> GetBookMarkById(int id)
        {
            return await _context.Bookmarks.FirstOrDefaultAsync(c => c.BookmarkId == id);
        }
    }
}
