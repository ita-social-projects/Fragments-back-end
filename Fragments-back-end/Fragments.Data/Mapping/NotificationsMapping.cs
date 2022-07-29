using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fragments.Data.Mapping
{
    public static class NotificationsMapping
    {
        public static void Map(EntityTypeBuilder<Notifications> entity)
        {
            entity.HasKey(k => k.NotificationId);

            entity.Property(x => x.Date)
                .HasDefaultValue(DateTime.UtcNow);
        }
    }
}