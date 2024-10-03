using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface ICourseVersionRepository
    {
        Task<CourseVersion> GetCourseVersionById(int courseVersionId);
        Task UpdateCourseVersion(CourseVersion courseVersion);
        Task<CourseVersionDetailDTO> GetCourseVersionDetail(int courseVersionId);
        Task<bool> CheckExistCourseVersion(int courseVersionId);
        Task<List<CourseVersionDTO>> GetCourseVersionListByCourseId(string courseId);

        Task AddThumbImage(Image image);

        Task<IEnumerable<UICourseContentList>> GetUICourseContentListsByCourseVerion(int courseVersionId);
        Task<string> GetInstructorIdByCourseVersionId(int courseVersionId);

        Task<CourseVersionDetail> GetCourseVersionDetailById(int courseVersionId);
        Task<string> GetCourseIdFromCourseVersionId(int courseVersionId);
        Task<decimal> NewVersionCalculate(int cvId, decimal version);
        Task<CourseVersion> GetLatestVersionById(int cvId);
        //sprint5
        Task<List<int>> GetCourseVersionByCourseId(string coursesId);


        Task<int> GetCourseVersionIdInUseByCourseId (string courseId);

        Task<int> GetCourseVersionIdByCourseId(string CourseId);
        Task<int> GetCourseVersionIdByCourseVersionDetailId(string courseVersionDetailId);


    }
}
