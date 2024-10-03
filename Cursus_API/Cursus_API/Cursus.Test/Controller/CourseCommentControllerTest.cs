using Cursus_API.Controllers;
using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using CursusApi.Controllers;
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
    public class CourseCommentControllerTest
    {
        private readonly ICourseCommentService _courseCommentService;
        private readonly IInstructorService _instructorService;
        private readonly ICourseVersionService _courseVersionService;
        private readonly CourseCommentController _courseCommentController;

        public CourseCommentControllerTest()
        {
            _courseCommentService = A.Fake<ICourseCommentService>();
            _instructorService = A.Fake<IInstructorService>();
            _courseVersionService = A.Fake<ICourseVersionService>();
            _courseCommentController = new CourseCommentController(_courseCommentService, _instructorService, _courseVersionService);
        }

        #region HttpPut("CourseComment/hide/{courseCommentId}")
        [Fact]
        public async Task HideCourseComment_ValidId_ReturnsOk()
        {
            // Arrange
            int validCourseCommentId = 1;
            A.CallTo(() => _courseCommentService.HideCourseCommnet(validCourseCommentId)).Returns(Task.FromResult(Result.Success()));

            // Act
            var result = await _courseCommentController.HideCourseComment(validCourseCommentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Result>(okResult.Value);
            Assert.True(((Result)okResult.Value).IsSuccess);
        }

        [Fact]
        public async Task HideCourseComment_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int invalidCourseCommentId = 999;
            A.CallTo(() => _courseCommentService.HideCourseCommnet(invalidCourseCommentId)).Returns(Task.FromResult(Result.Failure(CourseCommentError.CourseCommentIdWrong(invalidCourseCommentId))));

            // Act
            var result = await _courseCommentController.HideCourseComment(invalidCourseCommentId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<Result>(badRequestResult.Value);
            Assert.False(((Result)badRequestResult.Value).IsSuccess);
        }
        #endregion

       
    }
}
