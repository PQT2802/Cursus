using Cursus_Data.Context;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class EnrollCourseRepository : IEnrollCourseRepository
    {
        private readonly LMS_CursusDbContext _context;
        public EnrollCourseRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task AddEnrollCourse(EnrollCourse enrollCourse)
        {
            _context.EnrollCourses.Add(enrollCourse);
            await _context.SaveChangesAsync();
        }

        public async Task<string> AutoGenerateEnrollCourseId()
        {
            int count = await _context.EnrollCourses.CountAsync() + 1;
            string eC = "EC";
            string paddedNumber = count.ToString().PadLeft(8, '0');
            string enrollCourseId = eC + paddedNumber;
            return enrollCourseId;
        }
    }
}
