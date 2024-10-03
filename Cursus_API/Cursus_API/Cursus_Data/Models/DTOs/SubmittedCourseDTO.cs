using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class SubmittedCourseDTO
    {

        public string CourseId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public double Price { get; set; } = 0;
        public string InstructorId { get; set; }
        public bool IsApproved { get; set; }
    }
}