using FluentAssertions;
using FluentAssertions.Execution;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using Fragments.Test.Base;
using Fragments.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Fragments.Test.Controllers
{
    public class UserControllerTest : Base.Base
    {
        private readonly Mock<IUserService> userService;
        private readonly UsersController userController;

        public UserControllerTest()
        {
            userService = new Mock<IUserService>();
            userController = new UsersController(userService.Object);
        }
        [Theory]
        [AutoEntityData]
        public async Task GetAsync_WhenUserExists_ReturnsOkObjectResult(UserDTO user)
        {
            // Arrange
            userService.Setup(service => service.GetAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await userController.GetUser(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as ObjectResult)?.Value.Should().Be(user);
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task Login_WhenUserExists_ReturnsOkObjectResult(AuthenticateRequestDTO user)
        {
            // Arrange
            userService.Setup(service => service.LoginAsync(user)).ReturnsAsync(new AuthenticateResponseDTO(new User { Email = user.Email }, "token"));
            // Act
            var result = await userController.Login(user);

            // Assert
            using(new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<AuthenticateResponseDTO>();
                ((result as OkObjectResult)?.Value as AuthenticateResponseDTO)?.Email.Should().Be(user.Email);
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task Login_WhenUserExists_BadRequestObjectResult(AuthenticateRequestDTO user)
        {
            // Arrange
            AuthenticateResponseDTO? response = null;
#pragma warning disable CS8620 
            userService.Setup(service => service.LoginAsync(user)).ReturnsAsync(response);
#pragma warning restore CS8620 
            // Act
            var result = await userController.Login(user);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<BadRequestObjectResult>();
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task PostUser_WhenUserExists_ReturnsOkObjectResult(UserDTO user)
        {
            // Arrange
            userService.Verify(service => service.CreateAsync(user));        

            // Act
            var result = await userController.PostUser(user);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkResult>();
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task GetMe_WhenUserExists_ReturnsOkObjectResult(UserDTO user)
        {
            // Arrange
            userService.Setup(service => service.GetMe()).ReturnsAsync(user);

            // Act
            var result = await userController.GetMe();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDTO>();
                ((result as OkObjectResult)?.Value as UserDTO)?.Id.Should().Be(user.Id);
            }
        }
    }
}
