using Cursus_Business.Common;
using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [ApiController]
    [Route("api/v1.0/")]
    [EnableCors]
    public class CourseContentController : ControllerBase
    {
        private readonly ICourseContentService _courseContentService;
        public CourseContentController(ICourseContentService courseContentService) 
        {
            _courseContentService = courseContentService;
        }

        [Authorize(Policy = "RequireInstructorRole")]
        [HttpPut("CourseContent")]
        public async Task<IActionResult> UpdateCourseContents(UpdateCourseContentDTO update)
        {
            try
            {
                Result result = await _courseContentService.UpdateCourseContents(update);
                if(result.IsSuccess) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
