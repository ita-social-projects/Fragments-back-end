using Fragments.Domain.Services.Interfaces;
using Fragments.Domain.Dto;
using AutoMapper;
using Fragments.Data.Entities;
using Fragments.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Fragments.Domain.Helpers;

namespace Fragments.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public NotificationService(
            IFragmentsContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService
            )
            
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public async Task ReadingTheMessage(NotificationsDTO notificationsDTO)
        {
            var notification = await _context.Notifications.FindAsync(notificationsDTO.NotificationId);
            if (notification is not null)
            {
                notification.IsRead = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<NotificationsDTO>> GetNotificationsAsync(bool sortingByDateDescending, int pageIndex, bool typeOfRead)
        {
            var user = await _userService.GetMe();
            var notifications = _context.Notifications
                .Where(x => x.UserId == user.Id)
                .Where(y => typeOfRead == true ? (y.IsRead || !y.IsRead ) : !y.IsRead)
                .Select(y => new Notifications
                {
                    UserId = y.UserId,
                    NotificationId = y.NotificationId,
                    Date = y.Date,
                    IsRead = y.IsRead,
                    Theme = y.Theme,
                    Body = y.Body
                });

            if (sortingByDateDescending == false)
                notifications = notifications.OrderBy(u => u.Date);
            else
                notifications = notifications.OrderByDescending(u => u.Date);

            var notificationInfo = await PaginatedList<Notifications>.CreateAsync(notifications, pageIndex);

            return _mapper.Map<IReadOnlyList<Notifications>, IReadOnlyList<NotificationsDTO>>(notificationInfo);
        }
    }
}
