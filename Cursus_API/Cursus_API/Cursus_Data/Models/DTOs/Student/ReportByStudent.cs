using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Student
{
    public class ReportByStudent
    {


        public int ToCourseVersionId { get; set; }


        public string Description { get; set; }
        public string? Attachment { get; set; }


    }
}
