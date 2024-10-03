using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class CourseVersionDetailController : ControllerBase
    {
        private readonly ICourseVersionDetailService _courseVersionDetailService;
        private readonly ICourseVersionService _courseVersionService;

        public CourseVersionDetailController(ICourseVersionDetailService courseVersionDetailService, ICourseVersionService courseVersionService) 
        {
            _courseVersionDetailService = courseVersionDetailService;
            _courseVersionService = courseVersionService;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("CourseVersionDetail/top-purchased")]
        public async Task<IActionResult> Dashboard(int year, int? month, int? quarter)
        {

            var result = await _courseVersionDetailService.GetTopPurchasedCourse(year, month, quarter);
            return Ok(result);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("CourseVersionDetail/top-badcourse")]
        public async Task<IActionResult> Dashboard1(int year, int? month, int? quarter)
        {
            var result = await _courseVersionDetailService.GetTopBadCourse(year, month, quarter);
            return Ok(result);
        }

        [Authorize(Policy = "RequireInstructorRole")]
        [HttpPut("CourseVersionDetail")]
        public async Task<IActionResult> UpdateCourseVersionDetail(UpdateCourseDetailDTO update)
        {
            try
            {
                var result = await _courseVersionService.UpdateCoursevesiondetail(update);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
