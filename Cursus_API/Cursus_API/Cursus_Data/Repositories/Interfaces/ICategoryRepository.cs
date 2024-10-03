using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetProgramLanguageById(string ProgramLanguageId);
        Task UpdateProgramLanguage(Category programLanguage);
        Task AddProgramLanguageAsync(Category programLanguage);
        Task<bool> NameExists(string name);
        Task<bool> ParentIdExists(string parentId);
        Task<string> AutoGenerateProgramLanguageID();
        Task<bool> HasCoursesAsync(string programLanguageId);
        Task<bool> CheckParentId(string  programLanguageId);
        Task<bool> CheckProgramLanguageId(string programLanguageId);
        Task<List<CategoryUIDTO>> GetAllCategory();
        Task<IEnumerable<NestedCategoryDTO>> GetNestedCategoryDTOs();
        Task<NestedCategoryDTO> GetCategoryByIdAsync(string categoryId);

        Task<bool> CheckCategoryId(string categoryId);
        Task<List<string>> GetCategoryByUserBehavior(List<string> userBehaviors);
    }
}
