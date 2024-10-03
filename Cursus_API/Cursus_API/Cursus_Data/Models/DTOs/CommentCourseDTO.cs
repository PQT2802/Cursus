using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CommentCourseDTO
    {
        public int ToCourseVersionId { get; set; }
        public string? Description { get; set; }
        public string? Attachment { get; set; }
    }
}
