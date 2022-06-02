using Fragments.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fragments.Data.Context
{
    public class FragmentsContext : DbContext, IFragmentsContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ChannelsOfRefference> ChannelsOfRefferences { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            string connectionString = builder.Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                        .Property(x => x.RepresentativeAuthority)
                        .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                        .Property(x => x.RepresentativeHEI)
                        .HasDefaultValue(false);

            modelBuilder.Entity<ChannelsOfRefference>()
                        .HasKey(k => k.ChannelId);

            modelBuilder.Entity<User>()
                        .HasMany(c => c.ChannelsOfRefferences)
                        .WithOne(e => e.User)
                        .HasForeignKey(f => f.UserId);

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
