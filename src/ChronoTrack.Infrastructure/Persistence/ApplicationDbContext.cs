using ChronoTrack.Domain.Clients;
using ChronoTrack.Domain.Projects;
using ChronoTrack.Domain.Tags;
using ChronoTrack.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrack.Infrastructure.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Workspace> Workspaces => Set<Workspace>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Tag> Tags => Set<Tag>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}