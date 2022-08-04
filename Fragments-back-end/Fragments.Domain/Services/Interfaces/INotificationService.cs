using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Task ReadingTheMessage(NotificationsDto notificationsDTO);
        Task<IReadOnlyList<NotificationsDto>> GetNotificationsAsync(bool sortingBy, bool typeOfRead);

    }
}
