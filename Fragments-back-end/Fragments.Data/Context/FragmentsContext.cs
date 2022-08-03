using Fragments.Data.Entities;
using Fragments.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Fragments.Data.Context
{
    public class FragmentsContext : DbContext, IFragmentsContext
    {
        public FragmentsContext(DbContextOptions<FragmentsContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ChannelsOfRefference> ChannelsOfRefferences { get; set; } = null!;
        public DbSet<Notifications> Notifications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                string connectionString = builder.Build().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(UserMapping.Map);

            modelBuilder.Entity<ChannelsOfRefference>(ChannelsMapping.Map);

            modelBuilder.Entity<Notifications>(NotificationsMapping.Map);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }
    }
}
