using FluentValidation.TestHelper;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Fragments.Domain.Validations;
using Moq;

namespace Fragments.Test.FluentValidationTest
{
    public class UserValidatorTest
    {
        private readonly Mock<IUserService> service;
        private readonly UserValidator validator;

        public UserValidatorTest()
        {
            service = new Mock<IUserService>();
            validator = new UserValidator(service.Object);
        }
        [Theory]
        [InlineData("user")]
        public void Email_IsNotValid_GeneratesValidationError(string email)
        {
            // Arrange
            var model = new UserDto { Email = email };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Email);

        }

        [Theory]
        [InlineData("user")]
        public void IsEmailAlreadyExistsAsync_WhenEmailExists_GeneratesValidationError(string email)
        {
            // Arrange
            service.Setup(x => x.IsEmailAlreadyExistsAsync(email)).ReturnsAsync(true);
            var model = new UserDto { Email = email };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Email);

        }

        [Theory]
        [InlineData("user@gmail.com")]
        public void IsEmailAlreadyExistsAsync_WhenEmailIsNotExist_NotGeneratesValidationError(string email)
        {
            // Arrange
            service.Setup(x => x.IsEmailAlreadyExistsAsync(email)).ReturnsAsync(false);
            var model = new UserDto { Email = email };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(user => user.Email);

        }

        [Theory]
        [InlineData("user@example.com")]
        public void Email_IsValid_NotGeneratesValidationError(string email)
        {
            // Arrange
            var model = new UserDto { Email = email };

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(user => user.Email);

        }
        [Theory]
        [InlineData("2000-12-31")]
        public void Birthday_IsValid_NotGeneratesValidationError(string date)
        {
            // Arrange
            var model = new UserDto { Birthday = DateTime.Parse(date) };
            // Act
            var result = validator.TestValidate(model);
            // Assert
            result.ShouldNotHaveValidationErrorFor(user => user.Birthday);
        }
        [Theory]
        [InlineData("2100-12-31")]
        public void Birthday_IsNotValid_GeneratesValidationError(string date)
        {
            // Arrange
            var model = new UserDto { Birthday = DateTime.Parse(date) };
            // Act
            var result = validator.TestValidate(model);
            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Birthday);
        }
    }
}
