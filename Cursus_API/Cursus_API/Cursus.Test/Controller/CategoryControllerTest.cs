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
using CursusApi.Controllers;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace Cursus_Test.Controller
{
    public class CategoryControllerTest
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryController _categoryController;

        public CategoryControllerTest()
        {
            _categoryService = A.Fake<ICategoryService>();
            _categoryController = new CategoryController(_categoryService);
        }

        #region post/Category
        [Fact]
        public async Task AddCategory_Success_ReturnsOk()
        {
            // Arrange
            var programLanguage = new ProgramLanguageDTO { Name = "C#", Description = "Programming language", ParentId = "0" };
            var expectedResult = Result.Success();

            A.CallTo(() => _categoryService.CreateProgramLanguage(programLanguage))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.AddCategory(programLanguage);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public async Task AddCategory_NameDuplicated_ReturnsBadRequest()
        {
            // Arrange
            var programLanguage = new ProgramLanguageDTO { Name = "Java", Description = "Programming language", ParentId = "0" };
            var expectedResult = Result.Failure(ProgramLanguageErrors.NameDuplicated());

            A.CallTo(() => _categoryService.CreateProgramLanguage(programLanguage))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.AddCategory(programLanguage);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(ProgramLanguageErrors.NameDuplicated().Code, actualResult.Error.Code);
            Assert.Equal(ProgramLanguageErrors.NameDuplicated().Message, actualResult.Error.Message);
        }

        [Fact]
        public async Task AddCategory_InvalidParentId_ReturnsBadRequest()
        {
            // Arrange
            var programLanguage = new ProgramLanguageDTO { Name = "Python", Description = "Programming language", ParentId = "999" };
            var expectedResult = Result.Failure(ProgramLanguageErrors.ParentIdNotExist());

            A.CallTo(() => _categoryService.CreateProgramLanguage(programLanguage))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.AddCategory(programLanguage);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(ProgramLanguageErrors.ParentIdNotExist().Code, actualResult.Error.Code);
            Assert.Equal(ProgramLanguageErrors.ParentIdNotExist().Message, actualResult.Error.Message);
        }
        #endregion

        #region put/Category
        [Fact]
        public async Task UpdateCategory_Success_ReturnsOk()
        {
            // Arrange
            var programLanguageDTO = new ProgramLanguageDTO { ProgramLanguageId = "1", Name = "C#", Description = "Updated Description", ParentId = "0" };
            var expectedResult = Result.Success();

            A.CallTo(() => _categoryService.UpDateProgramLanguageInfo(programLanguageDTO))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.UpdateCategory(programLanguageDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
        }
        #endregion

        #region Category/status/{categoryId}
        [Fact]
        public async Task UpdateCategoryStatus_Success_ReturnsOk()
        {
            // Arrange
            var categoryId = "1";
            var expectedResult = Result.Success();

            A.CallTo(() => _categoryService.UpDateProgramLanguageStatus(categoryId))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.UpdateCategoryStatus(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public async Task UpdateCategoryStatus_IdNotExist_ReturnsBadRequest()
        {
            // Arrange
            var categoryId = "999";
            var expectedResult = Result.Failure(Result.CreateError("ID", "Id is not exist"));

            A.CallTo(() => _categoryService.UpDateProgramLanguageStatus(categoryId))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.UpdateCategoryStatus(categoryId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("ID", actualResult.Error.Code);
            Assert.Equal("Id is not exist", actualResult.Error.Message);
        }

        #endregion

        #region delete("Category/{categoryId}"
        [Fact]
        public async Task SoftDeleteProgramLanguage_HasCourses_ReturnsBadRequest()
        {
            // Arrange
            var categoryId = "1";
            var expectedResult = Result.Failure(Result.CreateError("Error", "Can not delete because having course"));

            A.CallTo(() => _categoryService.SoftDeleteProgramLanguageAsync(categoryId))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.SoftDeleteProgramLanguage(categoryId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("Error", actualResult.Error.Code);
            Assert.Equal("Can not delete because having course", actualResult.Error.Message);
        }

        [Fact]
        public async Task SoftDeleteProgramLanguage_ProgramLanguageNotFound_ReturnsBadRequest()
        {
            // Arrange
            var categoryId = "999";
            var expectedResult = Result.Failure(Result.CreateError("Error", "No course"));

            A.CallTo(() => _categoryService.SoftDeleteProgramLanguageAsync(categoryId))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.SoftDeleteProgramLanguage(categoryId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal("Error", actualResult.Error.Code);
            Assert.Equal("No course", actualResult.Error.Message);
        }

        [Fact]
        public async Task SoftDeleteProgramLanguage_Success_ReturnsOk()
        {
            // Arrange
            var categoryId = "1";
            var expectedResult = Result.Success();

            A.CallTo(() => _categoryService.SoftDeleteProgramLanguageAsync(categoryId))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.SoftDeleteProgramLanguage(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
        }
        #endregion

        #region HttpGet("Category")
        [Fact]
        public async Task GetAlls_ReturnsOk_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<CategoryUIDTO>
        {
            new CategoryUIDTO { /* Populate properties */ },
            new CategoryUIDTO { /* Populate properties */ }
        };
            var expectedResult = Result.SuccessWithObject(categories);

            A.CallTo(() => _categoryService.GetAllCategory())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.GetAlls();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal(categories, actualResult.Object);
        }

        [Fact]
        public async Task GetAlls_ReturnsBadRequest_WhenServiceFails()
        {
            // Arrange
            var expectedError = Result.CreateError("Error", "Service failure");
            var expectedResult = Result.Failure(expectedError);

            A.CallTo(() => _categoryService.GetAllCategory())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.GetAlls();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(expectedError, actualResult.Error);
        }
        #endregion

        #region HttpGet("Category/nested-categories")
        [Fact]
        public async Task GetNestedCategories_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var nestedCategories = new List<CategoryUIDTO>
        {
            new CategoryUIDTO { /* Populate properties */ },
            new CategoryUIDTO { /* Populate properties */ }
        };
            var expectedResult = Result.SuccessWithObject(nestedCategories);

            A.CallTo(() => _categoryService.GetNestedCategoryDTOs())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.GetNestedCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal(nestedCategories, actualResult.Object);
        }

        [Fact]
        public async Task GetNestedCategories_ReturnsBadRequest_WhenServiceFails()
        {
            // Arrange
            var expectedError = Result.CreateError("Error", "Service failure");
            var expectedResult = Result.Failure(expectedError);

            A.CallTo(() => _categoryService.GetNestedCategoryDTOs())
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.GetNestedCategories();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(expectedError, actualResult.Error);
        }
        #endregion

        #region HttpGet("Category/nested-categories/{id}")
        [Fact]
        public async Task GetNestedCategoriesById_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var categoryId = "test-id";
            var category = new CategoryUIDTO { /* Populate properties */ };
            var expectedResult = Result.SuccessWithObject(category);

            A.CallTo(() => _categoryService.GetCategoryByIdAsync(categoryId))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.GetNestedCategoriesById(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<Result>(okResult.Value);

            Assert.True(actualResult.IsSuccess);
            Assert.Equal(category, actualResult.Object);
        }

        [Fact]
        public async Task GetNestedCategoriesById_ReturnsBadRequest_WhenServiceFails()
        {
            // Arrange
            var categoryId = "test-id";
            var expectedError = Result.CreateError("Error", "Service failure");
            var expectedResult = Result.Failure(expectedError);

            A.CallTo(() => _categoryService.GetCategoryByIdAsync(categoryId))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var result = await _categoryController.GetNestedCategoriesById(categoryId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsType<Result>(badRequestResult.Value);

            Assert.False(actualResult.IsSuccess);
            Assert.Equal(expectedError, actualResult.Error);
        }
        #endregion
    }
}
