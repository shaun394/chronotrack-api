using ChronoTrack.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public sealed class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
    {
        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.ToTable("workspaces");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
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

            builder.HasIndex(x => x.Name)
                .HasDatabaseName("ix_workspaces_name");
        }
    }
}