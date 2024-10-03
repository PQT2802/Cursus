using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Context;
using Cursus_Business.Exceptions.ErrorHandler;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Cursus_API.Controllers;
using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cursus_Test
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IUserDetailService> _mockUserDetailService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<IUserBehaviorService> _mockUserBehaviorService;
        private readonly Mock<IRefreshTokenService> _mockRefreshTokenService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IInstructorService> _mockInstructorService;
        private readonly Mock<IMailServiceV2> _mockMailServiceV2;
        private readonly Mock<ICourseService> _mockCourseService;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly LMS_CursusDbContext _dbContext;
        private readonly UserController _controller;

        public string HttpMessageHandler { get; private set; }

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockUserDetailService = new Mock<IUserDetailService>();
            _mockTokenService = new Mock<ITokenService>();
            _mockUserBehaviorService = new Mock<IUserBehaviorService>();
            _mockRefreshTokenService = new Mock<IRefreshTokenService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockInstructorService = new Mock<IInstructorService>();
            _mockMailServiceV2 = new Mock<IMailServiceV2>();
            _mockCourseService = new Mock<ICourseService>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<LMS_CursusDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new LMS_CursusDbContext(options);

            _controller = new UserController(
                _dbContext,
                _mockConfiguration.Object,
                _mockUserService.Object,
                _mockUserDetailService.Object,
                _mockTokenService.Object,
                _mockUserBehaviorService.Object,
                _mockRefreshTokenService.Object,
                _mockHttpContextAccessor.Object,
                _mockInstructorService.Object,
                _mockMailServiceV2.Object,
                _mockCourseService.Object
            );
        }

        [Fact]
        public async Task SignUpForStudent_ReturnsOk_WhenSignUpAndSendMailAreSuccessful()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO
            {
                Email = "test@example.com",
                Password = "String12!",
                FullName = "Test",
                DateOfBirth = new DateTime(2003, 10, 16),
                Phone = "+84934041240",
                Address = "Test address"
            };
            string expectedMessage = "Sign up successfully, please check and confirm your mail";

            // Set up the mocks to return success with the expected message
            _mockUserService.Setup(s => s.SignUpForStudent(It.IsAny<RegisterUserDTO>()))
                .ReturnsAsync(Result.Success());
            _mockUserService.Setup(s => s.SendMailComfirm(It.IsAny<string>()))
                .ReturnsAsync(Result.SuccessWithObject(new { Message = expectedMessage }));

            // Act
            var result = await _controller.SignUpForStudent(registerUserDTO);

            // Assert
            
        }


        [Fact]
        public async Task SignUpForStudent_ReturnsBadRequest_WhenFieldsAreMissingOrIncorrect()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO { Email = "", Password = "short" }; // Missing email and short password
            var errors = new List<Error>
            {
                UserErrors.EmailIsEmpty,
                UserErrors.PasswordMinLength
            };
            var failedResult = Result.Failures(errors);
            _mockUserService.Setup(s => s.SignUpForStudent(It.IsAny<RegisterUserDTO>()))
                .ReturnsAsync(failedResult);

            // Act
            var result = await _controller.SignUpForStudent(registerUserDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(failedResult, badRequestResult.Value);
        }

        [Fact]
        public async Task SignUpForStudent_ReturnsBadRequest_WhenUserErrorsExist()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO
            {
                Email = "test@example.com",
                Password = "String12!",
                FullName = "Test",
                DateOfBirth = new DateTime(2003, 10, 16),
                Phone = "+84934041240",
                Address = "Test address"
            };
            var errors = new List<Error>
            {
                UserErrors.EmailAlreadyUsed(registerUserDTO.Email),
                UserErrors.PhoneAlreadyUsed(registerUserDTO.Phone)
            };
            var failedResult = Result.Failures(errors);
            _mockUserService.Setup(s => s.SignUpForStudent(It.IsAny<RegisterUserDTO>()))
                .ReturnsAsync(failedResult);

            // Act
            var result = await _controller.SignUpForStudent(registerUserDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(failedResult, badRequestResult.Value);
        }
    }
}
