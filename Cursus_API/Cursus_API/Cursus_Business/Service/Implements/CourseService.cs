using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Context;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Cursus_Data.Models.DTOs.UI;
using Cursus_Data.Models.DTOs.UI.UIAdmin;
using DocumentFormat.OpenXml.Wordprocessing;
using Cursus_Data.Models.DTOs.Course;


namespace Cursus_Business.Service.Implements
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IUserBehaviorRepository _userBehaviorRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICourseVersionRepository _courseVersionRepository;
        private readonly LMS_CursusDbContext _cursusDbContext;
        private readonly IFirebaseService _firebaseService;
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly ICourseVersionDetailRepository _courseVersionDetailRepository;
        private readonly IMailServiceV3 _mailServiceV3;
        private readonly IImageRepository _imageRepository;


        public CourseService(ICourseRepository courseRepository, IInstructorRepository instructorRepository,
            ICategoryRepository categoryRepository, IFirebaseService firebaseService, IUserBehaviorRepository userBehaviorRepository, IMailServiceV3 mailServiceV3
            ,ICourseContentRepository courseContentRepository, ICourseVersionRepository courseVersionRepository
            , ICourseVersionDetailRepository courseVersionDetailRepository, IImageRepository imageRepository)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _userBehaviorRepository = userBehaviorRepository;
            _categoryRepository = categoryRepository;
            _courseVersionRepository = courseVersionRepository;
            _firebaseService = firebaseService;
            _courseContentRepository = courseContentRepository;
            _courseVersionDetailRepository = courseVersionDetailRepository;
            _mailServiceV3 = mailServiceV3;
            _imageRepository = imageRepository;
        }

        public async Task<dynamic> CreateCourse(CourseDTO courseDTO, CurrentUserObject c)
        {
            try
            {
                if (courseDTO.CategoryId == null)
                {
                    return Result.Failure(CourseError.CategoryIdNull());
                }
                if (!await _courseRepository.CheckCategoryId(courseDTO.CategoryId))
                {
                    return Result.Failure(CourseError.CategoryIdNotFound(courseDTO.CategoryId));
                }
                if (await _courseRepository.CheckTitleExists(courseDTO.Title))
                {
                    return Result.Failure(CourseError.TitleAlreadyExist(courseDTO.Title));
                }
                if (courseDTO.Title == null)
                {
                    return Result.Failure(CourseError.TitleNull());
                }
                if(courseDTO.Price == null)
                {
                    return Result.Failure(CourseError.PriceNull());
                }
                var courseId = await _courseRepository.AutoGenerateCourseID();
                var cvdId = await _courseRepository.AutoGenerateCourseVersionDetailID();
                var instructorId = await _instructorRepository.GetInstructorIdByUserId(c.UserId);
                var filePath = await _firebaseService.UploadImage(courseDTO.Thumb, Common.FireBaseFolder.CourseThumb);
                Course course = new Course()
                {
                    CourseId = courseId,
                    CategoryId = courseDTO.CategoryId,
                    Title = courseDTO.Title,
                    InstructorId = instructorId,
                    CourseRating = 0,

                };
                await _courseRepository.AddCourseAsync(course);
                CourseVersion courseVersion = new CourseVersion()
                {
                    CourseId = courseId,
                    Status = "Pending",
                    Version = 1,
                    IsApproved = false,
                    IsUsed = false,
                };
                await _courseRepository.AddCourseVersionAsync(courseVersion);
                var courseVersionId = await _courseRepository.GetFirstCourseVersionIdByCourseId(courseId);
                var userId = c.UserId;
                CourseVersionDetail courseVersionDetail = new CourseVersionDetail()
                {
                    CourseVersionDetailId = cvdId,
                    CourseVersionId = courseVersionId,
                    Description = courseDTO.Description,
                    Price = courseDTO.Price,
                    OldPrice = 0,
                    CreatedDate = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = userId,
                    Rating = 0,
                    AlreadyEnrolled = 0,
                    CourseLearningTime = "0 hour",
                    IsDelete = false,
                };
                Image thumb = new Image()
                {
                    ImageId = Guid.NewGuid().ToString(),
                    CourseVersionDetailId = cvdId,
                    URL = filePath,
                    IsDelete = false,
                    Type = "Thumb",

                };

                // await _courseRepository.AddCourseVersionDetailAsync(courseVersionDetail);
                await _imageRepository.AddImage(thumb);
                var courseVersionIdResponse = await _courseRepository.AddCourseVersionDetailReturnIdAsync(courseVersionDetail);
                return Result.SuccessWithObject(new { courseVersionDetailId = courseVersionIdResponse });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<dynamic> CreateCourseContents(CourseContentDTO courseContentDTO, string userId)
        {

                if (courseContentDTO.CourseVersionDetailId == null)
                {
                    return Result.Failure(CourseContentError.CrsVerDetailIdNull());
                }
                if (!await _courseRepository.CheckCourseVersionDetailId(courseContentDTO.CourseVersionDetailId))
                {
                    return Result.Failure(CourseContentError.CrsVerDetailIdNotExist());
                }
                if (courseContentDTO.Title == null)
                {
                    return Result.Failure(CourseContentError.TitleNull());
                }
                if (await _courseRepository.CheckContentTitle(courseContentDTO.Title))
                {
                    return Result.Failure(CourseContentError.TitleExist());
                }
                if (courseContentDTO.Time == null)
                {
                    return Result.Failure(CourseContentError.TimeNull());
                }
                if (courseContentDTO.Type == null)
                {
                    return Result.Failure(CourseContentError.TypeNull());
                }
            //int cvid = await _courseVersionDetailRepository.GetCourseVersionDetailById(courseContentDTO.CourseVersionDetailId);
            //courseversion cv = await _courseVersionRepository.GetCourseVersionById(cvid);
            //if (cv.status == Status.activate || cv.status == Status.Submitted)
            //{
            //    return result.failure(new error("course", "course is activate or in queue list,can not add more content"));
            //}

            var filePath = await _firebaseService.UploadImage(courseContentDTO.File, FireBaseFolder.CourseContentFile);

                var ccId = await _courseRepository.AutoGenerateCourseContentID();
                CourseContent courseContent = new CourseContent()
                {
                    CourseContentId = ccId,
                    CourseVersionDetailId = courseContentDTO.CourseVersionDetailId,
                    Title = courseContentDTO.Title,
                    Url = filePath,
                    Time = courseContentDTO.Time,
                    Type = courseContentDTO.Type.ToString(),
                    CreatedDate = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = userId,
                    IsDelete = false,
                };
            if (courseContent == null)
            {
                return Result.Failure(new Error("CourseContent", "Course content is null."));
            }

            await _courseContentRepository.AddCourseContentAsync(courseContent);
            
            return Result.Success();
        }

        public async Task<Result> ApproveCourse(string courseid)
        {
            CourseVersion courseVersion = await _courseRepository.GetCourseVersionById(courseid);
            if (courseVersion == null)
            {
                return Result.Failure(Result.CreateError("Course", "Course not found"));
            }
            courseVersion.IsApproved = true;
            courseVersion.Status = Common.Status.Activate;

            await _courseRepository.UpdateCourseIsAcceptedAsync(courseVersion);


            var result = await _mailServiceV3.SendApproveCourseMail(courseid);

            //send email
            return Result.SuccessWithObject(result);
        }
        public async Task<List<SubmittedCourseDTO>> GetAllCourse()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }
        public async Task<dynamic> RejectCourse(RejectCourseDTO rejectCourseDTO)
        {
            CourseVersion courseVersion = await _courseRepository.GetCourseVersionById(rejectCourseDTO.courseId);
            if (courseVersion == null)
            {
                return Result.Failure(Result.CreateError("Course", "Course not found"));
            }
            courseVersion.IsApproved = false;
            courseVersion.Status = Common.Status.Deactivate;
            await _courseRepository.UpdateCourseIsAcceptedAsync(courseVersion);

            var result = await _mailServiceV3.SendRejectCourseMail(rejectCourseDTO.courseId,rejectCourseDTO.Reason);
            //send email

            return Result.Success();
        }
        public async Task<dynamic> GetCourseDetailById(string courseId, string instructorId)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                return Result.Failure(Result.CreateError("Null", "Course id must not be null"));
            }
            if (!await _courseRepository.CheckExistCourse(courseId))
            {
                return Result.Failure(Result.CreateError("Null", "Course id not exist"));
            }
            try
            {
                CourseDetailDTO courseDetail = await _courseRepository.GetCourseDetailById(courseId, instructorId);
                if (courseDetail != null)
                {
                    return Result.SuccessWithObject(courseDetail);
                }
                return Result.Failure(Result.CreateError("Null", "Cannot find course"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> GetCoursesDetail(GetListDTO getListDTO, string instructorId)
        {
            try
            {
                List<CourseDetailDTO> courseDetail = await _courseRepository.GetCoursesDetail(getListDTO, instructorId);
                if (courseDetail.Count > 0)
                {
                    return Result.SuccessWithObject(courseDetail);
                }
                return Result.Failure(Result.CreateError("Null", "You don't have any courses"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<Result> GetCoursesList(GetListDTO getListDTO)
        {
            try
            {
                List<CourseListDTO> coursesList = await _courseRepository.GetCourseList(getListDTO);
                if (coursesList.Count > 0)
                {
                    return Result.SuccessWithObject(coursesList);
                }
                return Result.Failure(Result.CreateError("Null", "Null courses"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> GetCourseListForUI(string instructorId)
        {
            try
            {
                List<UICourseListDTO> coursesList = await _courseRepository.GetCourseListForUI(instructorId);
                if (coursesList.Count > 0)
                {
                    return Result.SuccessWithObject(coursesList);
                }
                return Result.Failure(Result.CreateError("Null", "Null courses"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> GetCourseQueueList()
        {
            try
            {
                List<CourseQueueListDTO> coursesList = await _courseRepository.GetCourseQueueList();
                if (coursesList.Count > 0)
                {
                    return Result.SuccessWithObject(coursesList);
                }
                return Result.Failure(Result.CreateError("Null", "Null queue"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> AddCourseVersion(string courseid)
        {
            var course = await _courseRepository.FindCourseByid(courseid);
            if (course != null)
            {
                CourseVersion latestCourseVersion = await _courseRepository.FindCourseVersionDescending(courseid);
                decimal newVersionNumber = latestCourseVersion.Version + 0.01m;

                var newCourseVersion = new CourseVersion
                {
                    CourseId = courseid,
                    Status = "InProcess",
                    Version = newVersionNumber,
                };
                await _courseRepository.AddCourseVersion(newCourseVersion);

                // Save changes to the database
                CourseVersionDetail courseVersionDetail = await _courseVersionDetailRepository.GetCourseVersionDetailById(courseid);
                CourseVersionDetail newCvd = new CourseVersionDetail
                {
                    CourseVersionDetailId = await _courseRepository.AutoGenerateCourseVersionDetailID(),
                    CourseVersionId = newCourseVersion.CourseVersionId,
                    Description = courseVersionDetail.Description,
                    Price = courseVersionDetail.Price,
                    OldPrice = courseVersionDetail.OldPrice,
                    CreatedDate = courseVersionDetail.CreatedDate,
                    CreatedBy = courseVersionDetail.CreatedBy,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = courseVersionDetail.UpdatedBy, 
                    Rating = 0,
                    AlreadyEnrolled = 0,
                    CourseLearningTime = courseVersionDetail.CourseLearningTime,
                    IsDelete = false,
                };
                await _courseRepository.AddCourseVersionDetailAsync(newCvd);
                return new { Success = true, CourseVersion = newCourseVersion };
            }
            else
            {
                return new { Success = false, Message = "Course not found" };
            }

        }

        public async Task<dynamic> GetCourseListForAdminUI()
        {
            try
            {
                List<UICourseListAdminDTO> coursesList = await _courseRepository.GetCourseListForAdminUI();
                if (coursesList.Count > 0)
                {
                    return Result.SuccessWithObject(coursesList);
                }
                return Result.Failure(Result.CreateError("Null", "Null courses"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> GetCourseDetailForAdminUI(int courseVersionId)
        {
            try
            {
                UICourseListDetailAdminDTO courseDetail = await _courseRepository.GetCourseDetailForAdminUI(courseVersionId);
                return Result.SuccessWithObject(courseDetail);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> GetCourseById(string courseId)
        {
            if (string.IsNullOrEmpty(courseId))
                return Result.Failure(CourseError.CourseIdNull);
            if(!await _courseRepository.CheckExistCourse(courseId))
                return Result.Failure(CourseError.CourseIdError(courseId));
            var result = await _courseRepository.GetCourseById(courseId);
            return Result.SuccessWithObject(result);
        }
        public Task<dynamic> GetCoursesDetail(GetListDTO getListDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<dynamic> GetUICourseVerisonListByCourseId(string courseId)
        {
            try
            {
                var cvl = (List<UICourseVerisonList>)await _courseRepository.GetUICourseVerisonListByCourseId(courseId);
                return Result.SuccessWithObject(cvl);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<dynamic> GetUICoruseById(string courseId)
        {
            try
            {
                var course = await _courseRepository.GetUICoruseById(courseId);
                return Result.SuccessWithObject(course);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region 8 Homepage View - LDQ
        public async Task<dynamic> GetCoursesByBehavior(string userId)
        {
            List<string> userBehavior = await _userBehaviorRepository.GetBehaviorOfUser(userId);
            List<string> categoriesBehavior = await _categoryRepository.GetCategoryByUserBehavior(userBehavior);
            List<CourseDTO> coursesBehavior = await _courseRepository.GetCourseByUserBehavior(categoriesBehavior);
            return Result.SuccessWithObject(coursesBehavior);
        }
        public async Task<dynamic> GetCoursesByTrend()
        {
            List<string> courses = await _courseRepository.GetCourseByTrend();
            List<string> result = new List<string>();
            foreach (var category in courses)
            {
                int b = await _courseRepository.GetNumberOfCourseGroupByCategory(category);
                string a = $"{category} category has {b} courses.";
                result.Add(a);
            }
            return Result.SuccessWithObject(result);
        }
        public async Task<dynamic> GetCourseByGoodFeedback()
        {
            var result = await _courseRepository.GetCourseByGoodFeedbacks();
            return Result.SuccessWithObject(result);
        }
        #endregion
        public async Task<dynamic> GetCourseForGuest()

        {
            try
            {
                List<CourseForGuestDTO> courses = await _courseRepository.GetCourseForGuest();
                if (courses.Count > 0)
                {
                    return Result.SuccessWithObject(courses);
                }
                return Result.Failure(Result.CreateError("Null", "Null courses"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }



        public async Task<dynamic> SearchCourses(SearchDTO searchDTO)
        {
            if (string.IsNullOrEmpty(searchDTO.search))
            {
                return Result.Failure(Result.CreateError("Null", "Search must not be null"));
            }
            if (string.IsNullOrEmpty(searchDTO.filter))
            {
                return Result.Failure(Result.CreateError("Null", "Filter must not be null"));
            }
            if (searchDTO.filter == null)
            {
                return Result.Failure(Result.CreateError("Filter", "Filter not found"));
            }
            var result = await _courseRepository.SearchCourses(searchDTO); 
            return Result.SuccessWithObject(result);
        }

        public Task<dynamic> GetCourseDetailById(string courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> UpdateCourseByCourseId(UpdateCourseDTO courseDTO)
        {
            Result checkExist = await GetCourseById(courseDTO.CourseId);
            if (checkExist.IsFailure)
            {
                return checkExist;
            }
            if (courseDTO.CategoryId != null 
                || courseDTO.CategoryId != null 
                || courseDTO.Price != null 
                || courseDTO.Description != null
                || courseDTO.Thumb != null)
            {
                var result = await _courseRepository.UpdateCourseByCourseId(courseDTO);
                if (result != 0) 
                    return Result.Success();
            }
            return Result.SuccessWithObject(new { Message = "Nothing change" });

        }  

        

        public async Task<Result> ReSubmitCourse(ReSubmitCourse reSubmitCourse)
        {
            var result = await _courseRepository.ReSubmitCourse(reSubmitCourse);
            return Result.SuccessWithObject(result);
        }

    }
}
