using EgyptB2B.Domain.Entities;
using EgyptB2B.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class ApprovalAuditLogConfiguration : IEntityTypeConfiguration<ApprovalAuditLog>
{
    public void Configure(EntityTypeBuilder<ApprovalAuditLog> builder)
    {
        builder.ToTable("ApprovalAuditLogs");

        builder.HasKey(log => log.Id);

        builder.Property(log => log.EntityType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(log => log.Action)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(log => log.Reason)
            .HasMaxLength(500);

        builder.Property(log => log.CreatedAtUtc)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(log => log.AdminUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(log => log.AdminUserId);
        builder.HasIndex(log => new { log.EntityType, log.EntityId });
    }
}
