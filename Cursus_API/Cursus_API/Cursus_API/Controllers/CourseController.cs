using Cursus_Business.Service.Interfaces;
using Cursus_Data.Context;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Cors;
using Cursus_Business.Common;
using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cursus_Data.Models.DTOs;
using Cursus_Business.Common;
using Cursus_Data.Models.Entities;
using Cursus_Data.Models.DTOs.Course;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Admin;
using System.Security.Claims;

namespace Cursus_API.Controllers
{
    [ApiController]
    [Route("api/v1.0/")]
    [EnableCors]
    public class CourseController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseService _courseService;
        private readonly ICourseVersionService _courseVersionService;
        private readonly ICourseContentService _courseContentService;
        private readonly IUserBehaviorService _userBehaviorService;
        private readonly IStudentService _studentService;
        private readonly IAdminService _adminService;
        private readonly IInstructorService _instructorService;
        private readonly IExcelExportService _excelExportService;
        private readonly ICourseCommentService _courseCommentService;
        private readonly IEnrolledCourseService _enrolledCourseService;

        public CourseController(IHttpContextAccessor httpContextAccessor, IStudentService studentService, IExcelExportService excelExportService,

            ICourseService courseService, ICourseRepository courseRepository, ICourseVersionService courseVersionService,
            IAdminService adminService, IUserBehaviorService userBehaviorService, IInstructorService instructorService, ICourseCommentService courseCommentService, ICourseContentService courseContentService, IEnrolledCourseService enrolledCourseService)


        {
            _httpContextAccessor = httpContextAccessor;
            _courseService = courseService;
            _courseVersionService = courseVersionService;
            _courseContentService = courseContentService;
            _userBehaviorService = userBehaviorService;
            _studentService = studentService;
            _adminService = adminService;
            _instructorService = instructorService;
            _excelExportService = excelExportService;
            _courseCommentService = courseCommentService;
            _enrolledCourseService = enrolledCourseService;
        }

        #region HttpPost
        [Authorize(Policy = "RequireInstructorRole")]
        [HttpPost("Course/add-course")]
        public async Task<IActionResult> CreateCourse(CourseDTO courseDTO)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _courseService.CreateCourse(courseDTO, c);
            return Ok(result);
        }


        [Authorize(Policy = "RequireInstructorRole")]
        [HttpPost("Course/Instructor/add-courses-content")]
        public async Task<IActionResult> CreateCourseContents([FromForm] CourseContentDTO courseContentDTO)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _courseService.CreateCourseContents(courseContentDTO, c.UserId);
            return Ok(result);
        }

        [Authorize(Policy = "RequireInstructorRole")]
        [HttpPost("Course/Instructor/re-submit-course")]
        public async Task<IActionResult> ReSumitCourse([FromForm] ReSubmitCourse reSubmitCourse)
        {
            //CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _courseService.ReSubmitCourse(reSubmitCourse);
            return Ok(result);
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Course/export")]
        public async Task<IActionResult> ExportCourse([FromQuery] GetListDTO getListDTO)
        {
            Result result = await _courseService.GetCoursesList(getListDTO);
            if (result.IsSuccess)
            {
                var courseList = (List<CourseListDTO>)result.Object;
                Result exportResult = await _excelExportService.ExportToExcelAsync(courseList, "Course");
                if(exportResult.IsSuccess) return Ok(exportResult);
                return BadRequest(exportResult);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("Course/inactivated")]
        public async Task<IActionResult> InactivatedCourse(int id, TimeSpan maintainDays)
        {
            try
            {
                Result result = await _courseVersionService.UpdateDeactivedStatus(id, maintainDays);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "RequireStudentRole")]
        [HttpPost("Course/Enroll/{courseId}")]
        public async Task<IActionResult> EnrollInCourse(string courseId)
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _enrolledCourseService.EnrollInCourseAsync(c.UserId, courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error");
            }
        }
        #endregion

        #region HttpGet
        [Authorize(Policy = "RequireStudentRole")]
        [HttpGet("course/student/course-list-fillter")]
        public async Task<IActionResult> GetEnrolledCourseList([FromQuery] CourseListConfigForStudent config)
        {

            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _studentService.GetListEnrolledCourseById(c.UserId, config);
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("course/admin/course-list-fillter")]

        public async Task<IActionResult> GetCourseListFillterForAdmin([FromQuery] CourseListConfig config)
        {
            var result = await _adminService.GetCourseListFillterForAdmin(config);
            if (result.IsSuccess)
            {
                var courseList = (List<CourseListFillterForAdmin>)result.Object;
                return Ok(result);
            }
            else
            {
                return BadRequest(new
                {
                    error = result.Error
                });
            }
        }
        [Authorize(Policy = "RequireInstructorRole")]
        [HttpGet("course/instructor/course-list-fillter")]
        public async Task<IActionResult> GetCourseListForInstructor([FromQuery] CourseListConfigForInstrucor config)
        {

            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _instructorService.GetCourseListFillterForInstructor(c.UserId, config);
            return Ok(result);
        }






        [Authorize(Policy = "RequireStudentRole")]
        [HttpGet("Course/enrolled/{courseId}")]
        public async Task<IActionResult> GetEnrolledCourseDetail(string courseId)
        {
            CurrentUserObject currentUser = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _studentService.GetEnrolledCourseDetailById(currentUser.UserId, courseId);
            return Ok(result);

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Course/detail/{courseId}")]
        public async Task<IActionResult> GetCourseDetailForAdmin(string courseId)
        {
            var result = await _adminService.GetCourseDetailForAdmin(courseId);
            return Ok(result);
        }

        #region 20 View course
        //[Authorize(Policy = "RequireInstructorRole")]
        //[HttpGet("Course/detail")]
        //public async Task<IActionResult> GetCourses([FromQuery] GetListDTO getListDTO)
        //{
        //    CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
        //    string instructorid = await _instructorService.GetInstructorId(c.UserId);
        //    Result result = await _courseService.GetCoursesDetail(getListDTO, instructorid);
        //    return Ok(result);
        //}

        //[Authorize(Policy = "RequireInstructorRole")]
        //[HttpGet("course/my-course/{courseId}")]
        //public async Task<IActionResult> GetCourseById(string courseId)
        //{
        //    CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
        //    string instructorid = await _instructorService.GetInstructorId(c.UserId);
        //    Result result = await _courseService.GetCourseDetailById(courseId, instructorid);
        //    return Ok(result);
        //}

        [Authorize(Policy = "RequireStudentRole")]
        [HttpGet("course/click/{courseId}")]
        public async Task<IActionResult> ViewCourse(string courseId)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _courseService.GetCourseById(courseId);
            if (result.IsSuccess) await _userBehaviorService.UserBehaviorSearch(c.UserId, courseId);
            return Ok(result);
        }
        #endregion



        //[Authorize(Policy = "RequireAdminRole")]
        //[HttpGet("Course")]
        //public async Task<IActionResult> GetCoursesList([FromQuery] GetListDTO getListDTO)
        //{
        //    var result = await _courseService.GetCoursesList(getListDTO);
        //    if (result.IsSuccess)
        //    {
        //        var courseList = (List<CourseListDTO>)result.Object;
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(new
        //        {
        //            error = result.Error
        //        });
        //    }
        //}




        //[Authorize(Policy = "RequireAdminRole")]
        //[HttpGet("Course/queue-list")]
        //public async Task<IActionResult> GetCoursesQueueList()
        //{
        //    var result = await _courseService.GetCourseQueueList();
        //    if (result.IsSuccess)
        //    {
        //        var courseList = (List<CourseQueueListDTO>)result.Object;
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(new
        //        {
        //            error = result.Error
        //        });
        //    }
        //}

        #region 8 Home page view - LDQ
        //[Authorize(Policy = "RequireStudentRole")]
        [HttpGet("Course/suggest")]
        public async Task<IActionResult> CoursesSuggestion()
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _courseService.GetCoursesByBehavior(c.UserId);
            return Ok(result);
        }

        //[Authorize(Policy = "RequireStudentRole")]
        [HttpGet("Course/trend")]
        public async Task<IActionResult> CourseTrend()
        {
            Result result = await _courseService.GetCoursesByTrend();
            return Ok(result);
        }

        //[Authorize(Policy = "RequireStudentRole")]
        [HttpGet("Course/good-feedback")]
        public async Task<IActionResult> CourseGoodFeedback()
        {
            Result result = await _courseService.GetCourseByGoodFeedback();
            return Ok(result);
        }
        #endregion


        #region 10 View and Search course - Kiet
        [HttpGet("Course/search")]
        public async Task<IActionResult> SearchCourses([FromQuery] SearchDTO searchDTO)
        {
            var courses = await _courseService.SearchCourses(searchDTO);
            return Ok(courses);
        }

        [HttpGet("Course/for-guest")]
        public async Task<IActionResult> GetCourseForGuest()
        {
            var result = await _courseService.GetCourseForGuest();
            return Ok(result);
        }

        #endregion

        #region ADMIN UI
        //  [Authorize(Policy = "RequireAdminRole")]
        //[HttpGet("Course/UI/View/course-list")]
        //public async Task<IActionResult> GetCourseListForAdminUI()
        //{
        //    var result = await _courseService.GetCourseListForAdminUI();
        //    return Ok(result);
        //}

        //   [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Course/UI/View/course-detail")]
        public async Task<IActionResult> GetCourseDetailForAdminUI(int courseVersionId)
        {
            var result = await _courseService.GetCourseDetailForAdminUI(courseVersionId);
            return Ok(result);
        }
        #endregion

        #region Instructor UI
        //   [Authorize(Policy = "RequireInstructorRole")]
        //[HttpGet("Course/UI/View/courses")]
        //public async Task<IActionResult> GetCoursesForUI()
        //{
        //    CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
        //    string instructorid = await _instructorService.GetInstructorId(c.UserId);
        //    Result result = await _courseService.GetCourseListForUI(instructorid);
        //    return Ok(result);
        //}
        #endregion

        #region UI
        //[HttpGet("Course/UI/View")]
        //public async Task<IActionResult> GetAllCourse()
        //{
        //    var result = await _courseService.GetAllCourse();
        //    return Ok(result);
        //}

        //[HttpGet("Course/UI/View/{versionId}")]
        //public async Task<IActionResult> GetCourseByVersionId(int versionId)
        //{
        //    Result result = await _courseVersionService.GetCourseVersionDetail(versionId);
        //    return Ok(result);
        //}

        //[HttpGet("Course/UI/View/{courseId}")]
        //public async Task<IActionResult> GetUICourseByCourseId(string courseId)
        //{
        //    Result result = await _courseService.GetUICoruseById(courseId);
        //    return Ok(result);
        //}
        //[HttpGet("Course/UI/View/course-content/{courseVersionId}")]
        //public async Task<IActionResult> GetUICourseContentByVerison(int courseVersionId)
        //{
        //    Result result = await _courseVersionService.GetUICourseContentListsByCourseVerion(courseVersionId);
        //    return Ok(result);
        //}
        #endregion
        #endregion

        #region HttpPut
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Course/approve/{courseId}")]
        public async Task<IActionResult> ApproveCourse(string courseId)
        {
            try
            {
                Result result = await _courseService.ApproveCourse(courseId);
                if(result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Course/reject")]
        public async Task<IActionResult> RejectCourse(RejectCourseDTO rejectCourseDTO)
        {
            try
            {
                var result = await _courseService.RejectCourse(rejectCourseDTO);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Course/course-admin-deactivation/{courseId}")]
        public async Task<IActionResult> DeactiveCourseByAdmin(string courseId, string reason)
        {
            try
            {
                var result = await _adminService.DeactiveCourseByAdmin(courseId);
                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Course/course-admin-activation/{courseId}")]
        public async Task<IActionResult> ActiveCourseByAdmin(int courseVersionId)
        {
            try
            {
                var result = await _adminService.ActiveCourseByAdmin(courseVersionId);
                if (result.Success)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Course/Instructor/update-course")]
        public async Task<IActionResult> UpdateCourse([FromForm] UpdateCourseDTO course)
        {
            try
            {
                Result result = await _courseService.UpdateCourseByCourseId(course);
                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }


        #endregion

    }
}


