using ChronoTrack.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public sealed class ClientConfiguration
        : AuditableEntityConfiguration<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);

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

            builder.HasIndex(x => x.WorkspaceId)
                .HasDatabaseName("ix_clients_workspace_id");

            builder.HasIndex(x => new { x.WorkspaceId, x.Name })
                .HasDatabaseName("ix_clients_workspace_id_name");
        }
    }
}