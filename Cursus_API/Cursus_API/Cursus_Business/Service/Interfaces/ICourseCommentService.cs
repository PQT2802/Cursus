using Cursus_Business.Common;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface ICourseCommentService
    {
        Task<dynamic> AdminCommentCourse(CommentCourseDTO commentCourseDTO, string UserId);
        Task<dynamic> CommentCourse(CommentCourseDTO commentCourseDTO, string UserId);

        Task<Result> HideCourseCommnet(int coursseCommentId);

        Task<dynamic> ReportByStudent(ReportByStudent report,string userId);
        Task<dynamic> FeedbackByStudent(FeedbackByStudent feedback, string userId);

    }
}
