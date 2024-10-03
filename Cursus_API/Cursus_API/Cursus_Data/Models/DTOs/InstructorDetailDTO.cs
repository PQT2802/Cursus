using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class InstructorDetailDTO
    {
        public string UserId { get; set; }
        public string InstructorId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }  
        public int NumberOfActivatedCourses { get; set; }
        public int TotalCourse {  get; set; }
        public double TotalEarnedMoney { get; set; }
        public double TotalOfPayout {  get; set; }
        public double RatingNumber { get; set; }

    }
}
