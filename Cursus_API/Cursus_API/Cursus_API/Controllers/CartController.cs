using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class CartController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICartService _cartService;
        public CartController (ICartService cartService,IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _contextAccessor = httpContextAccessor;
        }

        [HttpPost("Cart/add-to-cart")]
        public async Task<IActionResult> AddCourseToCart (string courseId)
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _cartService.AddCourseToCart(c.UserId, courseId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("Cart/view-cart")]
        public async Task<IActionResult> ViewCart()
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _cartService.ViewCart(c.UserId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
