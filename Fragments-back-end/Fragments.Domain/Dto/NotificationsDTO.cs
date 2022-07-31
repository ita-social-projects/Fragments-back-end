namespace Fragments.Domain.Dto
{
    public class NotificationsDTO
    {
        public int NotificationId { get; set; }
        public string Theme { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool IsRead { get; set; } = false;
        public int UserId { get; set; }
    }
}