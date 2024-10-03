using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.Student;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class CourseCommentService : ICourseCommentService
    {
        private readonly ICourseCommentRepository _courseCommentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseVersionRepository _courseVersionRepository;
        private readonly ICourseCommentRepository _coursecommentRepository;
        public CourseCommentService(ICourseCommentRepository courseCommentRepository, IUserRepository UserRepository, ICourseVersionRepository courseVersionRepository, ICourseCommentRepository coursecommentRepository)
        {
            _courseCommentRepository = courseCommentRepository;
            _userRepository = UserRepository;
            _courseVersionRepository = courseVersionRepository;
            _coursecommentRepository = coursecommentRepository;
        }
        public async Task<dynamic> AdminCommentCourse(CommentCourseDTO commentCourseDTO, string UserId)
        {
            if (commentCourseDTO.ToCourseVersionId == null) return Result.Failure(CourseCommentError.ToCourseVersionIdNull);
            if (!await _courseVersionRepository.CheckExistCourseVersion(commentCourseDTO.ToCourseVersionId))
                return Result.Failure(CourseCommentError.ToCourseVersionIdWrong(commentCourseDTO.ToCourseVersionId));
            try
            {
                CourseComment courseComment = new CourseComment()
                {
                    FromUserId = UserId,
                    ToCourseVersionId = commentCourseDTO.ToCourseVersionId,
                    Description = commentCourseDTO.Description,
                    Attachment = commentCourseDTO.Attachment,
                    IsAdmin = true,
                    IsDelete = false,
                    IsHide = false,
                    CreateDate = DateTime.Now,
                };
                if (courseComment == null) return Result.Failure(CourseCommentError.ErrorToCreateComment);
                await _courseCommentRepository.AddCourseComment(courseComment);
                return Result.Success();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        public async Task<dynamic> CommentCourse(CommentCourseDTO commentCourseDTO, string UserId)
        {
            if (commentCourseDTO.ToCourseVersionId == null) return Result.Failure(CourseCommentError.ToCourseVersionIdNull);
            if (!await _courseVersionRepository.CheckExistCourseVersion(commentCourseDTO.ToCourseVersionId))
                return Result.Failure(CourseCommentError.ToCourseVersionIdWrong(commentCourseDTO.ToCourseVersionId));
            try
            {
                CourseComment courseComment = new CourseComment()
                {
                    FromUserId = UserId,
                    ToCourseVersionId = commentCourseDTO.ToCourseVersionId,
                    Description = commentCourseDTO.Description,
                    Attachment = commentCourseDTO.Attachment,
                    IsAdmin = false,
                    IsDelete = false,
                    IsHide = false,
                    CreateDate = DateTime.Now,
                };
                if (courseComment == null) return Result.Failure(CourseCommentError.ErrorToCreateComment);
                await _courseCommentRepository.AddCourseComment(courseComment);
                return Result.Success();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<Result> HideCourseCommnet(int coursseCommentId)
        {
            if (coursseCommentId == null) return Result.Failure(CourseCommentError.CourseCommnetIdNull);
            if (await _courseCommentRepository.CheckCourseComment(coursseCommentId))
            {
                await _courseCommentRepository.HideCourseComment(coursseCommentId);
                return Result.Success();
            }
            else return Result.Failure(CourseCommentError.CourseCommentIdWrong(coursseCommentId));
        }
        public async Task<dynamic> ReportByStudent(ReportByStudent report, string userId)
        {
            
            var courseversionid = _courseVersionRepository.GetCourseVersionById(report.ToCourseVersionId);
           
            try
            {
                var userreport = new CourseComment
                {
                    FromUserId = userId,
                    ToCourseVersionId = report.ToCourseVersionId,
                    Description = report.Description,
                    Attachment = report.Attachment,
                    IsAdmin = false,
                    IsComment = false,
                    IsDelete = false,
                    IsHide = false,
                    CreateDate = DateTime.Now,
                };
                await _coursecommentRepository.AddCourseComment(userreport);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Result.CreateError("Null", "Course version is doesn't has"));
            }
        }
        public async Task<dynamic> FeedbackByStudent(FeedbackByStudent feedback, string userId)
        {
            if (!await _coursecommentRepository.CheckIfEnrolled(userId, feedback.ToCourseVersionId))
               return Result.Failure(Result.CreateError("Null", "You must enroll course to feedback"));
            var courseversionid = _courseVersionRepository.GetCourseVersionById(feedback.ToCourseVersionId);

            try
            {
                var userreport = new CourseComment
                {
                    FromUserId = userId,
                    ToCourseVersionId = feedback.ToCourseVersionId,
                    Description = feedback.Description,
                    Attachment = feedback.Attachment,
                    IsAdmin = false,
                    IsComment = true,
                    IsDelete = false,
                    IsHide = false,
                    CreateDate = DateTime.Now,
                };
                await _coursecommentRepository.AddCourseComment(userreport);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Result.CreateError("Null", "Course version is doesn't has"));
            }
        }  
    }
}
