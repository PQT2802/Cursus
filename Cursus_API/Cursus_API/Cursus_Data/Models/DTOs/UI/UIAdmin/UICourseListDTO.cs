using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.UI.UIAdmin
{
    public class UICourseListAdminDTO
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Category { get; set; }
        public string Instructor { get; set; }
        public bool IsApproved { get; set; }
       //public string Status { get; set; }
        public decimal CurrentVersion { get; set; }
    }
}
