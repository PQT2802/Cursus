using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface  IStudentRepository
    {
        Task<IEnumerable<EnrolledCourseListDTO>> GetListEnrolledCourseById(string userId, CourseListConfigForStudent config);
        public Task<EnrolledCourseDetail> GetEnrolledCourseDetailById(string userId, string courseId);
    }
}
