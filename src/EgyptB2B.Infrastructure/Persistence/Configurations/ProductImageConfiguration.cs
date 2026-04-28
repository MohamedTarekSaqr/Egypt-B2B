using EgyptB2B.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(image => image.Id);

        builder.HasQueryFilter(image => image.Product != null && !image.Product.IsDeleted);

        builder.Property(image => image.Url)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(image => image.AltText)
            .HasMaxLength(200);

        builder.Property(image => image.CreatedAtUtc)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(image => image.Product)
            .WithMany(product => product.Images)
            .HasForeignKey(image => image.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(image => new { image.ProductId, image.IsPrimary })
            .IsUnique()
            .HasFilter("[IsPrimary] = 1");
    }
}
