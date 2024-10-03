using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Student
{
    public class EnrolledCourseDetail
    {
        public string CourseName { get; set; }
        public string CourseImageThumbnail { get; set; }
        public decimal Rating { get; set; }
        public string CourseSummary { get; set; }
        public string Category { get; set; }
        public string InstructorName { get; set; }
        public List<CourseContentDTO> Content { get; set; }
    }
    public class CourseContentDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Duration { get; set; }
        public bool IsCompleted { get; set; }
    }
}
