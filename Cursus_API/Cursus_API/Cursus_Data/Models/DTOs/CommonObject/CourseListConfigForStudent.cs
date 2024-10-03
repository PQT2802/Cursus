using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.CommonObject
{
    public class CourseListConfigForStudent
    {
        public int? PageSize { get; set; } = 20;


        public int? PageIndex { get; set; } = 1;
        public SortByForStudent? SortBy { get; set; } 

        public SortDirection? SortDirection { get; set; } 
        
        public StatusForStudent? Status { get; set; }
        public int? AroundMonth { get; set; }
    }
    public enum StatusForStudent
    {
        Purchased = 0,
        Enrolled = 1,
        Completed = 2
    }
    public enum SortByForStudent
    {
        Category = 0,


        Instructor = 1,


        Title = 2,


        Id = 3,

        LatestPurchased = 4
    }

}
