using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class CourseContentService : ICourseContentService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly ICourseVersionRepository _courseVersionRepository;
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        private readonly LMS_CursusDbContext _CursusDbContext;

        public CourseContentService(ICourseRepository courseRepository, ICourseContentRepository courseContentRepository, ICourseVersionRepository courseVersionRepository, LMS_CursusDbContext dbContext, ICourseVersionDetailRepository courseVersionDetailRepository)
        {
            _courseRepository = courseRepository;
            _courseContentRepository = courseContentRepository;
            _courseVersionRepository = courseVersionRepository;
            _courseVersionDetailRepository = courseVersionDetailRepository;
            _CursusDbContext = dbContext;
        }

        public async Task<dynamic> CreateCourseContents(CourseContentDTO courseContentDTO, CurrentUserObject c)
        {
            var name = c.Fullname;
            try
            {
                    if (courseContentDTO.CourseVersionDetailId == null)
                    {
                        return Result.Failure(CourseContentError.CrsVerDetailIdNull());
                    }
                    if (!await _courseRepository.CheckCourseVersionDetailId(courseContentDTO.CourseVersionDetailId))
                    {
                        return Result.Failure(CourseContentError.CrsVerDetailIdNotExist());
                    }
                    if (courseContentDTO.Title == null)
                    {
                        return Result.Failure(CourseContentError.TitleNull());
                    }
                    if (courseContentDTO.Time == null)
                    {
                        return Result.Failure(CourseContentError.TimeNull());
                    }
                    if (courseContentDTO.Type == null)
                    {
                        return Result.Failure(CourseContentError.TypeNull());
                    }

                    var ccId = await _courseRepository.AutoGenerateCourseContentID();
                    CourseContent courseContent = new CourseContent()
                    {
                        CourseContentId = ccId,
                        CourseVersionDetailId = courseContentDTO.CourseVersionDetailId,
                        Title = courseContentDTO.Title,
                        Time = courseContentDTO.Time,
                        Type = courseContentDTO.Type.ToString(),
                        CreatedDate = DateTime.Now,
                        CreatedBy = name,
                        UpdatedDate = DateTime.Now,
                        UpdatedBy = name,
                        IsDelete = false,
                    };
                    await _courseContentRepository.AddCourseContentAsync(courseContent);
                    await _courseRepository.UpdateCourseLearningTimeAsync(courseContentDTO.CourseVersionDetailId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Result.Success();
        }

        public async Task<Result> UpdateCourseContents(UpdateCourseContentDTO updateCourseContentDTO)
        {
            if (updateCourseContentDTO.CourseContentId == null)
            {
                return Result.Failure(CourseContentError.ccIdNull());
            }
            if (!await _courseContentRepository.CheckCourseContentId(updateCourseContentDTO.CourseContentId))
            {
                return Result.Failure(CourseContentError.ccIdNotFound());
            }
            if (updateCourseContentDTO.CourseVersionDetailId == null)
            {
                return Result.Failure(CourseContentError.CrsVerDetailIdNull());
            }
            if (!await _courseVersionDetailRepository.CheckCourseVersionDetailId(updateCourseContentDTO.CourseVersionDetailId))
            {
                return Result.Failure(CourseContentError.CrsVerDetailIdNotExist());
            }
            if (updateCourseContentDTO.Title == null)
            {
                return Result.Failure(CourseContentError.TitleNull());
            }
            if (updateCourseContentDTO.Url == null)
            {
                return Result.Failure(CourseContentError.UrlNull());
            }
            if (await _courseRepository.CheckContentUrlExist(updateCourseContentDTO.Url))
            {
                return Result.Failure(CourseContentError.UrlExist());
            }
            if (updateCourseContentDTO.Time == null)
            {
                return Result.Failure(CourseContentError.TimeNull());
            }
            if (updateCourseContentDTO.Type == null)
            {
                return Result.Failure(CourseContentError.TypeNull());
            }
            try
            {
                var cvdId = await _courseContentRepository.GetCourseVersionDetailIdByCcId(updateCourseContentDTO.CourseContentId);
                int cvId = await _courseVersionDetailRepository.GetCourseVersionIdByCvdId(cvdId);
                CourseVersion cv = await _courseVersionRepository.GetCourseVersionById(cvId);
                CourseContent cc = await _courseContentRepository.GetCourseContentByCcId(updateCourseContentDTO.CourseContentId);
                CourseContent newCc = new CourseContent
                {
                    CourseContentId = await _courseRepository.AutoGenerateCourseContentID(),
                    CourseVersionDetailId = cvdId,
                    Title = updateCourseContentDTO.Title,
                    Url = updateCourseContentDTO.Url,
                    Time = updateCourseContentDTO.Time,
                    Type = updateCourseContentDTO.Type,
                    CreatedDate = cc.CreatedDate,
                    CreatedBy = cc.CreatedBy,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = cc.UpdatedBy,
                    IsDelete = false,
                };
                await _courseContentRepository.AddCourseContentAsync(newCc);
                return Result.Success();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }
    }
}
