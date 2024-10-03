using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IEnrolledCourseRepository
    {

        
        Task<string> GetCourseVersionDetailIdAsync(int courseVersionId);
        Task<List<string>> GetCourseContentIdAsync(string courseVersionDetailId);
        Task AddUserProcessesAsync(UserProcess userProcesses);
        Task<CourseVersion> GetLatestCourseVersionAsync(string courseId);

        Task<string> EnrollInCourseAsync(string userId, string courseId);
        Task<string> GetEnrolledCourseIdByUserId(string userId, int courseVersionId);
        Task UpdateProcess(string enrollCourseId, double process);
        Task FinishCourse(string enrollCourseId);    
        

    }
}
