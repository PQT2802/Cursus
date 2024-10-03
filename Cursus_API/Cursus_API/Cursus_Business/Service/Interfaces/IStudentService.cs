using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IStudentService
    {
        public Task<dynamic> GetListEnrolledCourseById(string userId, CourseListConfigForStudent config);
        public  Task<dynamic> GetEnrolledCourseDetailById(string userId, string courseId);
    }
}
