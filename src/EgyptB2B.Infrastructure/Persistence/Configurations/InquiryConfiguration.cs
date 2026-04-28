using EgyptB2B.Domain.Entities;
using EgyptB2B.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
{
    public void Configure(EntityTypeBuilder<Inquiry> builder)
    {
        builder.ToTable("Inquiries");

        builder.HasKey(inquiry => inquiry.Id);

        builder.Property(inquiry => inquiry.Subject)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(inquiry => inquiry.Message)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(inquiry => inquiry.Quantity)
            .HasPrecision(18, 2);

        builder.Property(inquiry => inquiry.Unit)
            .HasMaxLength(50);

        builder.Property(inquiry => inquiry.Status)
            .HasConversion<byte>()
            .HasColumnType("tinyint");

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(inquiry => inquiry.BuyerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(inquiry => inquiry.SupplierProfile)
            .WithMany(supplier => supplier.Inquiries)
            .HasForeignKey(inquiry => inquiry.SupplierProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(inquiry => inquiry.Product)
            .WithMany(product => product.Inquiries)
            .HasForeignKey(inquiry => inquiry.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(inquiry => new { inquiry.BuyerUserId, inquiry.CreatedAtUtc });
        builder.HasIndex(inquiry => new { inquiry.SupplierProfileId, inquiry.Status });
    }
}
