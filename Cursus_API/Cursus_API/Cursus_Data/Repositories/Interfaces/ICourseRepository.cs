using Cursus_Data.Models.Entities;
﻿using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.UI;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cursus_Data.Models.DTOs.Course;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task AddCourseAsync(Course course);
        void UpdateCourse(Course course);
        Task SaveChangeCourseAsync();
        Task AddCourseVersionAsync(CourseVersion courseVersion);
        Task AddCourseVersionDetailAsync(CourseVersionDetail courseVersionDetail);
        Task<bool> CheckCategoryId(string categoryId);
        Task<bool> CheckTitleExists(string title);
        Task<string> AutoGenerateCourseID();
        Task<string> AutoGenerateCourseVersionDetailID();
        Task<int> GetFirstCourseVersionIdByCourseId(string courseId);
        Task<CourseVersion> GetCourseVersionById(string courseId);
        Task<bool> CheckExistCourse(string courseId);   
        Task<CourseDTO> GetCourseById(string courseId);
        Task<Course> GetCourseByIdV2(string courseId);
        Task<List<CourseDetailDTO>> GetCoursesDetail(GetListDTO getListDTO);
        Task<List<SubmittedCourseDTO>> GetAllCoursesAsync();
        Task<Course> FindCourseByid(string id);
        Task UpdateCourseIsAcceptedAsync(CourseVersion course);
        Task<CourseDetailDTO> GetCourseDetailById(string courseId, string instructorId);
        Task<List<CourseDetailDTO>> GetCoursesDetail(GetListDTO getListDTO, string instructorId);
        Task<List<CourseListDTO>> GetCourseList(GetListDTO getListDTO);
        Task<List<UICourseListDTO>> GetCourseListForUI(string instructorId);
        Task<List<CourseQueueListDTO>> GetCourseQueueList();
        Task AddCourseVersion(CourseVersion courseVersion);
        Task SaveChangesAsync();
        Task<CourseVersion> FindCourseVersionDescending(string courseid);
        Task<CourseVersionDetail> GetCourseVersionDetail(int courseversionid);
        Task<CourseVersionDetail> UpdateCourseVersionDetailAsync(CourseVersionDetail courseVersionDetail);

        Task<List<UICourseListAdminDTO>> GetCourseListForAdminUI();
        Task<UICourseListDetailAdminDTO> GetCourseDetailForAdminUI(int courseId);
        Task<bool> CheckCourseVersionDetailId(string courseVersionDetailId);  
        Task<string> AutoGenerateCourseContentID();
        Task<bool> CheckContentTitle(string title);
        Task<bool> CheckContentUrlExist(string url);
        Task UpdateCourseLearningTimeAsync(string courseVersionDetailId);
        Task<string> GetCourseTitleAtCourseTable(string courseId);
        Task<string> AddCourseVersionDetailReturnIdAsync(CourseVersionDetail courseVersionDetail);
        Task<IEnumerable<UICourseVerisonList>> GetUICourseVerisonListByCourseId(string courseId);
        Task<UICoruse> GetUICoruseById(string courseId);
        Task<List<CourseForGuestDTO>> GetCourseForGuest();
        Task<List<CourseForGuestDTO>> SearchCourses(SearchDTO searchDTO);
        Task<List<CourseDTO>> GetCourseByUserBehavior(List<string> categoriesBehavior);
        Task<List<string>> GetCourseByTrend();
        Task<List<CourseDTO>> GetCourseByGoodFeedbacks();
        Task<int> GetNumberOfCourseGroupByCategory(string categoryName);
        Task<dynamic> UpdateCourseByCourseId (UpdateCourseDTO courseDTO);
        //sprint5
        Task<List<string>> GetCourseIdByInstructorId(string instructorId);
        Task<Course> GetCourseIsUsed(string courseId);

        Task<ReSubmitCourse> ReSubmitCourse(ReSubmitCourse reSubmitCourse);
       Task SubmitCourse (string courseId,int courseVersionId);
    }
}
