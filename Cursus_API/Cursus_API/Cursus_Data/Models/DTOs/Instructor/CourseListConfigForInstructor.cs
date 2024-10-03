using Cursus_Data.Models.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Instructor
{
    public class CourseListConfigForInstructor
    {
        public string GroupKey { get; set; }
        public List<CourseListForInstructor> Courses { get; set; }
    }

    public class CourseListForInstructor
    {
        public string CourseId { get; set; }
        public string CourseThumb { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public int CourseVerisonId { get; set; }
        public decimal CurrentVersion { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; }
        public bool IsAprroved { get; set; }
        public bool IsUsed { get; set; }
    }

}
