using Xunit;
using Moq;
using Cursus_API.Controllers;
using Cursus_Business.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Cursus_Business.Service.Implements;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Data.Repositories.Interfaces;

namespace Cursus_Test.Controller
{
    public class UserControllerTest
    {
        private readonly IUserService _userService;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly UserController _userController;


        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _userDetailRepository = A.Fake<IUserDetailRepository>();
            _userController = new UserController(
                    new Mock<IConfiguration>().Object,
                    _userService,
                    new Mock<IUserDetailService>().Object,
                    new Mock<ITokenService>().Object,
                    new Mock<IUserBehaviorService>().Object,
                    new Mock<IRefreshTokenService>().Object,
                    new Mock<IMailServiceV2>().Object,
                    new Mock<IExcelExportService>().Object
                );
        }
        #region User/login
        [Fact]
        public async Task SignIn_UserExists_ReturnsOk()
        {
            // Arrange
            var signInDTO = new SignInDTO { Email = "student1@gmail.com", Password = "String12!" };
            var expectedResult = Task.FromResult(Result.Success());

            A.CallTo(() => _userService.SignIn(signInDTO))
                .Returns(expectedResult);

            // Act
            var result = await _userController.SignIn(signInDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Empty(actualResult.Error.Code);
            Assert.Empty(actualResult.Error.Message);
        }

        [Fact]
        public async Task SignIn_InvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var signInDTO = new SignInDTO { Email = "testuser", Password = "wrongpassword" };
            var expectedResult = Task.FromResult(Result.Failure(SignInErrors.InputEmpty()));

            A.CallTo(() => _userService.SignIn(signInDTO))
                .Returns(expectedResult);

            // Act
            var result = await _userController.SignIn(signInDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(SignInErrors.InputEmpty().Code, actualResult.Error.Code);
            Assert.Equal(SignInErrors.InputEmpty().Message, actualResult.Error.Message);
        }

        #endregion
        #region User/sign-up-for-student
        [Fact]
        public async Task SignUpForStudent_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO
            {
                Email = "student1@gmail.com",
                Password = "short",
                FullName = "",
                DateOfBirth = new DateTime(2010, 1, 1),
                Phone = "123",
                Address = ""
            };

            var errors = new List<Error>
        {
            UserErrors.PasswordIsInvalid,
            UserErrors.FullnameIsEmpty,
            UserErrors.DOBIsNotEnogh,
            UserErrors.PhoneIsInvalid,
            UserErrors.AddressIsEmpty
        };

            var signUpResult = Result.Failures(errors);

            A.CallTo(() => _userService.SignUpForStudent(registerUserDTO))
                .Returns(Task.FromResult(signUpResult));

            // Act
            var result = await _userController.SignUpForStudent(registerUserDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(errors.Count, actualResult.Errors.Count);
            foreach (var error in errors)
            {
                Assert.Contains(actualResult.Errors, e => e.Code == error.Code && e.Message == error.Message);
            }
        }

        #endregion
        #region User/active
        [Fact]
        public async Task ApproveUserIsStudent_UserNotExist_ReturnsNotFound()
        {
            // Arrange
            var approveStudent = new ApproveStudent { StudentId = "US000000053" };
            var expectedResult = Result.Failure(UserErrors.UserIsNotExist);

            A.CallTo(() => _userService.ApproveStudent(approveStudent))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _userController.ApproveUserIsStudent(approveStudent);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var actualResult = Assert.IsType<Result>(notFoundResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(UserErrors.UserIsNotExist.Code, actualResult.Error.Code);
            Assert.Equal(UserErrors.UserIsNotExist.Message, actualResult.Error.Message);
        }

        [Fact]
        public async Task ApproveUserIsStudent_UserExists_ReturnsOk()
        {
            // Arrange
            var approveStudent = new ApproveStudent { StudentId = "US00000005" };
            var expectedResult = Result.Failure(UserErrors.UserIsNotExist);

            A.CallTo(() => _userService.ApproveStudent(approveStudent))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _userController.ApproveUserIsStudent(approveStudent);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var actualResult = Assert.IsType<Result>(notFoundResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(UserErrors.UserIsNotExist.Code, actualResult.Error.Code);
            Assert.Equal(UserErrors.UserIsNotExist.Message, actualResult.Error.Message);
        }
        #endregion
        #region User/deactive
        [Fact]
        public async Task RejectUserIsStudent_UserNotExist_ReturnsNotFound()
        {
            // Arrange
            var rejectStudent = new RejectStudent { StudentID = "1" };
            var expectedResult = Result.Failure(new Error("User", "User is not exist!"));

            A.CallTo(() => _userService.RejectStudent(rejectStudent))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _userController.RejectUserIsStudent(rejectStudent);

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(notFoundResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("User", actualResult.Error.Code);
            Assert.Equal("User is not exist!", actualResult.Error.Message);
        }

        [Fact]
        public async Task RejectUserIsStudent_UserExists_ReturnsOk()
        {
            // Arrange
            var rejectStudent = new RejectStudent { StudentID = "1" };
            var expectedResult = Result.Success();

            A.CallTo(() => _userService.RejectStudent(rejectStudent))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _userController.RejectUserIsStudent(rejectStudent);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Empty(actualResult.Error.Code);
            Assert.Empty(actualResult.Error.Message);
        }
        #endregion
        #region User/student-detail-list
        [Fact]
        public async Task GetStudents_ReturnsOk()
        {
            // Arrange
            var students = new List<StudentDTO>
    {
        new StudentDTO { FullName = "John Doe", Phone = "123456789", Email = "john.doe@example.com", NumberJoinCourse = 3, NumberProgressCourse = 1 },
        new StudentDTO { FullName = "Jane Smith", Phone = "987654321", Email = "jane.smith@example.com", NumberJoinCourse = 2, NumberProgressCourse = 2 }
    };
            var expectedResult = Result.SuccessWithObject(students);

            A.CallTo(() => _userService.GetListOfStudent())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _userController.GetStudents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.NotNull(actualResult.Object);
            Assert.Equal(students.Count, ((List<StudentDTO>)actualResult.Object).Count);
        }

        [Fact]
        public async Task GetStudents_ReturnsBadRequest()
        {
            // Arrange
            var expectedResult = Result.Failure(Result.CreateError("NULL", "There is no data"));

            A.CallTo(() => _userService.GetListOfStudent())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _userController.GetStudents();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("NULL", actualResult.Error.Code);
            Assert.Equal("There is no data", actualResult.Error.Message);
        }
        #endregion
    }
}
