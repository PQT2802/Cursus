using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursus_Business.Exceptions.ErrorHandler;
using Hangfire.Logging.LogProviders;
using DocumentFormat.OpenXml.Bibliography;

namespace Cursus_Business.Service.Implements
{
    public class CourseVersionService : ICourseVersionService
    {
        private readonly ICourseVersionRepository _courseVersionRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        private readonly ICourseCommentRepository _courseCommentRepository;

        public CourseVersionService(ICourseVersionRepository courseVersionRepository, ICourseRepository courseRepository, 
            ICourseVersionDetailRepository courseVersionDetailRepository, ICourseCommentRepository courseCommentRepository)
        {
            _courseVersionRepository = courseVersionRepository;
            _courseRepository = courseRepository;
            _courseVersionDetailRepository = courseVersionDetailRepository;
            _courseCommentRepository = courseCommentRepository;
        }

        public async Task<dynamic> GetCourseVersionDetail(int courseVersionId)
        {
            if (courseVersionId <= 0 || courseVersionId == null)
            {
                return Result.Failure(Result.CreateError("InvalidId", "CourseVersion ID must be a positive integer."));
            }
            if (!await _courseVersionRepository.CheckExistCourseVersion(courseVersionId))
            {
                return Result.Failure(Result.CreateError("Null", "CourseVersionid not exist"));
            }
            try
            {
                CourseVersionDetailDTO courseVersionDetail = await _courseVersionRepository.GetCourseVersionDetail(courseVersionId);
                if (courseVersionDetail != null)
                {
                    return Result.SuccessWithObject(courseVersionDetail);
                }
                return Result.Failure(Result.CreateError("Null", "Cannot find course version"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);

            }
        }

        public async Task<dynamic> UpdateCoursevesiondetail(UpdateCourseDetailDTO update)
        {
            try
            {
                if (update.CourseVersionId == null)
                {
                    return Result.Failure(CourseVersionError.CvIdNull);
                }

                var courseId = await _courseVersionRepository.GetCourseIdFromCourseVersionId(update.CourseVersionId);
                string oldTitle = await _courseRepository.GetCourseTitleAtCourseTable(courseId);
                Course course = await _courseRepository.GetCourseByIdV2(courseId);

                bool isUpdated = false;

                if (!update.Title.Equals(oldTitle) && update.Title != "")
                {
                    course.Title = update.Title;
                    isUpdated = true;
                }

                CourseVersionDetail cvd = await _courseVersionDetailRepository.GetLatestCourseVersionDetailById(update.CourseVersionId);

                if (!update.Description.Equals(cvd.Description) && update.Description != "")
                {
                    cvd.Description = update.Description;
                    isUpdated = true;
                }

                if (!update.Price.Equals(cvd.Price) && update.Price != -1)
                {
                    cvd.Price = update.Price;
                    isUpdated = true;
                }
                cvd.UpdatedDate = DateTime.Now;

                if (isUpdated)
                {
                    _courseRepository.UpdateCourse(course);
                    _courseVersionDetailRepository.UpdateCourseVersionDetail(cvd);
                }

                return Result.Success();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
                
        public async Task<dynamic> ActivateCourseVersion(int courseVersionId)
        {
            if (!await _courseVersionRepository.CheckExistCourseVersion(courseVersionId))
                return Result.Failure(CourseVersionError.WrongInputId(courseVersionId));
            CourseVersion cv = await _courseVersionRepository.GetCourseVersionById(courseVersionId);
            List<CourseVersionDTO> courseVersionDTOs = await _courseVersionRepository.GetCourseVersionListByCourseId(cv.CourseId);
            var activatedCourse = courseVersionDTOs.FirstOrDefault(x => x.Status.Equals(Status.Activate, StringComparison.OrdinalIgnoreCase));
            if (activatedCourse != null)
                return Result.Failure(CourseVersionError.ActivatedCourse(activatedCourse.Version));
            cv.Status = Status.Activate;
            await _courseVersionRepository.UpdateCourseVersion(cv);
            return Result.Success();
        }

        public async Task<dynamic> DeactivateCourseVersion(int courseVersionId)
        {
            if (!await _courseVersionRepository.CheckExistCourseVersion(courseVersionId))
                return Result.Failure(CourseVersionError.WrongInputId(courseVersionId));
            CourseVersion cv = await _courseVersionRepository.GetCourseVersionById(courseVersionId);
            List<CourseVersionDTO> courseVersionDTOs = await _courseVersionRepository.GetCourseVersionListByCourseId(cv.CourseId);
            var activatedCourse = courseVersionDTOs.FirstOrDefault(x => x.Status.Equals(Status.Activate, StringComparison.OrdinalIgnoreCase));
            if (activatedCourse == null)
                return Result.Failure(CourseVersionError.DeactivatedCourse);
            if (activatedCourse.Status == Status.Deactivate)
                return Result.Failure(CourseVersionError.AlreadyDeactivated);
            cv.Status = Status.Deactivate;
            await _courseVersionRepository.UpdateCourseVersion(cv);
            return Result.Success();
        }

        public async Task<dynamic> GetCourseVersionByCourseId(string courseId)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                return Result.Failure(Result.CreateError("Null", "Course ID can not null "));
            }
            else if (!await _courseRepository.CheckExistCourse(courseId))
            {
                return Result.Failure(Result.CreateError("Null", "Can not find course "));
            }
            else
            {
                List<CourseVersionDTO> courseVersionDTOs = await _courseVersionRepository.GetCourseVersionListByCourseId(courseId);
                return courseVersionDTOs.Count > 0
                    ? Result.SuccessWithObject(courseVersionDTOs)
                    : Result.Failure(CourseVersionError.NullVersionOfCourse);

                //if (courseVersionDTOs.Count > 0)
                //    return Result.SuccessWithObject(courseVersionDTOs);
                //return Result.Failure(CourseVersionError.NullVersionOfCourse);

            }
        }

        public async Task<dynamic> GetUICourseContentListsByCourseVerion(int courseVersionId)
        {
            var ccl = await _courseVersionRepository.GetUICourseContentListsByCourseVerion(courseVersionId);
            return Result.SuccessWithObject(ccl);
        }


        public async Task<Result> GetCourseReviewForInstructorByCourseVersionId(string instructorId, string courseVersionId)
        {
            if (!int.TryParse(courseVersionId, out int parsedCourseVersionId) || parsedCourseVersionId <= 0) return Result.Failure(CourseVersionError.WrongInputId(parsedCourseVersionId));
            string insId = await _courseVersionRepository.GetInstructorIdByCourseVersionId(parsedCourseVersionId);
            if (insId != instructorId) return Result.Failure(CourseVersionError.WrongCourseOfInstructor);
            List<CourseReviewDTO> list = await _courseCommentRepository.GetCourseReviewByCourseVersionId(parsedCourseVersionId);
            return Result.SuccessWithObject(list);
        }
    
        public async Task<Result> UpdateDeactivedStatus(int id, TimeSpan maintainDays)
        {
            var courversion = await _courseVersionRepository.GetCourseVersionById(id);
            if (courversion == null)
            {
                return Result.Failure(CourseVersionError.NullVersionOfCourse);
            }

            if (!courversion.Status.Equals("Activate"))
            {
                return Result.Failure(CourseVersionError.DeactivatedCourse); // Thêm điều kiện kiểm tra nếu cần thiết
            }
            courversion.Status = "Deactivate";
                courversion.MaintainDay = DateTime.Now + maintainDays;
                _courseVersionRepository.UpdateCourseVersion(courversion);
                return Result.SuccessWithObject(courversion);
            
        }
    }
}
