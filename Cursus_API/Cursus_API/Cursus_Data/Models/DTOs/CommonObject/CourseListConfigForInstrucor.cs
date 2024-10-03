using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.CommonObject
{
    public class CourseListConfigForInstrucor
    {
        public int? PageSize { get; set; } = 20;


        public int? PageIndex { get; set; } = 1;
        public SortByForInstructor? SortBy { get; set; }

        public SortDirection? SortDirection { get; set; }


        public StatusForInstrutor? Status { get; set; }
    }
    public enum StatusForInstrutor
    {
        Pending = 0,
        Activate = 1,
        Deactivate = 2,
        Submitted = 3,
        Re_Submitted = 4,
    }
    public enum SortByForInstructor
    {
        Category = 0,

        Title = 1,

        Id = 2,

        NumberOfPurchased = 3,

        NumberOfRating= 4,
    }


}
