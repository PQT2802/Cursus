using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("UserComment")]
    public class UserComment
    {
        [Key]
        public int UserCommentId { get; set; }

        // Foreign key for FromUser
        public string FromUserId { get; set; }
        [ForeignKey(nameof(FromUserId))]
        public virtual User FromUser { get; set; }

        // Foreign key for ToUser
        public string ToUserId { get; set; }
        [ForeignKey(nameof(ToUserId))]
        public virtual User ToUser { get; set; }

        public string Description { get; set; }
        public string? Attachment { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDelete { get; set; }
        public bool IsHide { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
