using Cursus_API.Controllers;
using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Test.Controller
{
    public class InstructorControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInstructorService _instructorService;
        private readonly IExcelExportService _excelExportService;
        private readonly InstructorController _instructorController;
        public InstructorControllerTest()
        {
            _configuration = A.Fake<IConfiguration>();
            _mailService = A.Fake<IMailService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            _instructorService = A.Fake<IInstructorService>();
            _excelExportService = A.Fake<IExcelExportService>();

            _instructorController = new InstructorController(
                _configuration,
                _excelExportService,
                _httpContextAccessor,
                _mailService,
                _instructorService
            );
        }

        #region [HttpPost("Instructor/approve/{instructorId}")]
        [Fact]
        public async Task ApproveInstructor_InstructorNotFound_ReturnsBadRequest()
        {
            // Arrange
            var instructorId = "123";
            var approveResult = Result.Failure(Result.CreateError("Instructor", "Instructor not found"));

            A.CallTo(() => _instructorService.ApproveInstructorAsync(instructorId)).Returns(Task.FromResult(approveResult));

            // Act
            var result = await _instructorController.ApproveInstructor(instructorId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("Instructor", actualResult.Error.Code);
            Assert.Equal("Instructor not found", actualResult.Error.Message);
        }
        #endregion

        #region [HttpPost("Instructor/reject")]
        [Fact]
        public async Task RejectInstructor_InstructorNotFound_ReturnsBadRequest()
        {
            // Arrange
            var rejectDTO = new RejectInstructorDTO
            {
                instructorId = "123",
                Reason = "Incomplete application"
            };
            var rejectResult = Result.Failure(Result.CreateError("Instructor", "Instructor not found"));

            A.CallTo(() => _instructorService.RejectInstructorAsync(rejectDTO)).Returns(Task.FromResult(rejectResult));

            // Act
            var result = await _instructorController.RejectInstructor(rejectDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("Instructor", actualResult.Error.Code);
            Assert.Equal("Instructor not found", actualResult.Error.Message);
        }
        #endregion

        #region  [HttpGet("Instructor")]
        [Fact]
        public async Task GetInstructors_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var getListDTO = new GetListDTO { /* Set necessary properties */ };
            var instructors = new List<InstructorDetailDTO>
    {
        new InstructorDetailDTO { /* Populate properties */ },
        new InstructorDetailDTO { /* Populate properties */ }
    };
            var expectedResult = Result.SuccessWithObject(instructors);

            A.CallTo(() => _instructorService.GetInstructor(getListDTO))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _instructorController.GetInstructors(getListDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal(instructors, actualResult.Object);
        }

        [Fact]
        public async Task GetInstructors_ReturnsBadRequest_WhenServiceFails()
        {
            // Arrange
            var getListDTO = new GetListDTO { /* Set necessary properties */ };
            var expectedError = Result.CreateError("NULL", "There is no data");
            var expectedResult = Result.Failure(expectedError);

            A.CallTo(() => _instructorService.GetInstructor(getListDTO))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _instructorController.GetInstructors(getListDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(expectedError, actualResult.Error);
        }

        [Fact]
        public async Task GetInstructors_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var getListDTO = new GetListDTO { /* Set necessary properties */ };
            var expectedError = Result.CreateError("EXCEPTION", "Exception message");
            var expectedResult = Result.Failure(expectedError);

            A.CallTo(() => _instructorService.GetInstructor(getListDTO))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _instructorController.GetInstructors(getListDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(expectedError, actualResult.Error);
        }
        #endregion

        #region [HttpPost("Instructor/export")]
        [Fact]
        public async Task ExportInstructors_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var getListDTO = new GetListDTO { /* Set necessary properties */ };
            var instructors = new List<InstructorDetailDTO>
    {
        new InstructorDetailDTO { /* Populate properties */ },
        new InstructorDetailDTO { /* Populate properties */ }
    };
            var instructorResult = Result.SuccessWithObject(instructors);
            var exportResult = new { Message = "Export job enqueued successfully", JobId = "job-id" };
            var expectedExportResult = Result.SuccessWithObject(exportResult);

            A.CallTo(() => _instructorService.GetInstructor(getListDTO))
                .Returns(Task.FromResult(instructorResult));

            A.CallTo(() => _excelExportService.ExportToExcelAsync(instructors, "Instructor"))
                .Returns(Task.FromResult(expectedExportResult));

            // Act
            var result = await _instructorController.ExportInstructors(getListDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal(exportResult, actualResult.Object);
        }

        [Fact]
        public async Task ExportInstructors_ReturnsBadRequest_WhenInstructorServiceFails()
        {
            // Arrange
            var getListDTO = new GetListDTO { /* Set necessary properties */ };
            var expectedError = Result.CreateError("NULL", "There is no data");
            var instructorResult = Result.Failure(expectedError);

            A.CallTo(() => _instructorService.GetInstructor(getListDTO))
                .Returns(Task.FromResult(instructorResult));

            // Act
            var result = await _instructorController.ExportInstructors(getListDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(expectedError, actualResult.Error);
        }
        #endregion

        #region  [HttpPut("Instructor")]
        [Fact]
        public async Task UpdateInstructorInfomation_Success_ReturnsOk()
        {
            // Arrange
            var updateInstructorDTO = new UpdateInstructorDTO
            {
                InstructorId = "123",
                FullName = "Updated Name"
            };

            var updateResult = Result.SuccessWithObject(new
            {
                Message = "Update successfully"
            });

            A.CallTo(() => _instructorService.UpdateInfomationInstructor(updateInstructorDTO))
                .Returns(Task.FromResult(updateResult));

            // Act
            var result = await _instructorController.UpdateInstructorInfomation(updateInstructorDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal("Update successfully", ((dynamic)actualResult.Object).Message);
        }
        [Fact]
        public async Task UpdateInstructorInfomation_InstructorNotExist_ReturnsBadRequest()
        {
            // Arrange
            var updateInstructorDTO = new UpdateInstructorDTO
            {
                InstructorId = "123",
                FullName = "Updated Name"
            };

            var updateResult = Result.Failure(InstructorErrors.InstructorIdNotExist);

            A.CallTo(() => _instructorService.UpdateInfomationInstructor(updateInstructorDTO))
                .Returns(Task.FromResult(updateResult));

            // Act
            var result = await _instructorController.UpdateInstructorInfomation(updateInstructorDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(InstructorErrors.InstructorIdNotExist.Code, actualResult.Error.Code);
            Assert.Equal(InstructorErrors.InstructorIdNotExist.Message, actualResult.Error.Message);
        }
        [Fact]
        public async Task UpdateInstructorInfomation_Exception_ReturnsBadRequest()
        {
            // Arrange
            var updateInstructorDTO = new UpdateInstructorDTO
            {
                InstructorId = "123",
                FullName = "Updated Name"
            };

            var exceptionMessage = "An error occurred";
            var updateResult = Result.Failure(Result.CreateError("EXCEPTION", exceptionMessage));

            A.CallTo(() => _instructorService.UpdateInfomationInstructor(updateInstructorDTO))
                .Returns(Task.FromResult(updateResult));

            // Act
            var result = await _instructorController.UpdateInstructorInfomation(updateInstructorDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("EXCEPTION", actualResult.Error.Code);
            Assert.Equal(exceptionMessage, actualResult.Error.Message);
        }

        #endregion
    }
}
