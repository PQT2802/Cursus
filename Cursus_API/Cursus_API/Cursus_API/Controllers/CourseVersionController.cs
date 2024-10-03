using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class CourseVersionController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseService _courseService;
        private readonly ICourseVersionService _courseVersionService;
        private readonly IUserBehaviorService _userBehaviorService;
        private readonly IStudentService _studentService;
        private readonly IAdminService _adminService;
        private readonly IInstructorService _instructorService;

        public CourseVersionController(IHttpContextAccessor httpContextAccessor, IStudentService studentService,
            ICourseService courseService, ICourseRepository courseRepository, ICourseVersionService courseVersionService,
            IAdminService adminService, IUserBehaviorService userBehaviorService, IInstructorService instructorService)

        {
            _httpContextAccessor = httpContextAccessor;
            _courseService = courseService;
            _courseVersionService = courseVersionService;
            _userBehaviorService = userBehaviorService;
            _studentService = studentService;
            _adminService = adminService;
            _instructorService = instructorService;
        }

        [HttpGet("CourseVersion/UI/View/{courseId}")]
        public async Task<IActionResult> GetUICourseVerisonListByCourseId(string courseId)
        {
            Result result = await _courseService.GetUICourseVerisonListByCourseId(courseId);
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("CourseVersion/{coursesId}")]
        public async Task<IActionResult> GetCourseVersionByCourseId(string coursesId)
        {
            var result = await _courseVersionService.GetCourseVersionByCourseId(coursesId);
            return Ok(result);
        }

        [Authorize(Policy = "RequireInstructorRole")] 
        [HttpPost("CourseVersion")]
        public async Task<IActionResult> AddCourseVersion(string courseId)
        {
            try
            {
                var result = await _courseService.AddCourseVersion(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        #region 32 Manage Courses
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("CourseVersion/activate/{courseVersionId}")]
        public async Task<IActionResult> ActivateCourseVersion(int courseVersionId)
        {
            var result = await _courseVersionService.ActivateCourseVersion(courseVersionId);
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("CourseVersion/deactivate/{courseVersionId}")]
        public async Task<IActionResult> DeactivateCourseVersion(int courseVersionId)
        {
            var result = await _courseVersionService.DeactivateCourseVersion(courseVersionId);
            return Ok(result);
        }
        #endregion
    }
}
