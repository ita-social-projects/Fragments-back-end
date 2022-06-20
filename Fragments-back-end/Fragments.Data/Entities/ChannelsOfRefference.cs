namespace Fragments.Data.Entities
{
    public class ChannelsOfRefference
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; } = null!;
        public string ChannelDetails { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
