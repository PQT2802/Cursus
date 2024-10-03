using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CourseReviewDTO
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
    }
}
