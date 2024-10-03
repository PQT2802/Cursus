using Cursus_Business.Common;
using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cursus_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkedController : ControllerBase
    {
        private readonly IBookmarkedSerivce _bookmarkedSerivce;

        public BookMarkedController(IBookmarkedSerivce bookmarkedSerivce)
        {
            _bookmarkedSerivce = bookmarkedSerivce;
        }
        [Authorize(Policy = "RequireStudentRole")]
        [HttpGet("Course/GetListBookMarked")]
        public async Task<IActionResult> GetBookMark()
        {
            CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
            var result = await _bookmarkedSerivce.GetListBookMark(c.UserId);
            return Ok(result);
        }

        [HttpPost("Course/AddBookMark")]
        public async Task<IActionResult> AddBookMark(BookMarkDTO book)
        {
            try
            {
                CurrentUserObject c = await TokenHelper.Instance.GetThisUserInfo(HttpContext);
                var result = await _bookmarkedSerivce.AddBookmark(book,c.UserId);
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
        [HttpPost("Course/RemoveBookmark")]
        public async Task<IActionResult> RemoveBookMark(int BookMarkId)
        {
            try
            {
                var result = await _bookmarkedSerivce.RemoveBookMark(BookMarkId);
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
    }
}
