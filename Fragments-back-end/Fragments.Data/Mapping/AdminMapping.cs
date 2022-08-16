using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fragments.Data.Mapping
{
    static class AdminMapper
    {
        public static void Map(EntityTypeBuilder<User> entity)
        {
            entity.Property(x => x.RepresentativeHEI)
                  .HasDefaultValue(false);

            entity.HasMany(c => c.UsersRole)
                  .WithOne(e => e.User)
                  .HasForeignKey(f => f.UserId);

            entity.Property(x => x.RepresentativeAuthority)
                  .HasDefaultValue(false);
        }
    }
}
