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
    public class UserProcessController : ControllerBase
    {
        private readonly IUserProcessService _userProcessService;

        public UserProcessController(IUserProcessService userProcessService)
        {
            _userProcessService = userProcessService;
        }

        [Authorize(Policy = "RequireStudentRole")]
        [HttpPut("UserProcess/study/{courseContentId}")]
        public async Task<IActionResult> Study(string courseContentId)
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var results = await _userProcessService.StudyCourse(c.UserId, courseContentId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
