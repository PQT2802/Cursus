using Cursus_Business.Common;
using Cursus_Data.Models.DTOs.Admin;
using Cursus_Data.Models.DTOs.CommonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IAdminService 
    {
        public Task<dynamic> GetCourseDetailForAdmin(string courseId);
        public Task<Result> DeactiveCourseByAdmin(string courseId);
        public Task<dynamic> ActiveCourseByAdmin(int courseVersionId);
        public Task<dynamic> GetCourseListFillterForAdmin(CourseListConfig config);
    }
}
