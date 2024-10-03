using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CourseListDTO
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Category { get; set; }
        public string Instructor {  get; set; }
        public int NumberOfStudent {  get; set; }
        public double TotalPurchasedMoney { get; set; }
        public decimal Rating { get; set; }
        public decimal VersionOfCourse { get; set; }
        public string Status { get; set; }
        //public string AdminCommnet { get; set; }
    }
}
