using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class UpdateCourseContentDTO
    {
        public string CourseContentId { get; set; }
        public string CourseVersionDetailId  { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public double Time  { get; set; }
        public string Type { get; set; }
    }
}
