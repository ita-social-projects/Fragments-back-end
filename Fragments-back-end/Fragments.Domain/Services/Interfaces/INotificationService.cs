using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationsDto> AddNotificationAsync(NotificationsDto notification);
        Task<bool> DeleteNotificationAsync(int notificationId);
        Task ReadingTheMessage(NotificationsDto NotificationsDto);
        Task<IReadOnlyList<NotificationsDto>> GetNotificationsAsync(bool sortingBy, bool typeOfRead);
    }
}
