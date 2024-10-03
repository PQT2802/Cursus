using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
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
    public class EnrolledCourseService : IEnrolledCourseService
    {
        private readonly IEnrolledCourseRepository _enrolledCourseRepository;

        public EnrolledCourseService(IEnrolledCourseRepository enrolledCourseRepository) 
        {
            _enrolledCourseRepository = enrolledCourseRepository;
        }
        public async Task<dynamic> EnrollInCourseAsync(string userId, string courseId)
        {          
            var enrollCourseId = await _enrolledCourseRepository.EnrollInCourseAsync(userId, courseId);           
            var latestCourseVersion = await _enrolledCourseRepository.GetLatestCourseVersionAsync(courseId);         
            var courseVersionDetailId = await _enrolledCourseRepository.GetCourseVersionDetailIdAsync(latestCourseVersion.CourseVersionId);            
            var courseContentIds = await _enrolledCourseRepository.GetCourseContentIdAsync(courseVersionDetailId);          
            var userProcesses = new List<UserProcess>();
            foreach (var courseContentId in courseContentIds)
            {
                var userProcess = new UserProcess
                {
                    EnrollCourseId = enrollCourseId,
                    CourseContentId = courseContentId,
                    IsComplete = false
                };
                await _enrolledCourseRepository.AddUserProcessesAsync(userProcess);
            }  
            return Result.Success();
        }
    }


}

