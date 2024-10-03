using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class CategoryRepository : ICategoryRepository

    {
        private readonly LMS_CursusDbContext _context;

        public CategoryRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetProgramLanguageById(string CategoryId)
        {

            return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == CategoryId);
        }

        public async Task<bool> HasCoursesAsync(string CategoryId)
        {
            return await _context.Courses.AnyAsync(x => x.CategoryId == CategoryId);
        }

        public async Task UpdateProgramLanguage(Category programLanguage)
        {

            _context.Categories.Update(programLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task AddProgramLanguageAsync(Category programLanguage)
        {
            await _context.Categories.AddAsync(programLanguage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> NameExists(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name && !c.IsDelete);
        }

        public async Task<bool> ParentIdExists(string parentId)
        {
            return await _context.Categories.AnyAsync(x => x.ParentId == parentId && !x.IsDelete);
        }

        public async Task<string> AutoGenerateProgramLanguageID()
        {
            int count = await _context.Categories.CountAsync() + 1;
            string Pl = "CT";
            string paddedNumber = count.ToString().PadLeft(4, '0');
            string ProgramLaguageId = Pl + paddedNumber;
            return ProgramLaguageId;
        }

        public async Task<bool> CheckParentId(string CategoryId)
        {
            return await _context.Categories.AnyAsync(x => x.CategoryId == CategoryId);
        }

        public async Task<bool> CheckProgramLanguageId(string CategoryId)
        {
            return await _context.Categories.AnyAsync(x => x.CategoryId == CategoryId);
        }

        public async Task<List<CategoryUIDTO>> GetAllCategory()
        {
            try
            {
                var categories = await _context.Categories
                    .Select(c => new CategoryUIDTO
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Description = c.Description,
                        ParentId = c.ParentId,
                        // Assuming you want to fetch ParentName based on ParentId
                        ParentName = _context.Categories
                            .Where(parent => parent.CategoryId == c.ParentId)
                            .Select(parent => parent.Name)
                            .FirstOrDefault(),
                        Status = c.Status
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<NestedCategoryDTO>> GetNestedCategoryDTOs()
        {
            try
            {
                var categories = await _context.Categories
                    .Where(c => c.ParentId == null)
                    .ToListAsync();

                var result = categories.Select(c => MapToDto(c)).ToList();

                // Recursively build the hierarchy
                foreach (var dto in result)
                {
                    await PopulateChildren(dto);
                }

                return result;
            }
            catch (Exception)
            {
                // Handle or log the exception as needed
                throw;
            }
        }
        private async Task PopulateChildren(NestedCategoryDTO dto)
        {
            // Fetch the children of the current category
            var children = await _context.Categories
                .Where(c => c.ParentId == dto.Id)
                .ToListAsync();

            dto.Children = children.Select(c => MapToDto(c)).ToList();

            // Recursively populate children for each child
            foreach (var childDto in dto.Children)
            {
                await PopulateChildren(childDto);
            }
        }
        public async Task<NestedCategoryDTO> GetCategoryByIdAsync(string categoryId)
        {
            try
            {
                var category = await _context.Categories
                    .Where(c => c.CategoryId == categoryId)
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    return null; // Or handle category not found as per your application's requirements
                }

                var categoryDto = MapToDto(category);
                await PopulateChildren(categoryDto);

                return categoryDto;
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                // For example: _logger.LogError(ex, "Error fetching category by ID");
                throw;
            }
        }
        private NestedCategoryDTO MapToDto(Category category)
        {
            return new NestedCategoryDTO
            {
                Id = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                Children = category.Children.Select(c => MapToDto(c)).ToList()
            };
        }
        public async Task<bool> CheckCategoryId(string categoryId)
        {
            return await _context.Categories.AnyAsync(x => x.CategoryId == categoryId);

        }

        #region UserBehavior
        public async Task<List<string>> GetCategoryByUserBehavior(List<string> userBehaviors)
        {
            var categories = new List<string?>();
            var seenCategories = new HashSet<string>();
            foreach (var behavior in userBehaviors)
            {
                string category = await GetCategoryByKey(behavior);
                if (seenCategories.Contains(category))
                {
                    string newCategory = null;
                    foreach(var remainBehavior in userBehaviors)
                    {
                        newCategory = await GetCategoryByKey(remainBehavior);
                        if (!seenCategories.Contains(newCategory)) break;
                        newCategory = null;
                    }
                    if (newCategory != null)
                    {
                        categories.Add(newCategory);
                        seenCategories.Add(newCategory);
                    }
                    else categories.Add(null);
                }
                else
                {
                    categories.Add(category);
                    seenCategories.Add(category);
                }
            }
            return categories;
        }

        private async Task<string> GetCategoryByKey(string key)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == key);
            return category?.CategoryId;        
        }

        #endregion
    }
}