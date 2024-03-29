﻿namespace Fragments.Domain.Dto
{
    public class ChannelsOfRefferenceDto
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; } = null!;
        public string ChannelDetails { get; set; } = null!;
        public int UserId { get; set; } 
    }
}
