using Cursus_Data.Models.DTOs.Course;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IMailRepository
    {
        Task<List<EnrolledStudent>> GetEnrolledStudentOfCourse(int id);

        Task<UserEmail> GetConfirmationMailByUserIdAsync(string userId,int emailtemplateId );
    }
}
