using Cursus_Data.Context;
using Cursus_Data.Models.DTOs.Admin;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LMS_CursusDbContext _context;
        public AdminRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task<dynamic> ActiveCourseByAdmin(int courseVersionId)
        {
            try
            {
                var courseVersion = await _context.CourseVersions
                    .Include(cv => cv.Course)
                    .Where(cv => cv.CourseVersionId == courseVersionId)
                    .FirstOrDefaultAsync();

                if (courseVersion == null)
                {
                    return new {message = "Course version not found" };
                }

                courseVersion.Status = "Active";
                courseVersion.IsApproved = true;
                courseVersion.IsUsed = false;// Set status to active

                await _context.SaveChangesAsync();

                return new { message = "Course version activated successfully", courseId = courseVersion.CourseId };
            }
            catch (Exception ex)
            {
                // Log exception (ex) if necessary
                return new { success = false, message = "An error occurred", error = ex.Message };
            }
        }

        public async Task<dynamic> DeactiveCourseByAdmin(string courseId)
        {
            try
            {
                var course = await _context.Courses
                    .Include(c => c.CourseVersions) // Include course versions
                    .Where(c => c.CourseId == courseId)
                    .FirstOrDefaultAsync();

                if (course == null)
                {
                    return new { message = "Course not found" };
                }

                // Update the status of each course version
                foreach (var courseVersion in course.CourseVersions)
                {
                    courseVersion.Status = "Deactivate"; // Or any status value that represents deactivation
                    courseVersion.IsApproved = false;
                    courseVersion.IsUsed = false;
                }

                await _context.SaveChangesAsync();

                return new { message = "Course deactivated successfully",courseId = courseId };
            }
            catch (Exception ex)
            {
                // Log exception (ex) if necessary
                return new { success = false, message = "An error occurred", error = ex.Message };
            }
        }

        public async Task<CourseDetailForAdminDTO> GetCourseDetailForAdmin(string courseId)
        {
            try
            {
                var courseDetail = await _context.Courses
                    .Where(c => c.CourseId == courseId)
                    .Select(c => new CourseDetailForAdminDTO
                    {
                        CourseId = c.CourseId,
                        Title = c.Title,
                        CategoryName = c.Category.Name,
                        InstructorName = c.Instructor.User.FullName,
                        Description = c.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => cv.CourseVersionDetails.Description)
                            .FirstOrDefault(),
                        Price = c.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => cv.CourseVersionDetails.Price)
                            .FirstOrDefault(),
                        OldPrice = c.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => cv.CourseVersionDetails.OldPrice)
                            .FirstOrDefault(),
                        Status = c.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => cv.Status)
                            .FirstOrDefault(),
                        EarnedMoney = c.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .Select(cv => cv.CourseVersionDetails)
                            .Sum(cc => cc.Price * cc.AlreadyEnrolled),
                        Content = c.CourseVersions
                            .Where(cv => cv.IsUsed)
                            .SelectMany(cv => cv.CourseVersionDetails.CourseContents)
                            .ToList(),
                        studentComment = c.CourseVersions
                            .SelectMany(cv => cv.CourseComments)
                            .Where(comment => !comment.IsAdmin && !comment.IsDelete)
                            .Select(comment => new StudentCommentDTO
                            {
                                StudentEmail = comment.User.Email,
                                Attachment = comment.Attachment,
                                CreateDate = comment.CreateDate,
                                CourseVerison = comment.CourseVersion.Version
                            }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return courseDetail;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving the course details for admin.", ex);
            }
        }

        //    public async Task<List<CourseListFillterForAdmin>> GetCourseListFillterForAdmin(CourseListConfig config)
        //    {
        //        try
        //        {
        //            var coursesQuery = _context.Courses
        //                .Include(c => c.Instructor)
        //                .ThenInclude(i => i.User)
        //                .Include(c => c.Category)
        //                .Include(c => c.CourseVersions)
        //                .ThenInclude(cv => cv.CourseVersionDetails)
        //                .ThenInclude(cvd => cvd.CourseContents)  // Include CourseContents
        //                .Include(c => c.CourseVersions)
        //                .ThenInclude(cv => cv.CourseVersionDetails)
        //                .ThenInclude(cvd => cvd.Images) // Include Images through CourseVersionDetail
        //                .AsQueryable();

        //            // Apply filtering
        //            if (config.Status.HasValue)
        //            {
        //                var status = config.Status.Value.ToString();
        //                coursesQuery = coursesQuery.Where(c => c.CourseVersions.Any(cv => cv.Status == status));
        //            }

        //            // Apply sorting
        //            switch (config.SortBy)
        //            {
        //                case SortBy.Category:
        //                    coursesQuery = config.SortDirection == SortDirection.Ascending
        //                        ? coursesQuery.OrderBy(c => c.Category.Name)
        //                        : coursesQuery.OrderByDescending(c => c.Category.Name);
        //                    break;
        //                case SortBy.Instructor:
        //                    coursesQuery = config.SortDirection == SortDirection.Ascending
        //                        ? coursesQuery.OrderBy(c => c.Instructor.User.FullName)
        //                        : coursesQuery.OrderByDescending(c => c.Instructor.User.FullName);
        //                    break;
        //                case SortBy.Title:
        //                    coursesQuery = config.SortDirection == SortDirection.Ascending
        //                        ? coursesQuery.OrderBy(c => c.Title)
        //                        : coursesQuery.OrderByDescending(c => c.Title);
        //                    break;
        //                case SortBy.Id:
        //                    coursesQuery = config.SortDirection == SortDirection.Ascending
        //                        ? coursesQuery.OrderBy(c => c.CourseId)
        //                        : coursesQuery.OrderByDescending(c => c.CourseId);
        //                    break;
        //            }

        //            // Apply pagination
        //            coursesQuery = coursesQuery
        //.Skip(((config.PageIndex ?? 1) - 1) * (config.PageSize ?? 20))
        //.Take(config.PageSize ?? 20);

        //            // Execute the query
        //            var courses = await coursesQuery.ToListAsync();

        //            // Map to DTO
        //            var courseList = courses.Select(c => new CourseListFillterForAdmin
        //            {
        //                CourseId = c.CourseId,
        //                CourseTitle = c.Title,
        //                CourseThumb = c.CourseVersions
        //                    .Where(c => c.IsUsed)
        //                    .Select(cv => cv.CourseVersionDetails)
        //                    .SelectMany(cvd => cvd.Images)
        //                    .Where(img => img.Type == "Thumb" && !img.IsDelete)
        //                    .Select(img => img.URL)
        //                    .FirstOrDefault(),
        //                CategoryName = c.Category?.Name ?? "N/A",
        //                InstructorId = c.InstructorId,
        //                InstructorName = c.Instructor?.User?.FullName ?? "Unknown",
        //                courseVersionLists = c.CourseVersions.Select(cv => new CourseVersionList
        //                {
        //                    CourseVersionId = cv.CourseVersionId,
        //                    IsUsed = cv.IsUsed,
        //                    IsApproved = cv.IsApproved,
        //                    Status = cv.Status,
        //                    NumberOfContent = cv.CourseVersionDetails.CourseContents.Count(cc => cc.CourseVersionDetailId == cv.CourseVersionDetails.CourseVersionDetailId)
        //                }).ToList()
        //            }).ToList();

        //            return courseList;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exceptions as needed
        //            throw new Exception("An error occurred while retrieving the course list.", ex);
        //        }
        //    }

        public async Task<List<CourseListFillterForAdmin>> GetCourseListFillterForAdmin(CourseListConfig config)
        {
            try
            {
                var coursesQuery = _context.Courses
                    .Include(c => c.Instructor)
                    .ThenInclude(i => i.User)
                    .Include(c => c.Category)
                    .Include(c => c.CourseVersions)
                    .ThenInclude(cv => cv.CourseVersionDetails)
                    .ThenInclude(cvd => cvd.CourseContents)
                    .Include(c => c.CourseVersions)
                    .ThenInclude(cv => cv.CourseVersionDetails)
                    .ThenInclude(cvd => cvd.Images)
                    .AsQueryable();

                // Apply filtering
                if (config.Status.HasValue)
                {
                    var status = config.Status.Value.ToString();
                    coursesQuery = coursesQuery.Where(c => c.CourseVersions.Any(cv => cv.Status == status));
                }

                // Apply sorting
                if (string.IsNullOrEmpty(config.SortBy.ToString()))
                {
                    config.SortBy = SortBy.Category; // Default sorting by Category
                }

                switch (config.SortBy)
                {
                    case SortBy.Category:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.Category.Name)
                            : coursesQuery.OrderByDescending(c => c.Category.Name);
                        break;
                    case SortBy.Instructor:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.Instructor.User.FullName)
                            : coursesQuery.OrderByDescending(c => c.Instructor.User.FullName);
                        break;
                    case SortBy.Title:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.Title)
                            : coursesQuery.OrderByDescending(c => c.Title);
                        break;
                    case SortBy.Id:
                        coursesQuery = config.SortDirection == SortDirection.Ascending
                            ? coursesQuery.OrderBy(c => c.CourseId)
                            : coursesQuery.OrderByDescending(c => c.CourseId);
                        break;
                }

                // Apply pagination
                coursesQuery = coursesQuery
                    .Skip(((config.PageIndex ?? 1) - 1) * (config.PageSize ?? 20))
                    .Take(config.PageSize ?? 20);

                // Execute the query
                var courses = await coursesQuery.ToListAsync();

                // Group courses based on the sorting criteria and order by CourseId within each group
                var groupedCourses = new List<CourseListFillterForAdmin>();

                switch (config.SortBy)
                {
                    case SortBy.Category:
                        groupedCourses = courses
                            .GroupBy(c => c.Category.Name)
                            .Select(g => new CourseListFillterForAdmin
                            {
                                GroupKey = g.Key,
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForAdmin(c)).ToList()
                            }).ToList();
                        break;
                    case SortBy.Instructor:
                        groupedCourses = courses
                            .GroupBy(c => c.Instructor.User.FullName)
                            .Select(g => new CourseListFillterForAdmin
                            {
                                GroupKey = g.Key,
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForAdmin(c)).ToList()
                            }).ToList();
                        break;
                    case SortBy.Title:
                        groupedCourses = courses
                            .GroupBy(c => c.Title)
                            .Select(g => new CourseListFillterForAdmin
                            {
                                GroupKey = g.Key,
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForAdmin(c)).ToList()
                            }).ToList();
                        break;
                    case SortBy.Id:
                        groupedCourses = courses
                            .GroupBy(c => c.CourseId)
                            .Select(g => new CourseListFillterForAdmin
                            {
                                GroupKey = g.Key,
                                Courses = g.OrderBy(c => c.CourseId).Select(c => MapToCourseListForAdmin(c)).ToList()
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

        private CourseListForAdmin MapToCourseListForAdmin(Course c)
        {
            return new CourseListForAdmin
            {
                CourseId = c.CourseId,
                CategoryName = c.Category?.Name ?? "N/A",
                Title = c.Title,
                Description = c.CourseVersions
                //    .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.Description)
                    .FirstOrDefault(),
                CourseThumb = c.CourseVersions
         //  .Where(c => c.IsUsed)
          .Select(cv => cv.CourseVersionDetails)
          .SelectMany(cvd => cvd.Images)
           .Where(img => img.Type == "Thumb" && !img.IsDelete)
           .Select(img => img.URL)
           .FirstOrDefault(),
                InstructorName = c.Instructor?.User?.FullName ?? "Unknown",
                Price = c.CourseVersions
                    //.Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.Price)
                    .FirstOrDefault(),
                CourseVerisonId = c.CourseVersions
                 //   .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionId)
                    .FirstOrDefault(),
                CurrentVersion = c.CourseVersions
                  //  .Where(cv => cv.IsUsed)
                    .Select(cv => cv.Version)
                    .FirstOrDefault(),
                CreatedDate = c.CourseVersions
                   // .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.CreatedDate)
                    .FirstOrDefault(),
                UpdatedDate = c.CourseVersions
                   // .Where(cv => cv.IsUsed)
                    .Select(cv => cv.CourseVersionDetails.UpdatedDate)
                    .FirstOrDefault(),
                Status = c.CourseVersions
              //      .Where(cv => cv.IsUsed)
                    .Select(cv => cv.Status)
                    .FirstOrDefault(),
                IsAprroved = c.CourseVersions
                  //  .Where(cv => cv.IsUsed)
                    .Select(cv => cv.IsApproved)
                    .FirstOrDefault(),
                IsUsed = c.CourseVersions
                 //   .Where(cv => cv.IsUsed)
                    .Select(cv => cv.IsUsed)
                    .FirstOrDefault()
            };
        }


    }
}
