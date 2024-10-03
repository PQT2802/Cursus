using Cursus_Business.Common;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Instructor;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IInstructorService
    {
        Task<bool> RegisterInstructorAsync(RegisterInstructorDTO registerInstructorDTO, string userid);
        Task<Result> ApproveInstructorAsync(string instructorID);
        Task<Result> RejectInstructorAsync(RejectInstructorDTO rejectDTO);
        Task<Result> GetInstructor(GetListDTO getListDTO);
        Task<List<InstructorDetailDTO>> GetInstructorForExcel(GetListDTO getListDTO);
        Task<Result> DeactivateActivateInstructor(string instructorid);
        Task<Result> UpdateInfomationInstructor(UpdateInstructorDTO UpdateInstructorDTO);
        //Task<dynamic> GetInstructorDetail(string instructorID);
        Task<dynamic> SendApprovedInstructorMail(string id);
        Task<dynamic> SendRejectedInstructorMail(RejectInstructorDTO reject);

        Task<dynamic> GetAllInstructors();

        Task<string> GetInstructorId(string userid);
        Task<Result> GetCourseListFillterForInstructor(string userId, CourseListConfigForInstrucor config);
    }
}
