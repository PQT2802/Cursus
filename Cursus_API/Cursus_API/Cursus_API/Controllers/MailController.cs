using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    [EnableCors]
    public class MailController : ControllerBase
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailServiceV3 _mailServiceV3;
        public MailController(IHttpContextAccessor httpContextAccessor, IMailServiceV3 mailServiceV3)
        {
            _httpContextAccessor = httpContextAccessor;
            _mailServiceV3 = mailServiceV3;
        }

        #region HttpPost
        [HttpPost("Mail/mail-close-course")]
        public async Task<IActionResult> SendCloseCourseMail(int id, string reason, TimeSpan? duration)
        {
            TimeSpan effectiveDuration = duration ?? TimeSpan.Zero;
            var result = await _mailServiceV3.SendCloseCourseMail(id, reason, effectiveDuration);
            return Ok(result);
        }


        #endregion
    }
}