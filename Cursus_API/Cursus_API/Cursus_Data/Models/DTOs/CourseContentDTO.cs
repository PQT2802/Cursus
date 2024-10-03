using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class CourseContentDTO
    {
        public string CourseVersionDetailId { get; set; }
        public string Title { get; set; }

        public IFormFile File { get; set; }

        public double Time { get; set; }
        public CourseContentType Type { get; set; }
    }
    public enum CourseContentType
    {
        Video,
        Document,
        Audio,
        Quiz
    }
}
