using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class CategoryService : ICategoryService

    {
        private readonly ICategoryRepository _programLanguageRepository;

        public CategoryService(ICategoryRepository programLanguageRepository)
        {
            _programLanguageRepository = programLanguageRepository;

        }

        public async Task<Result> SoftDeleteProgramLanguageAsync(string id)
        {
            if (await _programLanguageRepository.HasCoursesAsync(id))
            {
                return Result.Failure(Result.CreateError("Error", "Can not delete beacause having course"));
            }

            var programLanguage = await _programLanguageRepository.GetProgramLanguageById(id);
            if (programLanguage == null)
            {
                return Result.Failure(Result.CreateError("Error", "No course"));

            }

            programLanguage.IsDelete = true;
            programLanguage.UpdateDate = DateTime.Now;
            await _programLanguageRepository.UpdateProgramLanguage(programLanguage);
            return Result.Success();

        }



        public async Task<Result> UpDateProgramLanguageInfo(ProgramLanguageDTO programLanguageDTO)
        {

            try
            {
                if (!await _programLanguageRepository.CheckParentId(programLanguageDTO.ProgramLanguageId))
                {
                    return Result.Failure(ProgramLanguageErrors.ParentIdNotExist());
                }
                if (string.IsNullOrEmpty(programLanguageDTO.Name))
                {
                    return Result.Failure(ProgramLanguageErrors.NameIsEmpty());
                }
                if (!await _programLanguageRepository.CheckProgramLanguageId(programLanguageDTO.ProgramLanguageId))
                {
                    return Result.Failure(ProgramLanguageErrors.ProgramLanguageIdNotExist());
                }
                Category p = await _programLanguageRepository.GetProgramLanguageById(programLanguageDTO.ProgramLanguageId);
                p.Name = programLanguageDTO.Name;
                p.Description = programLanguageDTO.Description;
                p.ParentId = programLanguageDTO.ParentId;
                p.UpdateDate = DateTime.Now;


                await _programLanguageRepository.UpdateProgramLanguage(p);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Result.CreateError("ID", "Id is not exist"));
            }
        }

        public async Task<Result> UpDateProgramLanguageStatus(string programlanguageId)
        {
            Category p = await _programLanguageRepository.GetProgramLanguageById(programlanguageId);
            if (p != null)
            {
                if (p.Status == Status.New || p.Status == Status.Deactivate)
                {
                    p.Status = Status.Activate;
                    await _programLanguageRepository.UpdateProgramLanguage(p);

                }
                else if (p.Status == Status.Activate)
                {
                    p.Status = Status.Deactivate;
                    await _programLanguageRepository.UpdateProgramLanguage(p);
                }
                return Result.Success();
            }
            else
            {
                return Result.Failure(Result.CreateError("ID", "Id is not exist"));
            }
        }

        public async Task<Result> CreateProgramLanguage(ProgramLanguageDTO programLanguage)
        {
            Error error = null;
            try
            {
                if (await _programLanguageRepository.NameExists(programLanguage.Name))
                {
                    error = ProgramLanguageErrors.NameDuplicated();
                    return Result.Failure(error);
                }
                if (!await _programLanguageRepository.CheckParentId(programLanguage.ParentId) && programLanguage.ParentId != "0")
                {

                    error = ProgramLanguageErrors.ParentIdNotExist();
                    return Result.Failure(error);

                }
                var categoryId = await _programLanguageRepository.AutoGenerateProgramLanguageID();
                Category p = new Category
                {
                    CategoryId = categoryId,
                    Name = programLanguage.Name,
                    Description = programLanguage.Description,
                    ParentId = programLanguage.ParentId,
                    Status = Status.New,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateBy = "Admin",
                    UpdateBy = "Admin",
                };
                await _programLanguageRepository.AddProgramLanguageAsync(p);
                return Result.Success();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Result> GetAllCategory()
        {
            try
            {
                var categories = await _programLanguageRepository.GetAllCategory();
                return Result.SuccessWithObject(categories);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Result> GetNestedCategoryDTOs()
        {
            var result = await _programLanguageRepository.GetNestedCategoryDTOs();
            return Result.SuccessWithObject(result);
        }

        public async Task<Result> GetCategoryByIdAsync(string categoryId)
        {
            var result = await _programLanguageRepository.GetCategoryByIdAsync(categoryId);
            return Result.SuccessWithObject(result);
        }
    }
}
