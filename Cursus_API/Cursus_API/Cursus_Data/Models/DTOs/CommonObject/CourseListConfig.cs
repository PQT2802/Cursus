using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace Cursus_Data.Models.DTOs.CommonObject
{
    public class CourseListConfig
    {

        public int? PageSize { get; set; } = 20;


        public int? PageIndex { get; set; } = 1;
        public SortBy? SortBy { get; set; }

        public SortDirection? SortDirection { get; set; } 


        public Status? Status { get; set; }
    }


    public enum SortBy
    {

        Category = 0,


        Instructor = 1,


        Title = 2,


        Id = 3
    }

    public enum SortDirection
    {

        Ascending = 0,

 
        Descending = 1
    }
    public enum Status
    {

        Activate = 0,
        Deactivate = 1,
        Submitted = 2,
        Re_Submitted = 3,

    }
    


}
