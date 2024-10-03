using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.Entities
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public string ImageId { get; set; }

        [ForeignKey("CourseVersionDetail")]
        public string CourseVersionDetailId { get; set; }

        public string URL { get; set; }
        public bool IsDelete { get; set; }
        public string Type { get; set; }

        // Navigation properties
        public virtual CourseVersionDetail CourseVersionDetail { get; set; }
    }

}
