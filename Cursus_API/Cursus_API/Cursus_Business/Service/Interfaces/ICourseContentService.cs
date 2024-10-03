using Cursus_Business.Common;
using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface ICourseContentService
    {

      //  Task<dynamic> CreateCourseContents(List<CourseContentDTO> courseContentDTOs, CurrentUserObject c);
        Task<Result> UpdateCourseContents(UpdateCourseContentDTO updateCourseContentDTO);

        Task<dynamic> CreateCourseContents(CourseContentDTO courseContentDTO, CurrentUserObject c);
        //Task<dynamic> UpdateCourseContents(UpdateCourseContentDTO updateCourseContentDTO);

    }
}
