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
    public class InstructorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInstructorService _instructorService;
        private readonly IExcelExportService _excelExportService;
        public InstructorController(IConfiguration configuration, IExcelExportService excelExportService,        
           IHttpContextAccessor httpContextAccessor, IMailService mailService,
           IInstructorService instructorService)
        {

            _configuration = configuration;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
            _instructorService = instructorService;
            _excelExportService = excelExportService;

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Instructor/approve/{instructorId}")]
        public async Task<IActionResult> ApproveInstructor(string instructorId)
        {
            var result = await _instructorService.ApproveInstructorAsync(instructorId);
            if (result.IsSuccess)
            {
                var resultMail = await _instructorService.SendApprovedInstructorMail(instructorId);
                if (resultMail.IsSuccess)
                {
                    return Ok(Result.SuccessWithObject(new { Message = "Sending notification to Instructor" }));
                }
            }
            return BadRequest(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Instructor/reject")]
        public async Task<IActionResult> RejectInstructor(RejectInstructorDTO rejectDTO)
        {

            var result = await _instructorService.RejectInstructorAsync(rejectDTO);
            if (result.IsSuccess)
            {
                var resultMail = await _instructorService.SendRejectedInstructorMail(rejectDTO);
                if (resultMail.IsSuccess)
                {
                    return Ok(Result.SuccessWithObject(new { Message = "Sending notification to Instructor" }));
                }
            }
            return BadRequest(result);

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Instructor")]
        public async Task<IActionResult> GetInstructors([FromQuery] GetListDTO getListDTO)
        {
            var result = await _instructorService.GetInstructor(getListDTO);
            if (result.IsSuccess)
            {
                var instructors = (List<InstructorDetailDTO>)result.Object;
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Instructor/export")]
        public async Task<IActionResult> ExportInstructors([FromQuery] GetListDTO getListDTO)
        {
            var result = await _instructorService.GetInstructor(getListDTO);
            if (result.IsSuccess)
            {
                var instructors = (List<InstructorDetailDTO>)result.Object;
                var exportResult = await _excelExportService.ExportToExcelAsync(instructors, "Instructor");
                return Ok(exportResult);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Instructor/deactivate-activate/{instructorId}")]
        public async Task<IActionResult> DeactivateActivateInstructor(string instructorId)
        {
            Result result = await _instructorService.DeactivateActivateInstructor(instructorId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Instructor")]
        public async Task<IActionResult> UpdateInstructorInfomation(UpdateInstructorDTO updateInstructorDTO)
        {
            Result result = await _instructorService.UpdateInfomationInstructor(updateInstructorDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
