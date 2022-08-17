using FluentAssertions.Execution;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Hubs;
using Fragments.Domain.Services.Implementation;
using Fragments.Domain.Services.Interfaces;
using Fragments.Test.Base;
using Microsoft.AspNetCore.SignalR;

namespace Fragments.Test.Services
{
    public class NotificationServiceTest : Base.Base
    {
        private readonly FragmentsContext context;
        private readonly Mock<IHubContext<NotificationsHub>> hub;
        private readonly Mock<IUserService> userService;
        private readonly INotificationService service;

        public NotificationServiceTest()
        {
            context = ContextGenerator.GetContext();
            hub = new Mock<IHubContext<NotificationsHub>>();
            userService = new Mock<IUserService>();
            service = new NotificationService(context, Mapper, hub.Object, userService.Object);
        }
        [Theory]
        [AutoEntityData]
        public async Task AddNotificationAsync_AddsNotificationToDb(NotificationsDto notfication)
        {
            //Arrange
            var user = new UserDto { Id = notfication.UserId, Email = "email", FullName = "name", Photo = "" };
            userService.Setup(service => service.GetMeAsync()).ReturnsAsync(user);
            hub.Setup(hub => hub.Clients.Group(user.Id.ToString())).Returns(Mock.Of<IClientProxy>());

            //Act
            var result = await service.AddNotificationAsync(notfication);
            var dbResult = await context.Notifications.FindAsync(notfication.NotificationId);
            //Assert
            using(new AssertionScope()) 
            {
                result.Should().BeOfType<NotificationsDto>();
                dbResult.Should().NotBeNull();
            }    
        }
        [Theory]
        [AutoEntityData]
        public async Task DeleteNotification_DeletesFromDbById_ReturnsTrue(int id)
        {
            //Arrange
            userService.Setup(service => service.GetMeAsync()).ReturnsAsync(new UserDto { Id = id });
            var notification = new Notifications { NotificationId = id , Theme = "test", Body = "test", UserId = id };
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
            //Act

            var result = await service.DeleteNotificationAsync(id);

            //Assert
            result.Should().BeTrue();

        }
        [Theory]
        [AutoEntityData]
        public async Task DeleteNotification_DeletesFromDbById_ReturnsFalse(int id)
        {
            //Arrange
            userService.Setup(service => service.GetMeAsync()).ReturnsAsync(new UserDto { Id = id+1 });
            var notification = new Notifications { NotificationId = id, Theme = "test", Body = "test", UserId = id };
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
            //Act

            var result = await service.DeleteNotificationAsync(id);

            //Assert
            result.Should().BeFalse();

        }
        [Theory]
        [AutoEntityData]
        public async Task ReadingTheMessage_ChangeNotificationValueCorrect(NotificationsDto notificationsDto)
        {
            //Arrange
            notificationsDto.IsRead = false;
            var notification = Mapper.Map<Notifications>(notificationsDto);
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();

            //Act
            await service.ReadingTheMessage(notificationsDto);

            //Assert
            notification.IsRead.Should().BeTrue();

        }
        [Theory]
        [AutoEntityData]
        public async Task GetNotificationsAsync_Returns_ReadNotificationsOfUser(bool sortingBy, UserDto user )
        {
            //Arrange
            bool typeOfRead = true;
            userService.Setup(service => service.GetMeAsync()).ReturnsAsync(user);
            IEnumerable<Notifications> notifications = new List<Notifications>
            {
               new Notifications
               {
                   Theme = "test1",
                   Body = "test1",
                   UserId = user.Id,
                   IsRead = typeOfRead,
               },
               new Notifications
               {
                   Theme = "test2",
                   Body = "test2",
                   UserId = user.Id,
                   IsRead = typeOfRead,

               },
               new Notifications
               {
                   Theme = "test3",
                   Body = "test3",
                   UserId = user.Id,
                   IsRead = typeOfRead,
               },
            };
            await context.Notifications.AddRangeAsync(notifications);
            await context.SaveChangesAsync();

            //Act
            var result = await service.GetNotificationsAsync(sortingBy, typeOfRead);

            //Assert
            result.Should().NotBeEmpty();

        }
        [Theory]
        [AutoEntityData]
        public async Task GetNotificationsAsync_Returns_UnreadNotificationsOfUser(bool sortingBy, UserDto user)
        {
            //Arrange
            bool typeOfRead = false;
            userService.Setup(service => service.GetMeAsync()).ReturnsAsync(user);
            IEnumerable<Notifications> notifications = new List<Notifications>
            {
               new Notifications
               {
                   Theme = "test4",
                   Body = "test4",
                   UserId = user.Id,
                   IsRead = typeOfRead,
               },
               new Notifications
               {
                   Theme = "test5",
                   Body = "test5",
                   UserId = user.Id,
                   IsRead = typeOfRead,

               },
            };
            await context.Notifications.AddRangeAsync(notifications);
            await context.SaveChangesAsync();

            //Act
            var result = await service.GetNotificationsAsync(sortingBy, typeOfRead);

            //Assert
            result.Should().NotBeEmpty();

        }
    }
}
