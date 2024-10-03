using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class UserProcessService : IUserProcessService
    {
        private readonly IUserProcessRepository _userProcessRepository;
        private readonly ICourseVersionRepository _courseVersionRepository;
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        private readonly IEnrolledCourseRepository _enrolledCourseRepository;
        private readonly ICourseContentRepository _courseContentRepository;

        public UserProcessService(IUserProcessRepository userProcessRepository, ICourseVersionRepository courseVersionRepository,
            ICourseVersionDetailRepository courseVersionDetailRepository, IEnrolledCourseRepository enrolledCourseRepository,
            ICourseContentRepository courseContentRepository)
        {
            _userProcessRepository = userProcessRepository;
            _courseVersionRepository = courseVersionRepository;
            _courseVersionDetailRepository = courseVersionDetailRepository;
            _enrolledCourseRepository = enrolledCourseRepository;
            _courseContentRepository = courseContentRepository;
        }

        public async Task<dynamic> StudyCourse(string userId, string courseContentId)
        {
            if (!await _courseContentRepository.CheckCourseContentId(courseContentId)) return Result.Failure(UserProcessError.CourseContentIdDoesNotExist);
            string courseVersionDetailId = await _courseContentRepository.GetCourseVersionDetailIdByCcId(courseContentId);
            int courseVersionId = await _courseVersionRepository.GetCourseVersionIdByCourseVersionDetailId(courseVersionDetailId);
            string enrolledCourseId = await _enrolledCourseRepository.GetEnrolledCourseIdByUserId(userId, courseVersionId);
            //if(await _userProcessRepository.CheckFinishProcess(enrolledCourseId, courseContentId)) return Result.Success();
            int check = await _userProcessRepository.CheckProcess(enrolledCourseId);
        //    if(check == 0) return Result.Success();
            int now = await _userProcessRepository.GetUserProcessId(enrolledCourseId, courseContentId);
            if (check != now) return Result.Failure(UserProcessError.NotComplete);
            await _userProcessRepository.CompleteUserProcess(enrolledCourseId, courseContentId);
            double numberOfContent = await _userProcessRepository.CountContentByEnrollCourseId(enrolledCourseId);
            await _enrolledCourseRepository.UpdateProcess(enrolledCourseId, numberOfContent);
            await _enrolledCourseRepository.FinishCourse(enrolledCourseId);
            return Result.Success();
        }
    }
}
