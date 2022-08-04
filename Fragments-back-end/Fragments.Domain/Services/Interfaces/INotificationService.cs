using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationsDTO> AddNotificationAsync(NotificationsDTO notification);
        Task<bool> DeleteNotificationAsync(int notificationId);
        Task ReadingTheMessage(NotificationsDto notificationsDTO);
        Task<IReadOnlyList<NotificationsDto>> GetNotificationsAsync(bool sortingBy, bool typeOfRead);
    }
}
