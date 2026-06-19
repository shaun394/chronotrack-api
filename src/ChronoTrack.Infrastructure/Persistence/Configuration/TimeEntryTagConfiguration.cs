using ChronoTrack.Domain.TimeEntryTags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public sealed class TimeEntryTagConfiguration
        : AuditableEntityConfiguration<TimeEntryTag>
    {
        public override void Configure(EntityTypeBuilder<TimeEntryTag> builder)
        {
            base.Configure(builder);

            builder.ToTable("time_entry_tags");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.WorkspaceId)
                .HasColumnName("workspace_id")
                .IsRequired();

            builder.Property(x => x.TimeEntryId)
                .HasColumnName("time_entry_id")
                .IsRequired();

            builder.Property(x => x.TagId)
                .HasColumnName("tag_id")
                .IsRequired();

            builder.HasIndex(x => x.WorkspaceId)
                .HasDatabaseName("ix_time_entry_tags_workspace_id");

            builder.HasIndex(x => x.TimeEntryId)
                .HasDatabaseName("ix_time_entry_tags_time_entry_id");

            builder.HasIndex(x => x.TagId)
                .HasDatabaseName("ix_time_entry_tags_tag_id");

            builder.HasIndex(x => new { x.TimeEntryId, x.TagId })
                .IsUnique()
                .HasDatabaseName("ix_time_entry_tags_time_entry_id_tag_id");
        }
    }
}