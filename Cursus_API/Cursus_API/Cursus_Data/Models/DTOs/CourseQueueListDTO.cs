using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CourseQueueListDTO
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public string InstructorId { get; set; }
        public decimal Version { get; set; }
        public string Status { get; set; }
    }
}
