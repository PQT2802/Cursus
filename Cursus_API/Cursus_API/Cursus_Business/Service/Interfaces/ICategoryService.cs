using Cursus_Business.Common;
using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<Result>UpDateProgramLanguageInfo(ProgramLanguageDTO programLanguageDTO);
        Task<Result> UpDateProgramLanguageStatus(string programlanguageId);
        Task<Result> CreateProgramLanguage(ProgramLanguageDTO programLanguage);
        Task<Result> SoftDeleteProgramLanguageAsync(string id);
        Task<Result> GetAllCategory();
        Task<Result> GetNestedCategoryDTOs();
        Task<Result> GetCategoryByIdAsync(string categoryId);
    }
}
