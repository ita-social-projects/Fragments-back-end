using Fragments.Data.Entities;
using Fragments.Domain.Dto;

namespace Fragments.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Task ReadingTheMessage(NotificationsDTO notificationsDTO);
        Task<IReadOnlyList<NotificationsDTO>> GetNotificationsAsync(bool sortingBy, int pageIndex, bool typeOfRead);
    }
}
