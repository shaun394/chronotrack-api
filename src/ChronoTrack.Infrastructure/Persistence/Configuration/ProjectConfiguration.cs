using ChronoTrack.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public sealed class ProjectConfiguration
        : AuditableEntityConfiguration<Project>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);

            builder.ToTable("projects");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.WorkspaceId)
                .HasColumnName("workspace_id")
                .IsRequired();

            builder.Property(x => x.ClientId)
                .HasColumnName("client_id")
                .IsRequired(false);

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.IsBillable)
                .HasColumnName("is_billable")
                .IsRequired();

            builder.HasIndex(x => x.WorkspaceId)
                .HasDatabaseName("ix_projects_workspace_id");

            builder.HasIndex(x => x.ClientId)
                .HasDatabaseName("ix_projects_client_id");

            builder.HasIndex(x => new { x.WorkspaceId, x.Name })
                .HasDatabaseName("ix_projects_workspace_id_name");
        }
    }
}