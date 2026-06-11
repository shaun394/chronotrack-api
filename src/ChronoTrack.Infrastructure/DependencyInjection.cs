using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Infrastructure.Persistence;
using ChronoTrack.Infrastructure.Persistence.Repositories.Clients;
using ChronoTrack.Infrastructure.Persistence.Repositories.Projects;
using ChronoTrack.Infrastructure.Persistence.Repositories.Tags;
using ChronoTrack.Infrastructure.Persistence.Repositories.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoTrack.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection connection string is not configured.");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWorkspaceWriteRepository, WorkspaceWriteRepository>();
            services.AddScoped<IWorkspaceReadRepository, WorkspaceReadRepository>();
            services.AddScoped<IClientWriteRepository, ClientWriteRepository>();
            services.AddScoped<IClientReadRepository, ClientReadRepository>();
            services.AddScoped<IProjectWriteRepository, ProjectWriteRepository>();
            services.AddScoped<IProjectReadRepository, ProjectReadRepository>();
            services.AddScoped<ITagWriteRepository, TagWriteRepository>();
            services.AddScoped<ITagReadRepository, TagReadRepository>();

            return services;
        }
    }
}