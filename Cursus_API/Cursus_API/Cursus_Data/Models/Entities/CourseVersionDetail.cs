using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("CourseVersionDetail")]
        public class CourseVersionDetail
        {
            [Key]
            public string CourseVersionDetailId { get; set; }

            [ForeignKey("CourseVersion")]
            public int CourseVersionId { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public double OldPrice { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public DateTime UpdatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public decimal Rating { get; set; }
            public int AlreadyEnrolled { get; set; }
            public string CourseLearningTime { get; set; }
            public bool IsDelete { get; set; }

            // Navigation properties
            public virtual CourseVersion CourseVersion { get; set; }
            public virtual ICollection<CourseContent> CourseContents { get; set; }
            public virtual ICollection<Image> Images { get; set; }
        }


}
