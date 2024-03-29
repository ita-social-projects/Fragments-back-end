﻿using FluentAssertions.Execution;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Fragments.Test.Base;
using Fragments.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

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
        public async Task GetAsync_WhenUserExists_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange
            userService.Setup(service => service.GetByIdAsync(user.Id)).ReturnsAsync(user);

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
        public async Task Login_WhenUserExists_ReturnsOkObjectResult(AuthenticateRequestDto user)
        {
            // Arrange
            userService.Setup(service => service.LoginAsync(user)).ReturnsAsync(new AuthenticateResponseDto(new User { Email = user.Email }, "token"));
            // Act
            var result = await userController.Login(user);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<AuthenticateResponseDto>();
                ((result as OkObjectResult)?.Value as AuthenticateResponseDto)?.Email.Should().Be(user.Email);
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task PostUser_WhenUserExists_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange       

            // Act
            var result = await userController.PostUser(user);

            // Assert
            userService.Verify(service => service.CreateAsync(user));

        }
        [Theory]
        [AutoEntityData]
        public async Task GetMe_WhenUserExists_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange
            userService.Setup(service => service.GetMeAsync()).ReturnsAsync(user);

            // Act
            var result = await userController.GetMe();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDto>();
                ((result as OkObjectResult)?.Value as UserDto)?.Id.Should().Be(user.Id);
            }
        }
        [Fact]
        public void Logout_WhenUserExists_ReturnsOkObjectResult()
        {
            //Arrange

            //Act
            var result = userController.Logout();

            //Assert
            result.Should().BeOfType<ActionResult<string>>();
        }
        [Theory]
        [AutoEntityData]
        public async void Update_WhenUserExists_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange

            //Act
            var result = await userController.Update(user);

            //Assert
            result.Should().BeOfType<ActionResult<UserDto>>();
        }
    }
}
