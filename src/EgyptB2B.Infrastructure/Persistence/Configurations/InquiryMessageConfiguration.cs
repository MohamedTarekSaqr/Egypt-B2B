using EgyptB2B.Domain.Entities;
using EgyptB2B.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class InquiryMessageConfiguration : IEntityTypeConfiguration<InquiryMessage>
{
    public void Configure(EntityTypeBuilder<InquiryMessage> builder)
    {
        builder.ToTable("InquiryMessages");

        builder.HasKey(message => message.Id);

        builder.Property(message => message.Message)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(message => message.CreatedAtUtc)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(message => message.IsRead)
            .HasDefaultValue(false);

        builder.HasOne(message => message.Inquiry)
            .WithMany(inquiry => inquiry.Messages)
            .HasForeignKey(message => message.InquiryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(message => message.SenderUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(message => new { message.InquiryId, message.CreatedAtUtc });
    }
}
