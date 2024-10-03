﻿using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Interfaces
{
    public interface IEnrollCourseRepository
    {
        Task<string> AutoGenerateEnrollCourseId();
        Task AddEnrollCourse(EnrollCourse enrollCourse);
    }
}
