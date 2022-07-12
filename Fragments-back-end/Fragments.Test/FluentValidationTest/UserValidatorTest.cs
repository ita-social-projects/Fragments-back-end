using FluentValidation.TestHelper;
using Fragments.Domain.Dto;
using Fragments.Domain.Validations;

namespace Fragments.Test.FluentValidationTest
{
    public class UserValidatorTest
    {
        private readonly UserValidator validator;

        public UserValidatorTest()
        {
            validator = new UserValidator();
        }
        [Theory]
        [InlineData("user")]
        public void Email_IsNotValid_GeneratesValidationError(string email)
        {
            // Arrange
            var model = new UserDTO { Email = email };
            // Act
            var result = validator.TestValidate(model);
            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Email);


        }
        [Theory]
        [InlineData("user@example.com")]
        public void Email_IsValid_NotGeneratesValidationError(string email)
        {
            // Arrange
            var model = new UserDTO { Email = email };
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
            var model = new UserDTO { Birthday = DateTime.Parse(date) };
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
            var model = new UserDTO { Birthday = DateTime.Parse(date) };
            // Act
            var result = validator.TestValidate(model);
            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Birthday);
        }
    }
}
