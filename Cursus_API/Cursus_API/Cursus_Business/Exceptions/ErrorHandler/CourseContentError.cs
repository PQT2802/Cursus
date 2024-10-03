using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Exceptions.ErrorHandler
{
    public class CourseContentError
    {
        public static Error CrsVerDetailIdNull() => new("CourseVersionDetailId", $"We need something to identidfy where to place this content");
        public static Error CrsVerDetailIdNotExist() => new("CourseVersionDetailId", $"Not found any thing related with this Id");
        public static Error TitleNull() => new("Title", $"Title ID is so lonely here");
        public static Error TitleExist() => new("Title", $"We already have this title, please update the exist one");
        public static Error UrlNull() => new("Url", $"Where is the url?");
        public static Error UrlExist() => new("Url", $"Use another url");
        public static Error TimeNull() => new("Time", $"Need to define learning time");
        public static Error TypeNull() => new("Type", $"It is a video, a slide or a document?");
        public static Error ccIdNull() => new("CourseContentId", $"Where to update these thing?");
        public static Error ccIdNotFound() => new("CourseContentId", $"Cannot find the content, check your id");
    }
}
