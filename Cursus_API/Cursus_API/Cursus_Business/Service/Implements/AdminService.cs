using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class AdminService : IAdminService
    {
		private readonly IAdminRepository _adminRepository;
		private readonly ICourseRepository _courseRepository;
		private ICourseVersionRepository _courseVersionRepository;
		public AdminService(IAdminRepository adminRepository, ICourseRepository courseRepository,ICourseVersionRepository courseVersionRepository)
		{
			_adminRepository = adminRepository;
			_courseRepository = courseRepository;
			_courseVersionRepository = courseVersionRepository;
		}

        public async Task<dynamic> ActiveCourseByAdmin(int courseVersionId)
        {
			try
			{
				if (! await _courseVersionRepository.CheckExistCourseVersion(courseVersionId))
				{
					return Result.Failure(CourseVersionError.CourseVersionIsNotExist);
				}
				var result = await _adminRepository.ActiveCourseByAdmin(courseVersionId);
				return Result.SuccessWithObject(result);
			}
			catch (Exception)
			{

				throw;
			}
        }

        public async Task<Result> DeactiveCourseByAdmin(string courseId)
        {
			try
			{
				if (!await _courseRepository.CheckExistCourse(courseId))
				{
					return Result.Failure(CourseError.CourseIsNotExist);
				}
				var result = await _adminRepository.DeactiveCourseByAdmin(courseId);
				return Result.SuccessWithObject(result);
			}
			catch (Exception)
			{

				throw;
			}
        }

        // Get all courses for admin
        public async Task<dynamic> GetCourseDetailForAdmin(string courseId)
        {
			try
			{
				var result = await _adminRepository.GetCourseDetailForAdmin(courseId);
                return Result.SuccessWithObject(result);
            }
			catch (Exception)
			{

				throw;
			}
        }

        public async Task<dynamic> GetCourseListFillterForAdmin(CourseListConfig config)
        {
			try
			{
				var result = await _adminRepository.GetCourseListFillterForAdmin(config);
				return Result.SuccessWithObject(result);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
