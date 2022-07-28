using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fragments.Data.Mapping
{
    public static class RoleMapping
    {
        public static void Map(EntityTypeBuilder<Role> entity)
        {
            entity.HasKey(k => k.RoleId);
        }
    }
}
