using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Instructor;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly LMS_CursusDbContext _context;

        public InstructorRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task AddInstructorAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateInstructorIsAcceptedAsync(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }
        public async Task<string> AutoGenerateInstructorID()
        {
            int count = await _context.Instructors.CountAsync() + 1;
            string INS = "INS";
            string paddedNumber = count.ToString().PadLeft(8, '0');
            string InstructorId = INS + paddedNumber;
            return InstructorId;
        }

        public async Task<List<InstructorDetailDTO>> GetAllInstructorsAsync(GetListDTO getListDTO)
        {
            var instructorsQuery = _context.Instructors.Include(i => i.User).Select(x => new InstructorDetailDTO
            {
                Email = x.User.Email,
                FullName = x.User.FullName,
                Phone = x.User.Phone,
                InstructorId = x.InstructorId,
                TotalCourse = _context.Courses.Where(c => c.InstructorId == x.InstructorId).Count(),
                NumberOfActivatedCourses = _context.CourseVersions.Where(c => c.Course.InstructorId == x.InstructorId && c.Status == "InProcess").Count(),
                TotalEarnedMoney = _context.CourseVersionDetails.Where(c => c.CourseVersion.Course.InstructorId == x.InstructorId).Sum(x => x.Price * x.AlreadyEnrolled),
                RatingNumber = _context.CourseVersionDetails
                                .Where(c => c.CourseVersion.Course.InstructorId == x.InstructorId)
                                .Select(c => (double?)c.Rating).Average() ?? 0,
                TotalOfPayout = 0
            }).Skip((getListDTO.pageIndex - 1) * getListDTO.pageSize).Take(getListDTO.pageSize);

            if (!string.IsNullOrEmpty(getListDTO.search) && !string.IsNullOrEmpty(getListDTO.searchBy))
            {
                instructorsQuery = getListDTO.searchBy switch
                {
                    "FullName" => instructorsQuery.Where(i => i.FullName.Contains(getListDTO.search)),
                    "Email" => instructorsQuery.Where(i => i.Email.Contains(getListDTO.search)),
                    "Phone" => instructorsQuery.Where(i => i.Phone.Contains(getListDTO.search)),
                    "InstructorId" => instructorsQuery.Where(i => i.InstructorId.ToString().Contains(getListDTO.search)),
                    _ => instructorsQuery
                };
            }

            if (!string.IsNullOrEmpty(getListDTO.sortBy))
            {
                var sortProperty = getListDTO.sortBy.ToLower() switch
                {
                    "fullname" => "FullName",
                    "email" => "Email",
                    "phone" => "Phone",
                    "instructorid" => "InstructorId",
                    _ => "FullName"
                };
                var sortDirection = getListDTO.sort?.ToUpper() == "DESC" ? "descending" : "ascending";
                instructorsQuery = instructorsQuery.OrderBy($"{sortProperty} {sortDirection}");
            }
            return await instructorsQuery.ToListAsync(); ;
        }


        public async Task<UserDetail> GetUserDetailByInstructorId(string instructorId)
        {
            var instructor = await _context.Instructors.Include(x => x.User).ThenInclude(x => x.UserDetail).FirstOrDefaultAsync(x => x.InstructorId == instructorId);
            return instructor?.User?.UserDetail;
        }

        public async Task<User> GetUserByInstructorId(string instructorId)
        {
            var instructor = await _context.Instructors.Include(x => x.User).FirstOrDefaultAsync(x => x.InstructorId == instructorId);
            return instructor?.User;
        }

        public async Task<Instructor> FindInstructorByid(string instructorID)
        {
            return await _context.Instructors.FirstOrDefaultAsync(x => x.InstructorId == instructorID);
        }

        public async Task<bool> CheckInstructorId(string instructorID)
        {
            return await _context.Instructors.AnyAsync(x => x.InstructorId == instructorID);
        }

        public async Task<List<InstructorDetailDTO>> GetAllInstructorsAsync()
        {
            try
            {
                var instructorsQuery = _context.Instructors.Include(i => i.User).Select(x => new InstructorDetailDTO
                {
                    UserId = x.UserId,
                    Email = x.User.Email,
                    FullName = x.User.FullName,
                    Phone = x.User.Phone,
                    InstructorId = x.InstructorId,
                    TotalCourse = _context.Courses.Where(c => c.InstructorId == x.InstructorId).Count(),
                    NumberOfActivatedCourses = _context.CourseVersions.Where(c => c.Course.InstructorId == x.InstructorId && c.Status == "InProcess").Count(),
                    TotalEarnedMoney = _context.CourseVersionDetails.Where(c => c.CourseVersion.Course.InstructorId == x.InstructorId).Sum(x => x.Price * x.AlreadyEnrolled),
                    RatingNumber = _context.CourseVersionDetails
                                .Where(c => c.CourseVersion.Course.InstructorId == x.InstructorId)
                                .Select(c => (double?)c.Rating).Average() ?? 0,
                    TotalOfPayout = 0
                });
                return await instructorsQuery.ToListAsync(); ;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task<string> GetInstructorIdByUserId(string userId)
        {
            return await _context.Instructors.Where(x => x.UserId == userId).Select(x => x.InstructorId).FirstOrDefaultAsync();

        }

        public async Task<List<CourseListConfigForInstructor>> GetCourseListFillterForInstructor(string userId, CourseListConfigForInstrucor config)
        {
            try
            {
                var coursesQuery = _context.Courses
                    .Include(c => c.Instructor)
                    .ThenInclude(i => i.User)
                    .Include(c => c.Category)
                    .Include(c => c.CourseVersions)
                    .ThenInclude(cv => cv.CourseVersionDetails)
                    .ThenInclude(cvd => cvd.Images)
                    .AsQueryable();

                // Filter courses by instructor
                coursesQuery = coursesQuery.Where(c => c.Instructor.UserId == userId);

                // Apply status filter
                if (config.Status.HasValue)
                {
                    var status = config.Status.Value.ToString();
                    coursesQuery = coursesQuery.Where(c => c.CourseVersions.Any(cv => cv.Status == status));
                }

                // Apply sorting
                if (!config.SortBy.HasValue)
                {
                    config.SortBy = SortByForInstructor.Category; // Default sorting by Category
                }

                switch (config.SortBy)
                {
                    case SortByForInstructor.Category:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.Category.Name)
                            : coursesQuery.OrderByDescending(c => c.Category.Name);
                        break;
                    case SortByForInstructor.Title:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.Title)
                            : coursesQuery.OrderByDescending(c => c.Title);
                        break;
                    case SortByForInstructor.Id:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.CourseId)
                            : coursesQuery.OrderByDescending(c => c.CourseId);
                        break;
                    case SortByForInstructor.NumberOfPurchased:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.CourseVersions.Sum(cv => cv.CourseVersionDetails.AlreadyEnrolled))
                            : coursesQuery.OrderByDescending(c => c.CourseVersions.Sum(cv => cv.CourseVersionDetails.AlreadyEnrolled));
                        break;
                    case SortByForInstructor.NumberOfRating:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.CourseVersions.Average(cv => cv.CourseVersionDetails.Rating))
                            : coursesQuery.OrderByDescending(c => c.CourseVersions.Average(cv => cv.CourseVersionDetails.Rating));
                        break;
                }

                // Apply pagination
                coursesQuery = coursesQuery
                    .Skip(((config.PageIndex ?? 1) - 1) * (config.PageSize ?? 20))
                    .Take(config.PageSize ?? 20);

                // Execute the query
                var courses = await coursesQuery.ToListAsync();

                // Group courses based on the sorting criteria and order by CourseId within each group
                var groupedCourses = new List<CourseListConfigForInstructor>();

                switch (config.SortBy)
                {
                    case SortByForInstructor.Category:
                        groupedCourses = courses
                            .GroupBy(c => c.Category.Name)
                            .Select(g => new CourseListConfigForInstructor
                            {
                                GroupKey = g.Key,
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForInstructor(c)).ToList()
                            }).ToList();
                        break;
                    case SortByForInstructor.Title:
                        groupedCourses = courses
                            .GroupBy(c => c.Title)
                            .Select(g => new CourseListConfigForInstructor
                            {
                                GroupKey = g.Key,
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForInstructor(c)).ToList()
                            }).ToList();
                        break;
                    case SortByForInstructor.Id:
                        groupedCourses = courses
                            .GroupBy(c => c.CourseId)
                            .Select(g => new CourseListConfigForInstructor
                            {
                                GroupKey = g.Key,
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForInstructor(c)).ToList()
                            }).ToList();
                        break;
                    case SortByForInstructor.NumberOfPurchased:
                        groupedCourses = courses
                            .GroupBy(c => c.CourseVersions.Sum(cv => cv.CourseVersionDetails.AlreadyEnrolled))
                            .Select(g => new CourseListConfigForInstructor
                            {
                                GroupKey = g.Key.ToString(),
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForInstructor(c)).ToList()
                            }).ToList();
                        break;
                    case SortByForInstructor.NumberOfRating:
                        groupedCourses = courses
                            .GroupBy(c => c.CourseVersions.Average(cv => cv.CourseVersionDetails.Rating))
                            .Select(g => new CourseListConfigForInstructor
                            {
                                GroupKey = g.Key.ToString(),
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForInstructor(c)).ToList()
                            }).ToList();
                        break;
                }

                return groupedCourses;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw new Exception("An error occurred while retrieving the course list.", ex);
            }
        }

        private CourseListForInstructor MapToCourseListForInstructor(Course c)
        {
            var courseThumb = c.CourseVersions
                //.Where(cv => cv.IsUsed)
                .SelectMany(cv => cv.CourseVersionDetails.Images)
                .FirstOrDefault(img => img.Type == "Thumb")?.URL ?? "default_thumb_url"; // Replace with your default thumbnail URL

            return new CourseListForInstructor
            {
                CourseId = c.CourseId,
                CourseThumb = courseThumb,
                Title = c.Title,
                Description = c.CourseVersions
                    //.Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.Description)
                    .FirstOrDefault(),
                CategoryName = c.Category?.Name ?? "N/A",
                Price = c.CourseVersions
                 //   .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.Price)
                    .FirstOrDefault(),
                CourseVerisonId = c.CourseVersions
                   // .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionId)
                    .FirstOrDefault(),
                CurrentVersion = c.CourseVersions
                  //  .Where(cv => cv.IsUsed)
                    .Select(cv => cv.Version)
                    .FirstOrDefault(),
                CreatedDate = c.CourseVersions
                  ///  .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.CreatedDate)
                    .FirstOrDefault(),
                UpdatedDate = c.CourseVersions
                  //  .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.UpdatedDate)
                    .FirstOrDefault(),
                Status = c.CourseVersions
                //    .Where(cv => cv.IsUsed)
                    .Select(cv => cv.Status)
                    .FirstOrDefault(),
                IsAprroved = c.CourseVersions
              //      .Where(cv => cv.IsUsed)
                    .Select(cv => cv.IsApproved)
                    .FirstOrDefault(),
                IsUsed = c.CourseVersions
               //     .Where(cv => cv.IsUsed)
                    .Select(cv => cv.IsUsed)
                    .FirstOrDefault()
            };
        }
    }
}