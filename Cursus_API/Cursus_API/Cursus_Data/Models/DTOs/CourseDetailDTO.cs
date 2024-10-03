using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CourseDetailDTO
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public string Category {  get; set; }
        public string Description { get; set; }
        //public string Content { get; set; }
        public string? Attachments { get; set; }
        public double Price { get; set; } = 0;
        public double? OldPrice { get; set; }
        public string Status { get; set; }
        //public List<CourseVersionDetail> Sections { get; set; }
    }
}
