using Cursus_Data.Context;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Student;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LMS_CursusDbContext _context;

        public StudentRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task<EnrolledCourseDetail> GetEnrolledCourseDetailById(string userId, string courseId)
        {
            try
            {
                var enrolledCourseDetail = await _context.EnrollCourses
                    .Where(ec => ec.CourseId == courseId && ec.UserId == userId)
                    .Select(ec => new EnrolledCourseDetail
                    {
                        CourseName = ec.Course.Title,
                        CourseImageThumbnail = ec.Course.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => cv.CourseVersionDetails.Images
                                .FirstOrDefault(img => img.Type == "Thumb").URL)
                            .FirstOrDefault(),
                        Rating = ec.Course.CourseRating ?? 0,
                        CourseSummary = ec.Course.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => cv.CourseVersionDetails.Description)
                            .FirstOrDefault(),
                        Category = ec.Course.Category.Name, // Assuming Name is a property in Category class
                        InstructorName = ec.Course.Instructor.User.FullName, // Fetch FullName from User
                        Content = ec.UserProcesses  
                    .Where(up => up.EnrollCourseId == ec.EnrollCourseId)
                    .Select(up => new CourseContentDTO
                    {
                        Title = up.CourseContent.Title,
                        Description = up.CourseContent.Url,
                        Duration = up.CourseContent.Time,
                        IsCompleted = up.IsComplete,

                    }).ToList()
                    }).FirstOrDefaultAsync();

                return enrolledCourseDetail;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving the enrolled course details.", ex);
            }
        }

        // get list student enrolled course by course id and user id

        public async Task<IEnumerable<EnrolledCourseListDTO>> GetListEnrolledCourseById(string userId, CourseListConfigForStudent config)
        {
            try
            {
                // Determine the number of months to use (default to 0)
                int monthsToUse = config.AroundMonth ?? 0;

                // Initialize the query for enrolled courses
                var query = _context.EnrollCourses
                    .Include(ec => ec.Course)
                        .ThenInclude(c => c.CourseVersions)
                            .ThenInclude(cv => cv.CourseVersionDetails)
                                .ThenInclude(cvd => cvd.Images)
                    .Include(ec => ec.Course)
                        .ThenInclude(c => c.Category)
                    .Include(ec => ec.Course)
                        .ThenInclude(c => c.Instructor)
                            .ThenInclude(i => i.User)
                    .Include(ec => ec.UserProcesses)
                    .Where(ec => ec.UserId == userId);

                // Apply the date filter only if monthsToUse is greater than 0
                if (monthsToUse > 0)
                {
                    var cutoffDate = DateTime.UtcNow.AddMonths(-monthsToUse);
                    query = query.Where(ec => ec.StartEnrollDate >= cutoffDate); // Filter by StartEnrollDate
                }

                // Filter by status if specified, otherwise include all statuses
                if (config.Status.HasValue)
                {
                    query = query.Where(ec => ec.Status == config.Status.ToString());
                }

                // Apply sorting
                if (!config.SortBy.HasValue)
                {
                    config.SortBy = SortByForStudent.LatestPurchased; // Default sorting by LatestPurchased
                }

                switch (config.SortBy)
                {
                    case SortByForStudent.Category:
                        query = config.SortDirection == SortDirection.Ascending
                            ? query.OrderBy(ec => ec.Course.Category.Name)
                            : query.OrderByDescending(ec => ec.Course.Category.Name);
                        break;
                    case SortByForStudent.Instructor:
                        query = config.SortDirection == SortDirection.Ascending
                            ? query.OrderBy(ec => ec.Course.Instructor.User.FullName)
                            : query.OrderByDescending(ec => ec.Course.Instructor.User.FullName);
                        break;
                    case SortByForStudent.Title:
                        query = config.SortDirection == SortDirection.Ascending
                            ? query.OrderBy(ec => ec.Course.Title)
                            : query.OrderByDescending(ec => ec.Course.Title);
                        break;
                    case SortByForStudent.Id:
                        query = config.SortDirection == SortDirection.Ascending
                            ? query.OrderBy(ec => ec.EnrollCourseId)
                            : query.OrderByDescending(ec => ec.EnrollCourseId);
                        break;
                    case SortByForStudent.LatestPurchased:
                    default:
                        query = config.SortDirection == SortDirection.Ascending
                            ? query.OrderBy(ec => ec.StartEnrollDate)
                            : query.OrderByDescending(ec => ec.StartEnrollDate);
                        break;
                }

                // Apply paging
                query = query
                    .Skip(((config.PageIndex ?? 1) - 1) * (config.PageSize ?? 20))
                    .Take(config.PageSize ?? 20);

                var enrolledCourses = await query
                    .Select(ec => new EnrolledCourseListDTO
                    {
                        CourseId = ec.CourseId,
                        CourseName = ec.Course.Title,
                        CourseRating = ec.Course.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => (decimal?)cv.CourseVersionDetails.Rating)
                            .FirstOrDefault() ?? 0m, // Default to 0 if no rating is available

                        CourseImageThumbnail = ec.Course.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .SelectMany(cv => cv.CourseVersionDetails.Images
                                .Where(img => img.Type == "Thumb"))
                            .Select(img => img.URL)
                            .FirstOrDefault(), // Default to null if no image is available

                        Category = ec.Course.Category.Name,
                        InstructorName = ec.Course.Instructor.User.FullName,
                        Status = ec.Status,
                        Process = (ec.Course.CourseVersions
                                      .Where(cv => cv.IsUsed)
                                      .SelectMany(cv => cv.CourseVersionDetails.CourseContents)
                                      .Count()) != 0
                                      ? (double)ec.UserProcesses.Count(up => up.IsComplete) /
                                        (ec.Course.CourseVersions
                                          .Where(cv => cv.IsUsed)
                                          .SelectMany(cv => cv.CourseVersionDetails.CourseContents)
                                          .Count())
                                      : 1.0 // Default to 1.0 if there are no course contents
                    }).ToListAsync();

                return enrolledCourses;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving the enrolled courses.", ex);
            }
        }


    }
}
