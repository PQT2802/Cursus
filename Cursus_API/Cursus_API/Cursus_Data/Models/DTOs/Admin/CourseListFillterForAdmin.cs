using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Admin
{
    public class CourseListFillterForAdmin
    {

        public string GroupKey { get; set; }
        public List<CourseListForAdmin> Courses { get; set; }

    }
    public class CourseListForAdmin
    {
        public string CourseId { get; set; }
        public string CourseThumb { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string InstructorName { get; set; }
        public double Price { get; set; }
        public int CourseVerisonId { get; set; }
        public decimal CurrentVersion { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; }
        public bool IsAprroved { get; set; }
        public bool IsUsed { get; set; }
    }

    //public class CourseVersionList
    //{
    //    public int CourseVersionId { get; set; }
    //    public bool IsUsed { get; set; }
    //    public bool IsApproved { get; set; }
    //    public string Status { get; set; }
    //    public int NumberOfContent { get; set; }
    //    public string CourseId { get; set; }
    //    public string CourseTitle { get; set; }
    //    public string? CourseThumb { get; set; }
    //    public string CategoryName { get; set; }
    //    public string InstructorId { get; set; }
    //    public string InstructorName { get; set; }
    //    public List<CourseVersionList> courseVersionLists { get; set; }
    //}
}
