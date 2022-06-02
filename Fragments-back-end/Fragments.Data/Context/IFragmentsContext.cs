using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Fragments.Data.Context
{
    public interface IFragmentsContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChannelsOfRefference> ChannelsOfRefferences { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
