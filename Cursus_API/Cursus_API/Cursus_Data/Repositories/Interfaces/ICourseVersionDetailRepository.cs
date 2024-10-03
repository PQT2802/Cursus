using Cursus_Data.Models.Entities;
﻿using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface ICourseVersionDetailRepository
    {
        Task<CourseVersionDetail> GetLatestCourseVersionDetailById(int courseVersionId);
        void UpdateCourseVersionDetail(CourseVersionDetail courseVersionDetail);
        Task SaveChangesCourseVersionDetailAsync();
        Task<int> GetCourseVersionIdByCvdId(string courseVersionId);
        Task<CourseVersionDetail> GetCourseVersionDetailById(string courseVersionDetailId);
        Task<bool> CheckCourseVersionDetailId(string courseVersionDetailId);
        Task<List<CourseDTO>> GetTopPurchasedCourse(int year, int? month = null, int? quarter = null);
        Task<bool> YearExists(int year);
        Task<bool> MonthExists(int month);
        Task<bool> QuarterExists(int year, int quarter);
        Task<List<CourseDTO>> GetTopBadCourse(int year, int? month = null, int? quarter = null);
        //sprint5
        Task<double> GetPriceByCourseVersionId(int courseVersionId);
        Task<string> GetCourseVersionDetailIdByCourseVersionId(int courseVersionId);
        Task<string> GetCourseVersionDetailIdByCourseContentId(string courseContentId);
        Task TrackingAlreadyEnrolled(int courseVersionId);
    }
}
