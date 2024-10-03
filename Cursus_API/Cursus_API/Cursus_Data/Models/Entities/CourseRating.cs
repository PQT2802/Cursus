using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("CourseRating")]
    public class CourseRating
    {
        [Key]
        public int CourseRatingId { get; set; }

        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public virtual User User { get; set; }

        public int ToCourseVersionId { get; set; }
        [ForeignKey("ToCourseVersionId")]
        public virtual CourseVersion CourseVersion { get; set; }
            
        public decimal Rating { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
