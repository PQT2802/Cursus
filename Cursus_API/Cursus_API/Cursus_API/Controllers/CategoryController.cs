using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cursus_Data.Models;
using Cursus_Data.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Cursus_Data.Context;
using Cursus_Business.Common;
using Cursus_Data.Models.DTOs;
using Cursus_Business.Service.Interfaces;
using Cursus_Business.Service.Implements;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Cors;

namespace CursusApi.Controllers
{
    [Route("api/v1.0/")]
    [ApiController]
    [EnableCors]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Category")]
        public async Task<IActionResult> AddCategory(ProgramLanguageDTO programLanguage)
        {
            try
            {
                var result = await _categoryService.CreateProgramLanguage(programLanguage);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Category")]
        public async Task<IActionResult> UpdateCategory(ProgramLanguageDTO programLanguageDTO)
        {           
                Result result = await _categoryService.UpDateProgramLanguageInfo(programLanguageDTO);
                return Ok(result);          
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("Category/status/{categoryId}")]
        public async Task<IActionResult> UpdateCategoryStatus(string categoryId)
        {
            try
            {
                Result result = await _categoryService.UpDateProgramLanguageStatus(categoryId);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("Category/{categoryId}")]
        public async Task<IActionResult> SoftDeleteProgramLanguage(string categoryId)
        {
            Result result = await _categoryService.SoftDeleteProgramLanguageAsync(categoryId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Category")]
        public async Task<IActionResult> GetAlls()
        {
            var result = await _categoryService.GetAllCategory();
            if (result.IsSuccess)
            {
                var instructors = (List<CategoryUIDTO>)result.Object;
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }         
        }

        [HttpGet("Category/nested-categories")]
        public async Task<IActionResult> GetNestedCategories()
        {

            var result = await _categoryService.GetNestedCategoryDTOs();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("Category/nested-categories/{id}")]
        public async Task<IActionResult> GetNestedCategoriesById(string id)
        {

            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
