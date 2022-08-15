using Fragments.Data.Entities;
using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationsDTO> AddNotificationAsync(NotificationsDTO notification);

        Task<bool> DeleteNotificationAsync(int notificationId);

        Task<IReadOnlyList<NotificationsDTO>> GetNotificationsAsync();
    }
}
