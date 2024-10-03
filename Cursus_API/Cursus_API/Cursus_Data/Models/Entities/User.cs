using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Cursus_Data.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public string FullName { get; set; }
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Password { get; set; }
        [Required]
        public string Phone {  get; set; }
        public bool? IsBan { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsMailConfirmed { get; set; }
        public bool? IsGoogleAccount { get; set; }


        // Navigation properties
        public virtual Role Role { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<EnrollCourse> EnrollCourses { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UserEmail> UserEmails { get; set; }
        public virtual ICollection<UserComment> UserComments { get; set; }
        public virtual ICollection<UserBehavior> UserBehaviors { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<CourseRating> CourseRatings { get; set; }
        public virtual ICollection<CourseComment> CourseComments { get; set; }
    }

}
