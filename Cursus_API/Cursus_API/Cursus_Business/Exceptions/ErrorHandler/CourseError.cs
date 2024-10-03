using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public class CourseError
    {
        public static Error CategoryIdNull() => new("CategoryId", $"Category ID is empty, it's said that it's so lonely here");
        public static Error CategoryIdNotFound(string categoryId) => new("CategoryId", $"We're currently not support this category.");
        public static Error TitleAlreadyExist(string title) => new("Title", $"The course '{title}' already exist. Please update that course.");
        public static Error TitleNull() => new("Title", $"Title ID is empty, it's said that it's so lonely here");
        public static Error PriceNull() => new("Price", $"If this course is free, just type 0 for sure");
        public static Error CourseNotApproved() => new("IsApproved", $"Someone was forgot to apporve this course");
        public static Error CourseIdNull => new("CourseId", "Course Id is not null here");
        public static Error CourseIsNotExist=> new("CourseId", "Course is not exist");
        public static Error CourseIdError(string courseId) => new("CourseId", $"Course Id {courseId} is invalid");
    }
}
