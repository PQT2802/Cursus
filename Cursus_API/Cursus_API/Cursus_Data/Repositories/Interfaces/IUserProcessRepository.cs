using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IUserProcessRepository
    {
        Task CompleteUserProcess(string enrolledCourseId, string courseContentId);
        Task<double> CountContentByEnrollCourseId(string enrolledCourseId);
        Task<int> GetUserProcessId(string enrolledCourseId, string courseContentId);
        Task<int> CheckProcess(string enrolledCourseId);
        Task<bool> CheckFinishProcess(string enrolledCourseId, string courseContentId);
    }
}
