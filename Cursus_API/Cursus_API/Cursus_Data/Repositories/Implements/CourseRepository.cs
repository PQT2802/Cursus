using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.CommonObject;
using Cursus_Data.Models.DTOs.Course;
using Cursus_Data.Models.DTOs.UI;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
using Cursus_Data.Models.DTOs.Virtual;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Cursus_Data.Repositories.Implements
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LMS_CursusDbContext _context;

        public CourseRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task AddCourseVersionAsync(CourseVersion courseVersion)
        {
            await _context.CourseVersions.AddAsync(courseVersion);
            await _context.SaveChangesAsync();
        }

        public async Task AddCourseVersionDetailAsync(CourseVersionDetail courseVersionDetail)
        {
            await _context.CourseVersionDetails.AddAsync(courseVersionDetail);
            await _context.SaveChangesAsync();
        }
        public async Task<string> AddCourseVersionDetailReturnIdAsync(CourseVersionDetail courseVersionDetail)
        {
            await _context.CourseVersionDetails.AddAsync(courseVersionDetail);
            await _context.SaveChangesAsync();
            return courseVersionDetail.CourseVersionDetailId;
        }

        public async Task<string> AutoGenerateCourseID()
        {
            int count = await _context.Courses.CountAsync() + 1;
            string Us = "CS";
            string paddedNumber = count.ToString().PadLeft(4, '0');
            string CourseId = Us + paddedNumber;
            return CourseId;
        }

        public async Task<string> AutoGenerateCourseVersionDetailID()
        {
            int count = await _context.CourseVersionDetails.CountAsync() + 1;
            string Us = "CVD";
            string paddedNumber = count.ToString().PadLeft(4, '0');
            string CVDId = Us + paddedNumber;
            return CVDId;
        }

        public async Task<bool> CheckCategoryId(string categoryId)
        {
            return await _context.Categories.AnyAsync(x => x.CategoryId == categoryId);
        }

        public async Task<Course?> FindCourseByid(string id)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task<List<SubmittedCourseDTO>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses
                .Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails).Include(x => x.Category)
                .Select(c => new SubmittedCourseDTO
                {
                    CourseId = c.CourseId,
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category.Name,
                    Description = c.Category.Description,
                    Title = c.Title,
                    Price = c.CourseVersions.Select(cv => cv.CourseVersionDetails.Price).FirstOrDefault(),
                    InstructorId = c.InstructorId,
                })
                .ToListAsync();

            return courses;
        }

        public async Task UpdateCourseIsAcceptedAsync(CourseVersion course)
        {
            _context.CourseVersions.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckExistCourse(string courseId)
        {
            return await _context.Courses.AnyAsync(x => x.CourseId == courseId);
        }

        public async Task<CourseDetailDTO> GetCourseDetailById(string courseId, string instructorId)
        {
            var courseDetailDTO = await _context.Courses.Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails).Include(x => x.Category)
                .Where(x => x.CourseId == courseId && x.InstructorId == instructorId).Select(x => new CourseDetailDTO
                {
                    CourseId = x.CourseId,
                    Name = x.Title,
                    Category = x.Category.Name,
                    Description = x.CourseVersions.Select(cv => cv.CourseVersionDetails.Description).FirstOrDefault(),
                    //Attachments
                    Price = x.CourseVersions.Select(cv => cv.CourseVersionDetails.Price).FirstOrDefault(),
                    //OldPrice
                    Status = x.CourseVersions.Select(cv => cv.Status).FirstOrDefault(),
                }).FirstOrDefaultAsync(); ;
            return courseDetailDTO;
        }

        public async Task<List<CourseDetailDTO>> GetCoursesDetail(GetListDTO getListDTO, string instructorId)
        {
            var courseDetailDTO = _context.Courses.Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails).Include(x => x.Category)
                .Where(x => x.InstructorId == instructorId).Select(x => new CourseDetailDTO

                {
                    CourseId = x.CourseId,
                    Name = x.Title,
                    Category = x.Category.Name,
                    Description = x.CourseVersions.Select(cv => cv.CourseVersionDetails.Description).FirstOrDefault(),
                    //Attachments
                    Price = x.CourseVersions.Select(cv => cv.CourseVersionDetails.Price).FirstOrDefault(),
                    //OldPrice
                    Status = x.CourseVersions.Select(cv => cv.Status).FirstOrDefault(),
                }).Skip((getListDTO.pageIndex - 1) * getListDTO.pageSize).Take(getListDTO.pageSize); ;
            if (!string.IsNullOrEmpty(getListDTO.search) && !string.IsNullOrEmpty(getListDTO.searchBy))
            {
                courseDetailDTO = getListDTO.searchBy switch
                {
                    "Name" => courseDetailDTO.Where(i => i.Name.Contains(getListDTO.search)),
                    "Description" => courseDetailDTO.Where(i => i.Description.Contains(getListDTO.search)),
                    "Status" => courseDetailDTO.Where(i => i.Status.ToString().Contains(getListDTO.search)),
                    _ => courseDetailDTO
                };
            }

            if (!string.IsNullOrEmpty(getListDTO.sortBy))
            {
                var sortProperty = getListDTO.sortBy.ToLower() switch
                {
                    "name" => nameof(CourseDetailDTO.Name),
                    "description" => nameof(CourseDetailDTO.Description),
                    "price" => nameof(CourseDetailDTO.Price),
                    "status" => nameof(CourseDetailDTO.Status),
                    _ => nameof(CourseDetailDTO.Name)
                };
                var sortedCourses = getListDTO.sort.ToUpper() == "DESC"
            ? courseDetailDTO.OrderByDescending(x => EF.Property<object>(x, sortProperty))
            : courseDetailDTO.OrderBy(x => EF.Property<object>(x, sortProperty));

                courseDetailDTO = sortedCourses;
            }
            return await courseDetailDTO.ToListAsync();
        }

        public async Task<int> GetFirstCourseVersionIdByCourseId(string courseId)
        {
            var courseVersion = await _context.CourseVersions.FirstOrDefaultAsync(x => x.CourseId == courseId);
            return courseVersion.CourseVersionId;
        }

        public async Task<List<CourseListDTO>> GetCourseList(GetListDTO getListDTO)
        {
            var courseList = _context.Courses.Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails).Include(x => x.Category).Select(x => new CourseListDTO
            {
                CourseId = x.CourseId,
                CourseName = x.Title,
                Category = x.Category.Name,
                Instructor = x.InstructorId,
                NumberOfStudent = x.CourseVersions.Select(x => x.CourseVersionDetails.AlreadyEnrolled).FirstOrDefault(),
                TotalPurchasedMoney = x.CourseVersions.Select(x => x.CourseVersionDetails.AlreadyEnrolled * x.CourseVersionDetails.Price).FirstOrDefault(),
                VersionOfCourse = x.CourseVersions.Count(),
                Rating = x.CourseVersions.Select(x => x.CourseVersionDetails.Rating).FirstOrDefault(),
                Status = x.CourseVersions.Select(x => x.Status).First()
            }).Skip((getListDTO.pageIndex - 1) * getListDTO.pageSize).Take(getListDTO.pageSize); ;
            if (!string.IsNullOrEmpty(getListDTO.search) && !string.IsNullOrEmpty(getListDTO.searchBy))
            {
                courseList = getListDTO.searchBy switch
                {
                    "Name" => courseList.Where(i => i.CourseName.Contains(getListDTO.search)),
                    "Category" => courseList.Where(i => i.Category.Contains(getListDTO.search)),
                    "Instructor" => courseList.Where(i => i.Instructor.ToString().Contains(getListDTO.search)),
                    _ => courseList
                };
            }

            if (!string.IsNullOrEmpty(getListDTO.sortBy))
            {
                var sortProperty = getListDTO.sortBy.ToLower() switch
                {
                    "Name" => nameof(CourseListDTO.CourseName),
                    "Category" => nameof(CourseListDTO.Category),
                    "NumberOfStudent" => nameof(CourseListDTO.NumberOfStudent),
                    "Instructor" => nameof(CourseListDTO.Instructor),
                    "TotalPurchasedMoney" => nameof(CourseListDTO.TotalPurchasedMoney),
                    "Rating" => nameof(CourseListDTO.Rating),
                    _ => nameof(CourseListDTO.CourseName)
                };
                var sortedCourses = getListDTO.sort.ToUpper() == "DESC"
            ? courseList.OrderByDescending(x => EF.Property<object>(x, sortProperty))
            : courseList.OrderBy(x => EF.Property<object>(x, sortProperty));

                courseList = sortedCourses;
            }
            return await courseList.ToListAsync();
        }

        public async Task<List<UICourseListDTO>> GetCourseListForUI(string instructorId)
        {
            var courseList = _context.Courses.Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails).Include(x => x.Category).Where(x => x.InstructorId == instructorId).Select(x => new UICourseListDTO
            {
                CourseName = x.Title,
                Category = x.Category.Name,
                ImageCourse = "image",
                NumberOfStudent = x.CourseVersions.Select(x => x.CourseVersionDetails.AlreadyEnrolled).FirstOrDefault(),
                Price = x.CourseVersions.Select(x => x.CourseVersionDetails.AlreadyEnrolled * x.CourseVersionDetails.Price).FirstOrDefault(),
                Status = "STATUS"
            });
            return await courseList.ToListAsync();

        }

        public async Task<List<CourseQueueListDTO>> GetCourseQueueList()
        {
            string status = "Pending";
            var courseList = _context.Courses.Include(x => x.CourseVersions).Include(x => x.Category).Where(x => x.CourseVersions.Any(x => x.Status == status)).Select(x => new CourseQueueListDTO
            {
                CourseId = x.CourseId,
                Title = x.Title,
                InstructorId = x.InstructorId,
                Version = x.CourseVersions.Select(x => x.Version).FirstOrDefault(),
                Status = x.CourseVersions.Select(x => x.Status).FirstOrDefault()
            });
            return await courseList.ToListAsync();
        }


        public async Task AddCourseVersion(CourseVersion courseVersion)
        {
            await _context.CourseVersions.AddAsync(courseVersion);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<CourseVersion?> FindCourseVersionDescending(string courseid)
        {
            return await _context.CourseVersions
                .OrderByDescending(cv => cv.Version)
                .FirstOrDefaultAsync(cv => cv.CourseId == courseid && cv.Status=="New");
        }

        public async Task<CourseVersionDetail> GetCourseVersionDetail(int courseversionid)
        {
            return await _context.CourseVersionDetails
            .FirstOrDefaultAsync(cvd => cvd.CourseVersionId == courseversionid);

        }

        public async Task<CourseVersionDetail> UpdateCourseVersionDetailAsync(CourseVersionDetail courseVersionDetail)
        {
            _context.CourseVersionDetails.Update(courseVersionDetail);
            await _context.SaveChangesAsync();
            return courseVersionDetail;
        }
        public async Task<List<UICourseListAdminDTO>> GetCourseListForAdminUI()
        {
            var courseList = _context.Courses.Include(x => x.CourseVersions).Include(x => x.Category).Select(x => new UICourseListAdminDTO
            {
                CourseId = x.CourseId,
                CourseName = x.Title,
                Category = x.Category.Name,
                Instructor = x.InstructorId,
                IsApproved = x.CourseVersions.Select(x => x.IsApproved).FirstOrDefault(),
                //Status = x.CourseVersions.Select(x => x.Status).FirstOrDefault(),
                CurrentVersion = x.CourseVersions.Where(x => x.Status == "Activate").Select(x => x.Version).FirstOrDefault()
            }); ;
            return await courseList.ToListAsync();
        }

        public async Task<UICourseListDetailAdminDTO> GetCourseDetailForAdminUI(int courseVersionId)
        {
            var courseDetail = await _context.CourseVersions.Include(x => x.CourseVersionDetails)
                .Where(x => x.CourseVersionId == courseVersionId).Select(x => new UICourseListDetailAdminDTO
                {
                    NumberOfStudent = x.CourseVersionDetails.AlreadyEnrolled,
                    TotalPurchasedMoney = x.CourseVersionDetails.AlreadyEnrolled * x.CourseVersionDetails.Price,
                    Price = x.CourseVersionDetails.Price,
                    Rating = x.CourseVersionDetails.Rating,
                }).FirstOrDefaultAsync();
            return courseDetail;
        }

        public async Task<bool> CheckTitleExists(string title)
        {
            return await _context.Courses.AnyAsync(x => x.Title == title);
        }

        public async Task<CourseDetailDTO> GetCourseDetailById(string courseId)
        {
            throw new NotImplementedException();
            //return await _context.CourseVersionDetails.FirstOrDefaultAsync();
        }

        public async Task<CourseDTO> GetCourseById(string courseId)
        {
            var course = await _context.Courses.Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails).Include(x => x.Category)
                .Where(x => x.CourseId == courseId).Select(x => new CourseDTO
                {
                    CategoryId = x.Category.Name,
                    Title = x.Title,
                    Description = x.CourseVersions.Select(x => x.CourseVersionDetails.Description).FirstOrDefault(),
                    Price = x.CourseVersions.Select(x => x.CourseVersionDetails.Price).FirstOrDefault()
                }).FirstOrDefaultAsync();
            return course;
        }
        /*
          CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
         */

        public Task<List<CourseDetailDTO>> GetCoursesDetail(GetListDTO getListDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckCourseVersionDetailId(string courseVersionDetailId)
        {
            return await _context.CourseVersionDetails.AnyAsync(x => x.CourseVersionDetailId == courseVersionDetailId);
        }

        public async Task<string> AutoGenerateCourseContentID()
        {
            int count = await _context.CourseContents.CountAsync() + 1;
            string Us = "CC";
            string paddedNumber = count.ToString().PadLeft(4, '0');
            string CourseContentId = Us + paddedNumber;
            return CourseContentId;
        }

        public async Task<bool> CheckContentTitle(string title)
        {
            return await _context.CourseContents.AnyAsync(x => x.Title == title);
        }

        public async Task<bool> CheckContentUrlExist(string url)
        {
            return await _context.CourseContents.AnyAsync(x => x.Url == url);
        }

        public async Task<CourseVersion> GetCourseVersionById(string courseId)
        {
            var courseVersion = await _context.CourseVersions.FirstOrDefaultAsync(x => x.CourseId == courseId);
            return courseVersion;

        }

        public async Task UpdateCourseLearningTimeAsync(string courseVersionDetailId)
        {
            // Retrieve all course contents with the same CourseVersionDetailId
            var courseContents = await _context.CourseContents
                .Where(cc => cc.CourseVersionDetailId == courseVersionDetailId && !cc.IsDelete)
                .ToListAsync();

            if (!courseContents.Any())
            {
                throw new Exception("No course contents found for the provided CourseVersionDetailId.");
            }

            // Get the total learning time
            double totalLearningTimeInHours = courseContents.Sum(cc => cc.Time);
            string formattedLearningTime = "";

            if (totalLearningTimeInHours <= 1)
            {
                formattedLearningTime = $"{totalLearningTimeInHours:F2} hour"; // Adding F2 for formatting to 2 decimal places
            }
            else
            {
                formattedLearningTime = $"{totalLearningTimeInHours:F2} hours";  // Adding F2 for formatting to 2 decimal places
            }

            // Update the CourseLearningTime in the CourseVersionDetail table
            var courseVersionDetail = await _context.CourseVersionDetails
                .FirstOrDefaultAsync(cvd => cvd.CourseVersionDetailId == courseVersionDetailId);

            if (courseVersionDetail == null)
            {
                throw new Exception("CourseVersionDetail not found.");
            }

            courseVersionDetail.CourseLearningTime = formattedLearningTime;

            // Save the changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetCourseTitleAtCourseTable(string courseId)
        {
            var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            return course.Title;
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            SaveChangeCourseAsync();
        }

        public async Task SaveChangeCourseAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UICourseVerisonList>> GetUICourseVerisonListByCourseId(string courseId)
        {
            var cvl = await _context.Courses
    .Where(x => x.CourseId.Equals(courseId))
    .Include(x => x.CourseVersions)
        .ThenInclude(cv => cv.CourseVersionDetails)
    .SelectMany(course => course.CourseVersions, (course, version) => new UICourseVerisonList
    {
        CourseId = course.CourseId,
        CourseVersionId = version.CourseVersionId.ToString(),
        Version = version.Version,
        AlreadyEnrolled = version.CourseVersionDetails.AlreadyEnrolled,
        CreatedDate = version.CourseVersionDetails.CreatedDate,
        Status = version.Status
    })
    .ToListAsync();

            return cvl;
        }

        public async Task<UICoruse> GetUICoruseById(string courseId)
        {
            var course = await _context.Courses
            .Include(c => c.Category)
            .Include(c => c.CourseVersions)
                .ThenInclude(cv => cv.CourseVersionDetails)
            .Include(c => c.Instructor)
                .ThenInclude(i => i.User)
            .Where(c => c.CourseId == courseId)
            .FirstOrDefaultAsync();

            if (course == null)
            {
                return null;
            }


            return new UICoruse
            {
                CourseId = course.CourseId,
                Title = course.Title,
                InstructorName = course.Instructor?.User?.FullName,
                InstructorEmail = course.Instructor?.User?.Email,
                InstructorId = course.Instructor?.User?.UserID,
            };
        }





        public async Task<List<CourseForGuestDTO>> GetCourseForGuest()
        {
            var courses = await _context.Courses
            .Include(x => x.CourseVersions)
            .ThenInclude(x => x.CourseVersionDetails)
            .Include(x => x.Category)
            .Select(x => new CourseForGuestDTO
            {
                CourseId = x.CourseId,
                CourseName = x.Title,
                ImageCourse = "image",
                Price = x.CourseVersions.Select(x => x.CourseVersionDetails.AlreadyEnrolled * x.CourseVersionDetails.Price).FirstOrDefault(),
                Rating = x.CourseRating ?? 0,
                Category = x.Category.Name,
                Instructor = x.InstructorId,
            }).OrderByDescending(x => x.Rating).ToListAsync();

            return courses;


        }


        public async Task<List<CourseForGuestDTO>> SearchCourses(SearchDTO searchDTO)
        {
            var query = _context.Courses
                   .Include(x => x.CourseVersions)
                   .ThenInclude(x => x.CourseVersionDetails)
                   .Include(x => x.Category)
                   .Include(x => x.Instructor)
                   .ThenInclude(x => x.User)
                   .AsQueryable();

            if (!string.IsNullOrEmpty(searchDTO.filter) && searchDTO.filter.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(x => x.Instructor.User.FullName.Contains(searchDTO.search) && x.Instructor.User.RoleID == 2);
            }
            else
            {
                query = query.Where(x => x.Category.Name.Contains(searchDTO.search));
            }
            var courses = await query
                    .Select(x => new CourseForGuestDTO
                    {
                        CourseName = x.Title,
                        ImageCourse = "image",
                        Price = x.CourseVersions.Select(cv => cv.CourseVersionDetails.AlreadyEnrolled * cv.CourseVersionDetails.Price).FirstOrDefault(),
                        Rating = x.CourseRating ?? 0,
                        Category = x.Category.Name,
                        Instructor = x.InstructorId,
                    }).OrderByDescending(x => x.Rating).ToListAsync();

            return courses;
        }



        #region #region 8 Homepage View - LDQ
        public async Task<List<CourseDTO>> GetCourseByUserBehavior(List<string> categoriesBehavior)
        {
            List<CourseDTO> totalList = new List<CourseDTO>();
            int items = 5;
            foreach (var category in categoriesBehavior)
            {
                var list = await GetCourseListByCategory(category, items);
                totalList.AddRange(list);
                items--;
            }
            return totalList;
        }

        private async Task<List<CourseDTO>> GetCourseListByCategory(string categoryId, int item)
        {
            var courseList = _context.Courses.Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails)
                .Where(x => x.CategoryId == categoryId && x.CourseVersions.Any(c => c.IsUsed)).Select(x => new CourseDTO
                {
                    CategoryId = categoryId,
                    Title = x.Title,
                    Description = x.CourseVersions.Select(x => x.CourseVersionDetails.Description).FirstOrDefault(),
                    Price = x.CourseVersions.Select(x => x.CourseVersionDetails.Price).FirstOrDefault()
                }).Take(item);
            return await courseList.ToListAsync();
        }


        public async Task<List<string>> GetCourseByTrend()
        {
            var courseList = await _context.Courses.Include(x => x.CourseVersions).ThenInclude(x => x.CourseVersionDetails).Include(x => x.Category)
                .Where(x => x.CourseVersions.Any(c => c.IsUsed)).Select(x => new
                {
                    Category = x.Category.Name,
                    Enrolled = x.CourseVersions.Select(cv => cv.CourseVersionDetails.AlreadyEnrolled).FirstOrDefault()
                }).OrderByDescending(x => x.Enrolled).ToListAsync();
            var trendList = courseList.GroupBy(x => x.Category).OrderByDescending(x => x.Sum(x => x.Enrolled)).Select(x => x.Key).Take(3).ToList();
            return trendList;
        }

        public async Task<int> GetNumberOfCourseGroupByCategory(string categoryName)
        {
            var num = _context.Courses.Where(x => x.Category.Name == categoryName).GroupBy(x => x.CategoryId).Select(x => new
            {
                CategoryId = x.Key,
                NumOfCsr = x.Count(),
            });
            return num.First().NumOfCsr;
        }

        public async Task<List<CourseDTO>> GetCourseByGoodFeedbacks()
        {
            List<FBCourseDTO> list = await GetGoodFeedbacksCourse();
            List<CourseDTO> courseList = new List<CourseDTO>();
            foreach (var fbcourse in list) {
                CourseDTO course = new CourseDTO()
                {
                    CategoryId = fbcourse.Catname,
                    Description = fbcourse.Description,
                    Price = fbcourse.Price,
                    Title = fbcourse.Title
                };
                courseList.Add(course);
            }
            return courseList;
        }

        private async Task<List<FBCourseDTO>> GetGoodFeedbacksCourse()
        {
            var courseList = await _context.Courses
                .Include(x => x.CourseVersions)
                .ThenInclude(x => x.CourseVersionDetails)
                .Include(x => x.Category)
                .Where(x => x.CourseVersions.Any(c => c.IsUsed))
                .Select(x => new FBCourseDTO
                {
                    Catname = x.Category.Name,
                    Title = x.Title,
                    Description = x.CourseVersions.Select(x => x.CourseVersionDetails.Description).FirstOrDefault(),
                    Price = x.CourseVersions.Select(x => x.CourseVersionDetails.Price).FirstOrDefault(),
                    Rating = x.CourseVersions.Select(cv => cv.CourseVersionDetails.Rating).FirstOrDefault()
                }).OrderByDescending(x => x.Rating).Take(10).ToListAsync();
            return courseList;
        }
        #endregion

        

        public async Task<Course> GetCourseByIdV2(string courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == courseId);
        }
        public async Task<dynamic> UpdateCourseByCourseId(UpdateCourseDTO courseDTO)
        {
            try
            {
                double oldPrice = 0;
                var course = _context.Courses
                    .Include(cv => cv.CourseVersions)
                    .ThenInclude(cvd => cvd.CourseVersionDetails)
                    .ThenInclude(im  => im.Images)
                    .FirstOrDefault(c => c.CourseId == courseDTO.CourseId && c.CourseVersions.FirstOrDefault().IsUsed);

                if (course != null)
                {
                    course.Title = courseDTO.Title;
                    course.CategoryId = courseDTO.CategoryId;
                    var courseVersionDetail = course.CourseVersions.FirstOrDefault()?.CourseVersionDetails;
                    if (courseVersionDetail != null)
                    {
                        courseVersionDetail.Description = courseDTO.Description;
                        if (courseDTO.Price.HasValue)
                        {
                            courseVersionDetail.Price = courseDTO.Price.Value;
                        }
                        courseVersionDetail.OldPrice = oldPrice;
                        courseVersionDetail.UpdatedDate = DateTime.Now;
                        var image = courseVersionDetail.Images.FirstOrDefault(im => im.Type.Equals("Thumb") && im.ImageId.Equals(courseVersionDetail.CourseVersionDetailId));
                        if (image != null) 
                        {
                            image.URL = courseDTO.ThumbUrl ;
                        }

                    }
                }

                  _context.Courses.Update(course);
                return await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<List<string>> GetCourseIdByInstructorId(string instructorId)
        {
            return await _context.Courses.Where(x => x.InstructorId == instructorId).Select(x => x.CourseId).ToListAsync();
        }

        public async Task<Course> GetCourseIsUsed(string courseId)
        {
            var result = await _context.Courses
                .Include(c => c.CourseVersions)
                .Where(c => c.CourseVersions.Any(cv => cv.CourseId == courseId && cv.IsUsed))
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<ReSubmitCourse> ReSubmitCourse(ReSubmitCourse reSubmitCourse)
        {
            var course = await _context.Courses
                .Include(c => c.CourseVersions)
                .ThenInclude(cv => cv.CourseVersionDetails)
                .FirstOrDefaultAsync(c => c.CourseId == reSubmitCourse.CourseId);

            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            CourseVersion courseVersion;

            if (reSubmitCourse.CourseVersionId == null)
            {
                if (course.CourseVersions.Count == 1)
                {
                    courseVersion = course.CourseVersions.First();
                }
                else
                {
                    throw new Exception("Multiple course versions found. Please specify a CourseVersionId.");
                }
            }
            else
            {
                courseVersion = course.CourseVersions
                    .FirstOrDefault(cv => cv.CourseVersionId == reSubmitCourse.CourseVersionId);

                if (courseVersion == null)
                {
                    throw new Exception("Specified CourseVersionId not found.");
                }

                courseVersion.Status = Status.Submitted.ToString();
                _context.CourseVersions.Update(courseVersion);
                await _context.SaveChangesAsync(); // Ensure SaveChangesAsync is awaited
            }

            // Proceed with the logic for resubmitting the course with the identified courseVersion
            // Example: Update some properties or handle the resubmission process

            // Return the ReSubmitCourse object or any relevant information
            return reSubmitCourse;
        }

        public Task SubmitCourse(string courseId, int courseVersionId)
        {
            throw new NotImplementedException();
        }
    }
}


