using System;
using System.Threading.Tasks;
using Cursus_API.Controllers;
using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Cursus_Test.Controller;
public class CourseContentControllerTests
{
    private readonly ICourseContentService _courseContentService;
    private readonly CourseContentController _courseContentController;

    public CourseContentControllerTests()
    {
        _courseContentService = A.Fake<ICourseContentService>();
        _courseContentController = new CourseContentController(_courseContentService);
    }

    [Fact]
    public async Task UpdateCourseContents_Success_ReturnsOk()
    {
        // Arrange
        var updateDTO = new UpdateCourseContentDTO
        {
            CourseContentId = "123",
            CourseVersionDetailId = "456",
            Title = "New Title",
            Url = "http://newurl.com",
            Time = 10.0,  // Time as double
            Type = "Video"
        };

        var updateResult = Result.SuccessWithObject(new
        {
            Message = "Update successfully"
        });

        A.CallTo(() => _courseContentService.UpdateCourseContents(updateDTO))
            .Returns(Task.FromResult(updateResult));

        // Act
        var result = await _courseContentController.UpdateCourseContents(updateDTO);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResult = Assert.IsType<Result>(okResult.Value);

        Assert.True(actualResult.IsSuccess);
        Assert.Equal("Update successfully", ((dynamic)actualResult.Object).Message);
    }

    [Fact]
    public async Task UpdateCourseContents_CourseContentIdNull_ReturnsBadRequest()
    {
        // Arrange
        var updateDTO = new UpdateCourseContentDTO
        {
            CourseContentId = null,
            CourseVersionDetailId = "456",
            Title = "New Title",
            Url = "http://newurl.com",
            Time = 10.0,  // Time as double
            Type = "Video"
        };

        var errorResult = Result.Failure(CourseContentError.ccIdNull());

        A.CallTo(() => _courseContentService.UpdateCourseContents(updateDTO))
            .Returns(Task.FromResult(errorResult));

        // Act
        var result = await _courseContentController.UpdateCourseContents(updateDTO);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var actualResult = Assert.IsType<Result>(badRequestResult.Value);

        Assert.False(actualResult.IsSuccess);
        Assert.Equal(CourseContentError.ccIdNull().Code, actualResult.Error.Code);
        Assert.Equal(CourseContentError.ccIdNull().Message, actualResult.Error.Message);
    }
}
