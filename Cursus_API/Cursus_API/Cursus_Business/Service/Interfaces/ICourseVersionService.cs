 using System;
﻿using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
using Cursus_Business.Common;

namespace Cursus_Business.Service.Interfaces
{
    public interface ICourseVersionService
    {
        Task<dynamic> GetCourseVersionDetail(int courseVersionId);
        Task<dynamic> UpdateCoursevesiondetail(UpdateCourseDetailDTO update);
        Task<dynamic> GetCourseVersionByCourseId(string courseId);
        Task<dynamic> ActivateCourseVersion(int courseVersionId);
        Task<dynamic> DeactivateCourseVersion(int courseVersionId);
        Task<dynamic> GetUICourseContentListsByCourseVerion(int courseVersionId);
        Task<Result> GetCourseReviewForInstructorByCourseVersionId(string instructorId, string courseVersionId);
        Task<Result> UpdateDeactivedStatus(int id, TimeSpan maintainDays);
    }
}
