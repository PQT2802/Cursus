using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("UserProcess")]
    public class UserProcess
    {
        [Key]
        public int UserProcessId {  get; set; }
        public string EnrollCourseId { get; set; }
        [ForeignKey("EnrollCourseId")]
        public virtual EnrollCourse EnrollCourse { get; set; }
        public string CourseContentId { get; set; }
        [ForeignKey("CourseContentId")]
        public virtual CourseContent CourseContent { get; set; }
        public bool IsComplete { get; set; }
    }
}
