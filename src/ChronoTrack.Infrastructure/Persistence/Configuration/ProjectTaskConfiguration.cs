using ChronoTrack.Domain.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public sealed class ProjectTaskConfiguration
        : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable("project_tasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

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

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.RemovedAt)
                .HasColumnName("removed_at")
                .IsRequired(false);

            builder.Property(x => x.RemovedBy)
                .HasColumnName("removed_by")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.HasIndex(x => x.ProjectId)
                .HasDatabaseName("ix_project_tasks_project_id");

            builder.HasIndex(x => new { x.ProjectId, x.Name })
                .HasDatabaseName("ix_project_tasks_project_id_name");
        }
    }
}