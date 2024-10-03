using Cursus_Business.Common;
using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class CourseCommentController : ControllerBase
    {
        private readonly ICourseCommentService _courseCommentService;
        private readonly IInstructorService _instructorService;
        private readonly ICourseVersionService _courseVersionService;

        public CourseCommentController(ICourseCommentService courseCommentService, IInstructorService instructorService, ICourseVersionService courseVersionService)
        {
            _courseCommentService = courseCommentService;
            _instructorService = instructorService;
            _courseVersionService = courseVersionService;
        }

        #region 32 Manage Courses
        [Authorize(Policy = "RequireAdminOrStudentRole")]
        [HttpPost("CourseComment/comment")]
        public async Task<IActionResult> CommentToCourse(CommentCourseDTO commentCourseDTO)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            if (c.RoleId == 3)
            {
                var result = await _courseCommentService.AdminCommentCourse(commentCourseDTO, c.UserId);
                return Ok(result);
            }
            else
            {
                var result = await _courseCommentService.CommentCourse(commentCourseDTO, c.UserId);
                return Ok(result);
            }
        }

        [HttpPost("CourseComment/Add-Report")]
        public async Task<IActionResult> AddReport(ReportByStudent report)
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _courseCommentService.ReportByStudent(report,c.UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("CourseComment/Add-Feedback")]
        public async Task<IActionResult> AddFeedback(FeedbackByStudent feedback)
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _courseCommentService.FeedbackByStudent(feedback, c.UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        #endregion

        #region 25 View reviews - LDQ
        [Authorize(Policy = "RequireInstructorRole")]
        [HttpGet("CourseComment/review/{courseVersionId}")]
        public async Task<IActionResult> ReviewMyCourse(string courseVersionId)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            string instructorId = await _instructorService.GetInstructorId(c.UserId);
            Result result = await _courseVersionService.GetCourseReviewForInstructorByCourseVersionId(instructorId, courseVersionId);
            return Ok(result);
        }

        [Authorize(Policy = "RequireInstructorRole")]
        [HttpPut("CourseComment/hide/{courseCommentId}")]
        public async Task<IActionResult> HideCourseComment(int courseCommentId)
        {
            Result result = await _courseCommentService.HideCourseCommnet(courseCommentId);
            if(result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }
        #endregion
    }
}
