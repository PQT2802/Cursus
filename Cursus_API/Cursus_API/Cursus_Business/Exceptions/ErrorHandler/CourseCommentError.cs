using System;
using Cursus_Data.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public class CourseCommentError
    {
        public static Error ToCourseVersionIdNull => new("Null", $"Course version is not null here");
        public static Error ToCourseVersionIdWrong(int ToCourseVersionId) => new("Error", $"The course version {ToCourseVersionId} is not exist");
        public static Error ErrorToCreateComment => new("Error", "Cannot comment to this course");
        public static Error CourseCommentIdWrong(int courseCommentId) => new("Error", $"The course commnet {courseCommentId} is not exist");
        public static Error CourseCommnetIdNull => new("Null", "Course commnet id is not null here");
    }
}
