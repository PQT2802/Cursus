using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _contextAccessor;
        public OrderController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _contextAccessor = httpContextAccessor;
        }

        [HttpPost("order/create-order")]
        public async Task<IActionResult> CreateOrder(List<Guid> selectedCartItemIds)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _orderService.CreateOrderFromCart(c.UserId, selectedCartItemIds);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("order/view-order")]
        public async Task<IActionResult> ViewOrder([FromQuery] OrderListConfig config)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _orderService.ViewOrder(c.UserId, config);
         //   if (result.IsSuccess)
          //  {
                return Ok(result);
          //  }
       //     return BadRequest(result);
        }

        [HttpPost("order/pay-order")]
        public async Task<IActionResult> PayOrder(Guid orderId)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            Result result = await _orderService.PayUserOrder(c.UserId, orderId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
