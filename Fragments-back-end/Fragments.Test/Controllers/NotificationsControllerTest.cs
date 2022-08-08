using FluentAssertions.Execution;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Fragments.Test.Base;
using Fragments.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Fragments.Test.Controllers
{
    public class NotificationsControllerTest
    {
        private readonly Mock<INotificationService> notificationService;
        private readonly NotificationsController userController;

        public NotificationsControllerTest()
        {
            notificationService = new Mock<INotificationService>();
            userController = new NotificationsController(notificationService.Object);
        }
        [Theory]
        [AutoEntityData]
        public async Task CreateAsync_Returns_CorrectValue(NotificationsDto notifaction)
        {
            //Arrange
            notificationService.Setup(service => service.AddNotificationAsync(notifaction)).ReturnsAsync(notifaction);

            //Act
            var result = await userController.CreateAsync(notifaction);
            //Assert
            using(new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                ((OkObjectResult)result).Value.Should().Be(notifaction);
            }
            
        }
        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_Returns_OkResult_IfTrue(int id)
        {
            //Arrange
            notificationService.Setup(service => service.DeleteNotificationAsync(id)).ReturnsAsync(true);

            //Act
            var result = await userController.DeleteAsync(id);
            //Assert
            result.Should().BeOfType<OkResult>();

        }
        [Theory]
        [AutoEntityData]
        public async Task DeleteAsync_Returns_ForbidResult_IfFalse(int id)
        {
            //Arrange
            notificationService.Setup(service => service.DeleteNotificationAsync(id)).ReturnsAsync(false);

            //Act
            var result = await userController.DeleteAsync(id);
            //Assert
            result.Should().BeOfType<ForbidResult>();
        }
        [Theory]
        [AutoEntityData]
        public async Task ReadingTheMessage_Returns_OkObjectResult(NotificationsDto notifaction)
        {
            //Arrange

            //Act
            var result = await userController.ReadingTheMessage(notifaction);
            //Assert
            notificationService.Verify(service => service.ReadingTheMessage(notifaction));
        }
        [Theory]
        [AutoEntityData]
        public async Task GetNotificationsWithCorrectUser_Returns_OkObjectResult(bool sortingBy, bool typeOfRead, IReadOnlyList<NotificationsDto> notifications)
        {
            //Arrange
            notificationService.Setup(service => service.GetNotificationsAsync(sortingBy, typeOfRead)).ReturnsAsync(notifications);

            //Act
            var result = await userController.GetNotificationsWithCorrectUser(sortingBy, typeOfRead);

            //Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                ((OkObjectResult)result).Value.Should().Be(notifications);
            }
        }
    }
}
