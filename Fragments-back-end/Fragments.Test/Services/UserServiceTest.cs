using FluentAssertions;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Helpers;
using Fragments.Domain.Services;
using Fragments.Test.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.QualityTools.Testing.Fakes;
using Moq;

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
        public async Task CreateAsync_WhenUserIsValid_AddsToDb(UserDTO user)
        {
            //Arrange
            User user1 = Mapper.Map<User>(user);

            // Act
            await service.CreateAsync(user);
            var result = await context.Users.FindAsync(user.Id);

            // Assert
            result.Should().BeEquivalentTo(user1);
        }

        [Fact]
        public async Task GetMe_WhenUserIsNotAuthorized_ReturnsNull()
        {
            //Arrange
            httpContextAccessor.SetupGet(x => x.HttpContext).Returns((HttpContext?)null);

            // Act
            var result = await service.GetMe();

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetByIdAsync_WhenUserExists_ReturnsValidResponse(UserDTO user)
        {
            //Arrange
            User user1 = Mapper.Map<User>(user);
            await service.CreateAsync(user);

            // Act
            var result = await service.GetByIdAsync(user.Id);

            // Assert
            result?.Should().BeEquivalentTo(user1);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetByIdAsync_WhenUserIsNotExist_ThrowsException(int id)
        {
            //Arrange

            // Act
            Func<Task<UserDTO>> func = () => service.GetByIdAsync(id);

            // Assert
            await func.Should().ThrowAsync<Exception>().WithMessage("Not Found");
        }
    }
}
