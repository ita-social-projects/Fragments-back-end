using AutoMapper;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Hubs;
using Fragments.Domain.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Fragments.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationsHub> _hub;

        private readonly IUserService _userService;
        public NotificationService(IFragmentsContext context, IMapper mapper, IHubContext<NotificationsHub> hub, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _hub = hub;
            _userService = userService;
        }
        public async Task<NotificationsDto> AddNotificationAsync(NotificationsDto notification)
        {
            var addNotification = _mapper.Map<Notifications>(notification);
            await _context.Notifications.AddAsync(addNotification);
            await _context.SaveChangesAsync();

            if (notification.UserId == (await _userService.GetMeAsync()).Id)
            {
                await NotifyUserAsync("New notification!");
            }

            return notification;
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(x => x.NotificationId == notificationId);

            if (notification != null)
            {
                var user = await _userService.GetMeAsync();
                if (user.Id != notification.UserId)
                {
                    return false;
                }

                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
            return true;
        }
        private async Task NotifyUserAsync(string message)
        {
            var groupName = (await _userService.GetMeAsync()).Id.ToString();
            await _hub.Clients.Group(groupName).SendAsync("createNotify", message);
        }
        public async Task ReadingTheMessage(NotificationsDto NotificationsDto)
        {
            var notification = await _context.Notifications.FindAsync(NotificationsDto.NotificationId);
            var notificationInfo = _mapper.Map<Notifications>(notification);
            notificationInfo.IsRead = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<NotificationsDto>> GetNotificationsAsync(bool sortingBy, bool typeOfRead)
        {
            var user = await _userService.GetMeAsync(); 
            var notifications = _context.Notifications
                .Where(x => x.UserId == user.Id)
                .Where(y => typeOfRead ? (y.IsRead || !y.IsRead ) : !y.IsRead)
                .Select(y => new Notifications
                {
                    UserId = y.UserId,
                    NotificationId = y.NotificationId,
                    Date = y.Date,
                    IsRead = y.IsRead,
                    Theme = y.Theme,
                    Body = y.Body
                });
            notifications = sortingBy
                ?  notifications.OrderByDescending(u => u.Date) 
                :  notifications.OrderBy(u => u.Date);

            return _mapper.Map<IReadOnlyList<Notifications>, IReadOnlyList<NotificationsDto>>(notifications.ToList());
        }
    }
}
