using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fragments.Data.Mapping
{
    public static class ChannelsMapping
    {
        public static void Map(EntityTypeBuilder<ChannelsOfRefference> entity)
        {
            entity.HasKey(k => k.ChannelId);
        }
    }
}
