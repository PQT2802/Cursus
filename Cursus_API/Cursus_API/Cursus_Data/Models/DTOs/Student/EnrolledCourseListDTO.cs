using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Student
{
    public class EnrolledCourseListDTO
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseRating { get; set; }
        public string CourseImageThumbnail { get; set; }
        public string Category {  get; set; }
        public string InstructorName { get; set; }
        public string Status { get; set; }
        public double Process {  get; set; }

    }
}
