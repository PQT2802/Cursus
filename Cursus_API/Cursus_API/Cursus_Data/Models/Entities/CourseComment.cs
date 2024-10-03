using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("CourseComment")]
    public class CourseComment
    {
        [Key]
        public int CourseCommentId { get; set; }
        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public virtual User User { get; set; }
        public int ToCourseVersionId { get; set; }
        [ForeignKey("ToCourseVersionId")]
        public virtual CourseVersion CourseVersion { get; set; }
        public string Description{ get; set; }
        public string? Attachment {  get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDelete {  get; set; }
        public bool IsHide {  get; set; }
        public bool? IsComment { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
