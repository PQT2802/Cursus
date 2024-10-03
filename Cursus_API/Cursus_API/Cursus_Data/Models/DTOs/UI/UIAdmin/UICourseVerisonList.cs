using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.UI.UIAdmin
{
    public class UICourseVerisonList
    {
        public string CourseId { get; set; } 
        public string CourseVersionId { get; set; } 
        public decimal Version { get; set; }
        public int AlreadyEnrolled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

    }
}
