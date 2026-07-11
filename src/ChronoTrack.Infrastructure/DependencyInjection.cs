using ChronoTrack.Application.Common.Interfaces;
using ChronoTrack.Application.Interfaces.Repositories.Clients;
using ChronoTrack.Application.Interfaces.Repositories.Projects;
using ChronoTrack.Application.Interfaces.Repositories.Reports;
using ChronoTrack.Application.Interfaces.Repositories.Tags;
using ChronoTrack.Application.Interfaces.Repositories.Tasks;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntries;
using ChronoTrack.Application.Interfaces.Repositories.TimeEntryTags;
using ChronoTrack.Application.Interfaces.Repositories.Workspaces;
using ChronoTrack.Infrastructure.EventStore;
using ChronoTrack.Infrastructure.Persistence;
using ChronoTrack.Infrastructure.Persistence.Projections;
using ChronoTrack.Infrastructure.Persistence.Repositories.Clients;
using ChronoTrack.Infrastructure.Persistence.Repositories.Projects;
using ChronoTrack.Infrastructure.Persistence.Repositories.Reports;
using ChronoTrack.Infrastructure.Persistence.Repositories.Tags;
using ChronoTrack.Infrastructure.Persistence.Repositories.Tasks;
using ChronoTrack.Infrastructure.Persistence.Repositories.TimeEntries;
using ChronoTrack.Infrastructure.Persistence.Repositories.TimeEntryTags;
using ChronoTrack.Infrastructure.Persistence.Repositories.Workspaces;
using JasperFx;
using JasperFx.Events.Projections;
using Marten;
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
            services.AddScoped<IEventStore, MartenEventStore>();

            services.AddScoped<IWorkspaceWriteRepository, WorkspaceWriteRepository>();
            services.AddScoped<IWorkspaceReadRepository, WorkspaceReadRepository>();
            services.AddScoped<IClientWriteRepository, ClientWriteRepository>();
            services.AddScoped<IClientReadRepository, ClientReadRepository>();
            services.AddScoped<IProjectWriteRepository, ProjectWriteRepository>();
            services.AddScoped<IProjectReadRepository, ProjectReadRepository>();
            services.AddScoped<ITagWriteRepository, TagWriteRepository>();
            services.AddScoped<ITagReadRepository, TagReadRepository>();
            services.AddScoped<IProjectTaskWriteRepository, ProjectTaskWriteRepository>();
            services.AddScoped<IProjectTaskReadRepository, ProjectTaskReadRepository>();
            services.AddScoped<ITimeEntryWriteRepository, TimeEntryWriteRepository>();
            services.AddScoped<ITimeEntryReadRepository, TimeEntryReadRepository>();
            services.AddScoped<ITimeEntryTagWriteRepository, TimeEntryTagWriteRepository>();
            services.AddScoped<ITimeEntryTagReadRepository, TimeEntryTagReadRepository>();
            services.AddScoped<IReportReadRepository, ReportReadRepository>();

            services
                .AddMarten(options =>
                {
                    options.Connection(connectionString);

                    options.AutoCreateSchemaObjects = AutoCreate.All;
                    options.Events.StreamIdentity = JasperFx.Events.StreamIdentity.AsString;
            
                    options.Projections.Add<WorkspaceAuditProjection>(ProjectionLifecycle.Inline);
                })
                .UseLightweightSessions();


            return services;
        }
    }
}