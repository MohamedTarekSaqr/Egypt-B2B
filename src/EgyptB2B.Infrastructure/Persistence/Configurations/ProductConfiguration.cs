using EgyptB2B.Domain.Entities;
using EgyptB2B.Domain.Enums;
using EgyptB2B.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.HasQueryFilter(product => !product.IsDeleted);

        builder.Property(product => product.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(product => product.Slug)
            .HasMaxLength(220)
            .IsRequired();

        builder.Property(product => product.SKU)
            .HasMaxLength(100);

        builder.Property(product => product.Description)
            .IsRequired();

        builder.Property(product => product.Unit)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(product => product.MinimumOrderQuantity)
            .HasPrecision(18, 2);

        builder.Property(product => product.Price)
            .HasPrecision(18, 2);

        builder.Property(product => product.Currency)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasDefaultValue("EGP");

        builder.Property(product => product.StockQuantity)
            .HasPrecision(18, 2);

        builder.Property(product => product.Status)
            .HasConversion<byte>()
            .HasColumnType("tinyint");

        builder.Property(product => product.RejectionReason)
            .HasMaxLength(500);

        builder.HasOne(product => product.SupplierProfile)
            .WithMany(supplier => supplier.Products)
            .HasForeignKey(product => product.SupplierProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(product => product.ApprovedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(product => new { product.SupplierProfileId, product.Slug })
            .IsUnique();

        builder.HasIndex(product => new { product.SupplierProfileId, product.Status });
        builder.HasIndex(product => new { product.CategoryId, product.Status });
        builder.HasIndex(product => new { product.Status, product.CreatedAtUtc });
        builder.HasIndex(product => product.Name);
    }
}
