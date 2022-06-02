using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Data.Entities
{
    public class ChannelsOfRefference
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; } = null!;
        public string ChannelDetails { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
