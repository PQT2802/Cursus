using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.Course;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class BookmarkedSerivce : IBookmarkedSerivce
    {
        private readonly IBookmarkedRepository _bookmarkedRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public BookmarkedSerivce(IBookmarkedRepository bookmarkedRepository, ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _bookmarkedRepository = bookmarkedRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<dynamic> GetListBookMark(string userId)
        {
            try
            {
                List<CourseForGuestDTO> bookmarks = await _bookmarkedRepository.GetListBookMark(userId);
                if (bookmarks.Count > 0)
                {
                    return Result.SuccessWithObject(bookmarks);
                }
                return Result.Failure(Result.CreateError("Null", "Null bookmarked courses"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> AddBookmark(BookMarkDTO bookmark, string userId)
        {
           
            var course = await _courseRepository.GetCourseByIdV2(bookmark.CourseId);
            if (course == null)
            {
                return Result.Failure(Result.CreateError("Null", "No course found"));
            }
            var mark = new Bookmark
            {
                UserId = userId,
                CourseId = course.CourseId,

                BookmarkedAt = DateTime.Now,




            };
            await _bookmarkedRepository.AddBookMark(mark);
            return Result.Success();
        }

        public async Task<dynamic> RemoveBookMark(int BookMarkId)
        {
            var result = await _bookmarkedRepository.GetBookMarkById(BookMarkId);
            if (result == null)
            {
                return Result.Failure(Result.CreateError("Null", "No bookmarked found"));

            }
            await _bookmarkedRepository.RemoveBookMark(result);
            return Result.Success();
        }
    }
}
