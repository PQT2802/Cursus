using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Instructor;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IInstructorRepository
    {
        Task AddInstructorAsync(Instructor instructor);
        Task<string> AutoGenerateInstructorID();
        Task<Instructor> FindInstructorByid(string instructorID);
        Task<List<InstructorDetailDTO>> GetAllInstructorsAsync(GetListDTO getListDTO);
        Task<UserDetail> GetUserDetailByInstructorId(string instructorId);
        Task<User> GetUserByInstructorId(string instructorId);
        Task UpdateInstructorIsAcceptedAsync(Instructor instructor);
        Task<bool> CheckInstructorId(string instructorID);
        Task<string> GetInstructorIdByUserId(string userId);
        Task<List<InstructorDetailDTO>> GetAllInstructorsAsync();

        Task<List<CourseListConfigForInstructor>> GetCourseListFillterForInstructor(string userId, CourseListConfigForInstrucor config);
    }
}