using EgyptB2B.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(category => category.NameAr)
            .HasMaxLength(150);

        builder.Property(category => category.Slug)
            .HasMaxLength(180)
            .IsRequired();

        builder.Property(category => category.Description)
            .HasMaxLength(1000);

        builder.Property(category => category.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(category => category.ParentCategory)
            .WithMany(category => category.Children)
            .HasForeignKey(category => category.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(category => category.Slug)
            .IsUnique();

        builder.HasIndex(category => category.ParentCategoryId);
    }
}
