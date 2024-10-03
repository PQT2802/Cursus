using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Admin
{
    public class CourseDetailForAdminDTO
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string InstructorName { get; set; }
        public string Description { get; set; } 
        public  double Price { get; set; }
        public  double OldPrice { get; set; }
        public string Status { get; set; }
        public double EarnedMoney { get; set; }
        public List<CourseContent> Content { get; set; }
        public List<StudentCommentDTO> studentComment { get; set; }

    }


    public class StudentCommentDTO
    {
        public string StudentEmail { get; set; }
        public string? Attachment {  get; set; }
        public DateTime CreateDate { get; set; }
        public decimal CourseVerison { get; set; }
    }
}
