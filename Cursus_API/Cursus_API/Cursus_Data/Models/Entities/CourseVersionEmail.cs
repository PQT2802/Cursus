using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("CourseVersionEmail")]
    public class CourseVersionEmail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("CourseVersionId")]
        public int CourseVersionId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("EmailTemplateId")]
        public int EmailTemplateId { get; set; }

        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual CourseVersion CourseVersion { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
