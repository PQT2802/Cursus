using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [ApiController]
    [Route("api/v1.0/")]
    [EnableCors]
    public class PaymentController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IVnPaymentService _vpnPaymentService;
        public PaymentController(IVnPaymentService vnPaymentService, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _vpnPaymentService = vnPaymentService;
        }

        #region HttpPost

        #endregion

        #region HttpGet

        [HttpGet("Payment/create-payment-url/")]
        public async Task<IActionResult> CreatePaymentUrl(double depositMoney)
        {
            var context =  _contextAccessor.HttpContext;
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            string userId = c.UserId;
            var result = await _vpnPaymentService.CreatePaymentUrl(context, depositMoney, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("payment/callback")]
        //public async Task<IActionResult> PaymentCallback([FromQuery] IQueryCollection collections, [FromQuery] string userId, [FromQuery] double depositMoney)
        public async Task<IActionResult> PaymentCallback([FromQuery] string userId, [FromQuery] double depositMoney)
        {
            var collections = HttpContext.Request.Query;
            var result = await _vpnPaymentService.PaymentExecute(collections, userId, depositMoney);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion
    }
}