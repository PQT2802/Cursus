using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.UI.UIAdmin
{
    public class UICourseContentList
    {
        public string CourseContentId { get; set; }
        public string CourseVersionDetailId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public double Time { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
