using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Course
{
    public class ReSubmitCourse
    {
        public string CourseId { get; set; }
        public int? CourseVersionId { get; set; }
    }
}
