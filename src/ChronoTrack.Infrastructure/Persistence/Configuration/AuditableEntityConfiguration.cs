using ChronoTrack.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoTrack.Infrastructure.Persistence.Configuration
{
    public abstract class AuditableEntityConfiguration<TEntity>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ModifiedAt)
                .HasColumnName("modified_at")
                .IsRequired();

            builder.Property(x => x.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.RemovedAt)
                .HasColumnName("removed_at")
                .IsRequired(false);

            builder.Property(x => x.RemovedBy)
                .HasColumnName("removed_by")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.RestoredAt)
                .HasColumnName("restored_at")
                .IsRequired(false);

            builder.Property(x => x.RestoredBy)
                .HasColumnName("restored_by")
                .HasMaxLength(100)
                .IsRequired(false);
        }
    }
}