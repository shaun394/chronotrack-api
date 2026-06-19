using ChronoTrack.Domain.TimeEntries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public sealed class TimeEntryConfiguration
        : AuditableEntityConfiguration<TimeEntry>
    {
        public override void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            base.Configure(builder);

            builder.ToTable("time_entries");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.WorkspaceId)
                .HasColumnName("workspace_id")
                .IsRequired();

            builder.Property(x => x.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();

            builder.Property(x => x.ProjectTaskId)
                .HasColumnName("project_task_id")
                .IsRequired(false);

            builder.Property(x => x.ClientId)
                .HasColumnName("client_id")
                .IsRequired(false);

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.WorkDate)
                .HasColumnName("work_date")
                .IsRequired();

            builder.Property(x => x.StartTime)
                .HasColumnName("start_time")
                .IsRequired();

            builder.Property(x => x.EndTime)
                .HasColumnName("end_time")
                .IsRequired();

            builder.Property(x => x.DurationMinutes)
                .HasColumnName("duration_minutes")
                .IsRequired();

            builder.Property(x => x.IsBillable)
                .HasColumnName("is_billable")
                .IsRequired();

            builder.HasIndex(x => x.WorkspaceId)
                .HasDatabaseName("ix_time_entries_workspace_id");

            builder.HasIndex(x => x.ProjectId)
                .HasDatabaseName("ix_time_entries_project_id");

            builder.HasIndex(x => x.ProjectTaskId)
                .HasDatabaseName("ix_time_entries_project_task_id");

            builder.HasIndex(x => x.ClientId)
                .HasDatabaseName("ix_time_entries_client_id");

            builder.HasIndex(x => x.WorkDate)
                .HasDatabaseName("ix_time_entries_work_date");

            builder.HasIndex(x => new { x.WorkspaceId, x.WorkDate })
                .HasDatabaseName("ix_time_entries_workspace_id_work_date");
        }
    }
}