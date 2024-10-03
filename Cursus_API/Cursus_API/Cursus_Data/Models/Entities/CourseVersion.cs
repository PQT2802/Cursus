using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("CourseVersion")]
    public class CourseVersion
    {
        [Key]
        public int CourseVersionId { get; set; }

        [ForeignKey("Course")]
        public string CourseId { get; set; }
        public string Status { get; set; }
        public decimal Version { get; set; }
        public bool IsApproved { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? MaintainDay { get; set; }

        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual CourseVersionDetail CourseVersionDetails { get; set; }
        public virtual ICollection<CourseVersionEmail> CourseVersionEmails { get; set; }
        public virtual ICollection<CourseComment> CourseComments { get; set; }
        public virtual ICollection<CourseRating> CourseRatings { get; set; }

    }

}
