using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("CourseContent")]
    public class CourseContent
    {
        [Key]
        public string CourseContentId { get; set; }

        public string CourseVersionDetailId { get; set; }
        [ForeignKey("CourseVersionDetailId")]
        public virtual CourseVersionDetail CourseVersionDetail { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public double Time { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDelete { get; set; }

        // Navigation properties
        public virtual ICollection<UserProcess> UserProcesses { get; set; }
    }
}
