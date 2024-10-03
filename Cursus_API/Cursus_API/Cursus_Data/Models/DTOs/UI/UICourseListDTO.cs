using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.UI
{
    public class UICourseListDTO
    {
        public string CourseName { get; set; }
        public string ImageCourse { get; set; }
        public string Category { get; set; }
        public int NumberOfStudent { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
    }
}
