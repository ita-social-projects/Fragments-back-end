using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Implementation;
using Fragments.Domain.Services.Interfaces;
using Fragments.Test.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Fragments.Test.Services
{
    public class UserServiceTest : Base.Base
    {
        private readonly FragmentsContext context;
        private readonly Mock<IConfiguration> configuration;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly IUserService service;
        public UserServiceTest()
        {
            configuration = new Mock<IConfiguration>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            context = ContextGenerator.GetContext();
            service = new UserService(context, Mapper, httpContextAccessor.Object, configuration.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task CreateAsync_WhenUserIsValid_AddsToDb(UserDto user)
        {
            //Arrange

            // Act
            await service.CreateAsync(user);
            var result = await context.Users.FindAsync(user.Id);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetMeAsync_WhenUserIsNotAuthorized_ReturnsNull()
        {
            //Arrange

            httpContextAccessor.SetupGet(x => x.HttpContext).Returns((HttpContext?)null);

            // Act
            var result = await service.GetMeAsync();

            // Assert
            result.Should().BeNull();
        }
        [Theory]
        [AutoEntityData]
        public async Task GetMeAsync_WhenUserIsNotAuthorized_ReturnsCorrectUser(User user)
        {
            //Arrange
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext!.User.Claims).Returns(claims);

            // Act
            var result = await service.GetMeAsync();

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetByIdAsync_WhenUserExists_ReturnsValidResponse(UserDto user)
        {
            //Arrange
            User user1 = Mapper.Map<User>(user);
            await service.CreateAsync(user);

            // Act
            var result = await service.GetByIdAsync(user.Id);

            // Assert
            result.Id.Should().Be(user1.Id);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetByIdAsync_WhenUserIsNotExist_ThrowsException(int id)
        {
            //Arrange

            // Act
            Func<Task<UserDto>> func = () => service.GetByIdAsync(id);

            // Assert
            await func.Should().ThrowAsync<Exception>();
        }
        [Theory]
        [AutoEntityData]
        public async Task IsEmailExist_WhenUserIsNotExist_ReturnsFalse(User user)
        {
            //Arrange

            // Act
            var result = await service.IsEmailAlreadyExistsAsync(user.Email);

            // Assert
            result.Should().BeFalse();
        }
        [Theory]
        [AutoEntityData]
        public async Task IsEmailExist_WhenUserIsNotExist_ReturnsTrue(User user)
        {
            //Arrange
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            // Act
            var result = await service.IsEmailAlreadyExistsAsync(user.Email);

            // Assert
            result.Should().BeTrue();
        }
    }
}
