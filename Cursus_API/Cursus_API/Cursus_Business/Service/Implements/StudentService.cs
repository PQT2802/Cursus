using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Student;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<dynamic> GetEnrolledCourseDetailById(string userId, string courseId)
        {
            try
            {
                var result = await _studentRepository.GetEnrolledCourseDetailById(userId, courseId);
                return Result.SuccessWithObject(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<dynamic> GetListEnrolledCourseById(string userId, CourseListConfigForStudent config)
        {
            try
            {
                var cvl = (List<EnrolledCourseListDTO>)await _studentRepository.GetListEnrolledCourseById(userId, config);
                return Result.SuccessWithObject(cvl);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
