using Fragments.Domain.Services.Interfaces;
using Fragments.Domain.Dto;
using AutoMapper;
using Fragments.Data.Entities;
using Fragments.Data.Context;

namespace Fragments.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IFragmentsContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public NotificationService(
            IFragmentsContext context,
            IMapper mapper,
            IUserService userService
            )
            
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task ReadingTheMessage(NotificationsDTO notificationsDTO)
        {
            var notification = await _context.Notifications.FindAsync(notificationsDTO.NotificationId);
            var notificationInfo = _mapper.Map<Notifications>(notification);
            notificationInfo.IsRead = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<NotificationsDTO>> GetNotificationsAsync(bool sortingByDateDescending, bool typeOfRead)
        {
            var user = await _userService.GetMe();
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
            notifications = sortingByDateDescending
                ?  notifications.OrderByDescending(u => u.Date) 
                :  notifications.OrderBy(u => u.Date);

            return _mapper.Map<IReadOnlyList<Notifications>, IReadOnlyList<NotificationsDTO>>(notifications.ToList());
        }
    }
}
