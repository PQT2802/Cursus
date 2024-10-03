
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Business.Common;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
using Cursus_Data.Models.DTOs.Course;

namespace Cursus_Business.Service.Interfaces
{
    public interface ICourseService
    {
        Task<dynamic> CreateCourse(CourseDTO courseDTO, CurrentUserObject c);
        Task<dynamic> GetCourseDetailById (string courseId);
        //Task<dynamic> CreateCourseContent(CourseContentDTO courseContentDTO, CurrentUserObject c);
        Task<dynamic> CreateCourseContents(CourseContentDTO courseContentDTO, string userId);
        Task<dynamic> GetCourseById (string courseId);
        Task<dynamic> GetCoursesDetail(GetListDTO getListDTO);
        Task<List<SubmittedCourseDTO>> GetAllCourse();
        Task<Result> ApproveCourse(string courseid);
        Task<dynamic> RejectCourse(RejectCourseDTO rejectCourseDTO);
        Task<dynamic> GetCourseDetailById(string courseId, string instructorId);
        Task<dynamic> GetCoursesDetail(GetListDTO getListDTO, string instructorId);
        Task<Result> GetCoursesList(GetListDTO getListDTO);
        Task<dynamic> GetCourseListForUI(string instructorId);
        Task<dynamic> GetCourseQueueList();
        Task<dynamic> AddCourseVersion(string courseid);
        Task<dynamic> GetCourseListForAdminUI();
        Task<dynamic> GetCourseDetailForAdminUI(int courseVersionId);
        Task<dynamic> GetUICourseVerisonListByCourseId(string courseId);
        Task<dynamic> GetUICoruseById(string courseId);
        Task<dynamic> GetCourseForGuest();
        Task<dynamic> SearchCourses(SearchDTO search);
        Task<dynamic> GetCoursesByBehavior(string userId);

        Task<dynamic> GetCoursesByTrend();
        Task<dynamic> GetCourseByGoodFeedback();

        Task<Result> UpdateCourseByCourseId(UpdateCourseDTO courseDTO);
        

        Task<Result> ReSubmitCourse(ReSubmitCourse reSubmitCourse);


    }
}


