using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fragments.Data.Mapping
{
    static class UserMapping
    {
        public static void Map(EntityTypeBuilder<User> entity)
        {
            entity.HasMany(c => c.ChannelsOfRefferences)
                  .WithOne(e => e.User)
                  .HasForeignKey(f => f.UserId);

            entity.Property(x => x.RepresentativeHEI)
                  .HasDefaultValue(false);

            entity.Property(x => x.RepresentativeAuthority)
                  .HasDefaultValue(false);
        }
    }
}
