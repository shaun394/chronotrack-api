using ChronoTrack.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public sealed class ClientConfiguration
        : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.WorkspaceId)
                .HasColumnName("workspace_id")
                .IsRequired();

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

            builder.HasIndex(x => x.WorkspaceId)
                .HasDatabaseName("ix_clients_workspace_id");

            builder.HasIndex(x => new { x.WorkspaceId, x.Name })
                .HasDatabaseName("ix_clients_workspace_id_name");
        }
    }
}