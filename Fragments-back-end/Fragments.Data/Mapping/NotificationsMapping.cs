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

            //entity.Property(x => x.Body)
            //    .HasDefaultValue("Вітаємо у Спільноті Fragmenty! " +
            //    "Ви можете підтримати платформу, створити проект або долучитись до існуючих проектів");

            //entity.Property(x => x.Theme)
            //    .HasDefaultValue("Вітання у Fragmenty");

            entity.Property(x => x.Date)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
