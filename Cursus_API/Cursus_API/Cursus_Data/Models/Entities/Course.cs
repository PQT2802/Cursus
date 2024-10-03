using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public string CourseId { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public string Title { get; set; }
        public string InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public virtual Instructor Instructor { get; set; }
        public decimal? CourseRating { get; set; } = 0;
        public int? AssignNumber { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<EnrollCourse> EnrollCourses { get; set; }
        public virtual ICollection<CourseVersion> CourseVersions { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }


}
