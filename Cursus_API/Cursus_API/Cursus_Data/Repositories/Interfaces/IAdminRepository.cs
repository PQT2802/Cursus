using Cursus_Data.Models.DTOs.Admin;
using Cursus_Data.Models.DTOs.CommonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        public Task<CourseDetailForAdminDTO> GetCourseDetailForAdmin(string courseId);
        public Task<dynamic> DeactiveCourseByAdmin(string courseId);
        public Task<dynamic> ActiveCourseByAdmin(int courseVersionId);
        //public Task<List<CourseListFillterForAdmin>> GetCourseListFillterForAdmin(CourseListConfig config);
        public Task<List<CourseListFillterForAdmin>> GetCourseListFillterForAdmin(CourseListConfig config);
        
        }
}
