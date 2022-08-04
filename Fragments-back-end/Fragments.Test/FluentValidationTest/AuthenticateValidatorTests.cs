using FluentValidation.TestHelper;
using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using Fragments.Domain.Validations;

namespace Fragments.Test.FluentValidationTest
{
    public class AuthenticateValidatorTests
    {
        private readonly Mock<IUserService> userService;
        private readonly AuthenticateValidator validator;

        public AuthenticateValidatorTests()
        {
            userService = new Mock<IUserService>();
            validator = new AuthenticateValidator(userService.Object);
        }
        [Theory]
        [InlineData("email@gmail.com")]
        public void Email_IsAlreadyExists_NotGeneratesValidationError(string email)
        {
            //Arrange 
            userService.Setup(service => service.IsEmailAlreadyExistsAsync(email)).ReturnsAsync(true);
            var request = new AuthenticateRequestDto { Email = email };

            //Act
            var result = validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveValidationErrorFor(user => user.Email);
        }
        [Theory]
        [InlineData("email")]
        public void Email_IsNotAlreadyExists_GeneratesValidationError(string email)
        {
            //Arrange 
            userService.Setup(service => service.IsEmailAlreadyExistsAsync(email)).ReturnsAsync(false);
            var request = new AuthenticateRequestDto { Email = email };

            //Act
            var result = validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(user => user.Email);
        }
    }
}
