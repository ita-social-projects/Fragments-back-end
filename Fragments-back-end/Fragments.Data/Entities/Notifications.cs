namespace Fragments.Data.Entities
{
    public class Notifications
    {
        public int NotificationId { get; set; }
        public string Theme { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; } = false;
        public virtual User User { get; set; } = null!;
    }
}