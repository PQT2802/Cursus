using Cursus_API.Controllers;
using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.DTOs.Course;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Test.Controller
{
    public class CourseControllerTest
    {
        private readonly CourseController _courseController;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICourseService _courseService;
        private readonly ICourseVersionService _courseVersionService;
        private readonly ICourseContentService _courseContentService;
        private readonly IUserBehaviorService _userBehaviorService;
        private readonly IStudentService _studentService;
        private readonly IAdminService _adminService;
        private readonly IInstructorService _instructorService;
        private readonly IExcelExportService _excelExportService;
        private readonly ICourseCommentService _courseCommentService;
        private readonly IEnrolledCourseService _enrolledCourseService;

        public CourseControllerTest()
        {
            // Setup mocks for dependencies
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            _courseService = A.Fake<ICourseService>();
            _courseVersionService = A.Fake<ICourseVersionService>();
            _courseContentService = A.Fake<ICourseContentService>();
            _userBehaviorService = A.Fake<IUserBehaviorService>();
            _studentService = A.Fake<IStudentService>();
            _adminService = A.Fake<IAdminService>();
            _instructorService = A.Fake<IInstructorService>();
            _excelExportService = A.Fake<IExcelExportService>();
            _courseCommentService = A.Fake<ICourseCommentService>();
            _enrolledCourseService = A.Fake<IEnrolledCourseService>();

            // Initialize CourseController with mocked dependencies
            _courseController = new CourseController(
                _httpContextAccessor,
                _studentService,
                _excelExportService,
                _courseService,
                A.Fake<ICourseRepository>(),
                _courseVersionService,
                _adminService,
                _userBehaviorService,
                _instructorService,
                _courseCommentService,
                _courseContentService,
                _enrolledCourseService
            );
        }

        #region [HttpPost("Course/export")]
        [Fact]
        public async Task ExportCourse_Success_ReturnsOk()
        {
            // Arrange
            var getListDTO = new GetListDTO
            {
                // Initialize with necessary data if needed
            };
            var coursesList = new List<CourseListDTO>
    {
        new CourseListDTO { /* Initialize with sample data */ },
        new CourseListDTO { /* Initialize with sample data */ }
    };
            var getCoursesResult = Result.SuccessWithObject(coursesList);
            var exportResult = Result.SuccessWithObject(new { Message = "Exported successfully" });

            A.CallTo(() => _courseService.GetCoursesList(getListDTO)).Returns(Task.FromResult(getCoursesResult));
            A.CallTo(() => _excelExportService.ExportToExcelAsync(coursesList, "Course")).Returns(Task.FromResult(exportResult));

            // Act
            var result = await _courseController.ExportCourse(getListDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal("Exported successfully", ((dynamic)actualResult.Object).Message);
        }
        [Fact]
        public async Task ExportCourse_NoCourses_ReturnsBadRequest()
        {
            // Arrange
            var getListDTO = new GetListDTO
            {
                // Initialize with necessary data if needed
            };
            var getCoursesResult = Result.Failure(Result.CreateError("Null", "Null courses"));

            A.CallTo(() => _courseService.GetCoursesList(getListDTO)).Returns(Task.FromResult(getCoursesResult));

            // Act
            var result = await _courseController.ExportCourse(getListDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("Null", actualResult.Error.Code);
            Assert.Equal("Null courses", actualResult.Error.Message);
        }
        [Fact]
        public async Task ExportCourse_ExportFailure_ReturnsBadRequest()
        {
            // Arrange
            var getListDTO = new GetListDTO
            {
                // Initialize with necessary data if needed
            };
            var coursesList = new List<CourseListDTO>
    {
        new CourseListDTO { /* Initialize with sample data */ }
    };
            var getCoursesResult = Result.SuccessWithObject(coursesList);
            var exportResult = Result.Failure(Result.CreateError("ExportError", "Export failed"));

            A.CallTo(() => _courseService.GetCoursesList(getListDTO)).Returns(Task.FromResult(getCoursesResult));
            A.CallTo(() => _excelExportService.ExportToExcelAsync(coursesList, "Course")).Returns(Task.FromResult(exportResult));

            // Act
            var result = await _courseController.ExportCourse(getListDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("ExportError", actualResult.Error.Code);
            Assert.Equal("Export failed", actualResult.Error.Message);
        }

        #endregion

        #region [HttpPut("Course/inactivated")]
        [Fact]
        public async Task InactivatedCourse_Success_ReturnsOk()
        {
            // Arrange
            int id = 1;
            TimeSpan maintainDays = TimeSpan.FromDays(30);
            var courseVersion = new CourseVersion
            {
                CourseVersionId = id,
                Status = "Activate",
                MaintainDay = DateTime.Now
            };
            var updatedCourseVersion = new CourseVersion
            {
                CourseVersionId = id,
                Status = "Deactivate",
                MaintainDay = DateTime.Now.Add(maintainDays)
            };
            var result = Result.SuccessWithObject(updatedCourseVersion);

            A.CallTo(() => _courseVersionService.UpdateDeactivedStatus(id, maintainDays))
                .Returns(Task.FromResult(result));

            // Act
            var actionResult = await _courseController.InactivatedCourse(id, maintainDays);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            var actualCourseVersion = Assert.IsType<CourseVersion>(actualResult.Object);
            Assert.Equal("Deactivate", actualCourseVersion.Status);
            Assert.Equal(updatedCourseVersion.MaintainDay, actualCourseVersion.MaintainDay);
        }

        [Fact]
        public async Task InactivatedCourse_VersionNotFound_ReturnsOk()
        {
            // Arrange
            int id = 1;
            TimeSpan maintainDays = TimeSpan.FromDays(30);
            var result = Result.Failure(CourseVersionError.NullVersionOfCourse);

            A.CallTo(() => _courseVersionService.UpdateDeactivedStatus(id, maintainDays))
                .Returns(Task.FromResult(result));

            // Act
            var actionResult = await _courseController.InactivatedCourse(id, maintainDays);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(CourseVersionError.NullVersionOfCourse.Code, actualResult.Error.Code);
            Assert.Equal(CourseVersionError.NullVersionOfCourse.Message, actualResult.Error.Message);
        }

        [Fact]
        public async Task InactivatedCourse_AlreadyDeactivated_ReturnsOk()
        {
            // Arrange
            int id = 1;
            TimeSpan maintainDays = TimeSpan.FromDays(30);
            var courseVersion = new CourseVersion
            {
                CourseVersionId = id,
                Status = "Deactivate",
                MaintainDay = DateTime.Now
            };
            var result = Result.Failure(CourseVersionError.DeactivatedCourse);

            A.CallTo(() => _courseVersionService.UpdateDeactivedStatus(id, maintainDays))
                .Returns(Task.FromResult(result));

            // Act
            var actionResult = await _courseController.InactivatedCourse(id, maintainDays);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(CourseVersionError.DeactivatedCourse.Code, actualResult.Error.Code);
            Assert.Equal(CourseVersionError.DeactivatedCourse.Message, actualResult.Error.Message);
        }

        #endregion

        #region [HttpPut("Course/approve/{courseId}")]        
        [Fact]
        public async Task ApproveCourse_CourseNotFound_ReturnsBadRequest()
        {
            // Arrange
            var courseId = "123";
            var result = Result.Failure(Result.CreateError("Course", "Course not found"));

            A.CallTo(() => _courseService.ApproveCourse(courseId))
                .Returns(Task.FromResult(result));

            // Act
            var actionResult = await _courseController.ApproveCourse(courseId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("Course", actualResult.Error.Code);
            Assert.Equal("Course not found", actualResult.Error.Message);
        }
        #endregion

        #region [HttpPut("Course/course-admin-deactivation/{courseId}")]
        [Fact]
        public async Task DeactiveCourseByAdmin_Success_ReturnsOk()
        {
            // Arrange
            var courseId = "123";
            var reason = "Maintenance required";
            var result = Result.SuccessWithObject(new { Message = "Course deactivated successfully" });

            A.CallTo(() => _adminService.DeactiveCourseByAdmin(courseId))
                .Returns(Task.FromResult(result));

            // Act
            var actionResult = await _courseController.DeactiveCourseByAdmin(courseId, reason);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal("Course deactivated successfully", ((dynamic)actualResult.Object).Message);
        }
        [Fact]
        public async Task DeactiveCourseByAdmin_CourseNotFound_ReturnsBadRequest()
        {
            // Arrange
            var courseId = "123";
            var reason = "Maintenance required";
            var result = Result.Failure(CourseError.CourseIsNotExist);

            A.CallTo(() => _adminService.DeactiveCourseByAdmin(courseId))
                .Returns(Task.FromResult(result));

            // Act
            var actionResult = await _courseController.DeactiveCourseByAdmin(courseId, reason);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(CourseError.CourseIsNotExist.Code, actualResult.Error.Code);
            Assert.Equal(CourseError.CourseIsNotExist.Message, actualResult.Error.Message);
        }
        [Fact]
        public async Task DeactiveCourseByAdmin_InvalidOperationException_ReturnsNotFound()
        {
            // Arrange
            var courseId = "123";
            var reason = "Maintenance required";
            A.CallTo(() => _adminService.DeactiveCourseByAdmin(courseId))
                .Throws(new InvalidOperationException("Course not found"));

            // Act
            var actionResult = await _courseController.DeactiveCourseByAdmin(courseId, reason);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("Course not found", notFoundResult.Value);
        }

        #endregion

        #region [HttpPut("Course/Instructor/update-course")]
        
        [Fact]
        public async Task UpdateCourse_CourseNotFound_ReturnsBadRequest()
        {
            // Arrange
            var updateCourseDTO = new UpdateCourseDTO
            {
                CourseId = "123",
                CategoryId = "456",
                Price = 299.99
            };

            var notFoundResult = Result.Failure(Result.CreateError("Course", "Course not found"));

            A.CallTo(() => _courseService.UpdateCourseByCourseId(updateCourseDTO))
                .Returns(Task.FromResult(notFoundResult));

            // Act
            var actionResult = await _courseController.UpdateCourse(updateCourseDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("Course", actualResult.Error.Code);
            Assert.Equal("Course not found", actualResult.Error.Message);
        }
        [Fact]
        public async Task UpdateCourse_InvalidOperationException_ReturnsNotFound()
        {
            // Arrange
            var updateCourseDTO = new UpdateCourseDTO
            {
                CourseId = "123"
            };

            A.CallTo(() => _courseService.UpdateCourseByCourseId(updateCourseDTO))
                .Throws(new InvalidOperationException("Course not found"));

            // Act
            var actionResult = await _courseController.UpdateCourse(updateCourseDTO);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("Course not found", notFoundResult.Value);
        }
        

        #endregion
    }
}
