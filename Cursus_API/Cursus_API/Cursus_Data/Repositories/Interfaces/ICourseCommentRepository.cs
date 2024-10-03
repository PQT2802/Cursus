using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface ICourseCommentRepository
    {
        Task AddCourseComment(CourseComment courseComment);
        Task<bool> HideCourseComment(int courseCommnetId);
        Task<bool> CheckCourseComment(int courseCommentId);
        Task<List<CourseComment>> GetAllCourseComments();
        Task<List<CourseComment>> GetAllCourseCommentsOfAdmin();
        Task<List<CourseReviewDTO>> GetCourseReviewByCourseVersionId(int courseVersionId);
        Task<bool> CheckIfEnrolled(string userId,int courseVersionId);
    }
}
