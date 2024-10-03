using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public static class CourseVersionError
    {
        public static Error NullVersionOfCourse => new("Null", $"No versions of this course exist");
        public static Error WrongInputId(int courseVersionId) => new("Wrong input", $"Course version ID {courseVersionId} is wrong");
        public static Error ActivatedCourse(decimal version) => new("Error", $"There is a version {version} being activated");
        public static Error DeactivatedCourse => new("Error", $"There is currently no version activated");
        public static Error AlreadyDeactivated => new("Error", $"The course is already deactivated");

        public static Error WrongCourseOfInstructor => new("Error", "This is not your course");
        public static Error CvIdNotFound => new("CourseVersionId", $"CvId not found");
        public static Error CvIdNull => new("CourseVersionId", $"CvId is empty");
        public static Error CvIdIsApporved => new("CourseVersionId", $"This version is right now offically used and cannot change, if you want some changes, please create new version to place content for approval");

        public static Error CourseVersionIsNotExist => new("CourseVersisonId", $"The course version is not exist");



    }
}
