﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs.Course
{
    public class UpdateCourseDTO
    {
        public string CourseId { get; set; }
        public string? CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public IFormFile? Thumb { get; set; }
        public string? ThumbUrl { get; set; }
    }
}
