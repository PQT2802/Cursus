using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public class UserProcessError
    {
        public static Error CourseContentIdDoesNotExist => new("Null", $"Course content ID is not exist");
        public static Error NotComplete => new("Error", "You must have completed the previous course first.");
    }
}
