using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fragments.Data.Context
{
    public interface IFragmentsContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChannelsOfRefference> ChannelsOfRefferences { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UsersRole> UsersRoles { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        //
        public DbSet<Notifications> Notifications { get; set; }
        //
        public EntityEntry Entry(object entity);
    }
}
