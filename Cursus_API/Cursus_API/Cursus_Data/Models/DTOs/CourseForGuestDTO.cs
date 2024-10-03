using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CourseForGuestDTO
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string ImageCourse { get; set; }
        public double Price { get; set; }
        public decimal Rating { get; set; }
        public string Category { get; set; }
        public string Instructor { get; set; }
    }
}
