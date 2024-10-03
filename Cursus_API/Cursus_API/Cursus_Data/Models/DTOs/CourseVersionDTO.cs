using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CourseVersionDTO
    {
        public int CourseVersionId { get; set; }
        public decimal Version { get; set; }
        public string Status { get; set; }
    }
}
