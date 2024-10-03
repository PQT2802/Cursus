using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("EnrollCourse")]
    public class EnrollCourse
    {
        [Key]
        public string EnrollCourseId { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public int CourseVersionId { get; set; }
        [ForeignKey("CourseVersionId")]
        public virtual CourseVersion CourseVersion { get; set; }

        public DateTime StartEnrollDate { get; set; }
        public DateTime EndEnrollDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Status { get; set; }
        public double Process { get; set; }
        public virtual ICollection<UserProcess> UserProcesses { get; set; }
    }

}
