using EgyptB2B.Domain.Entities;
using EgyptB2B.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class SupplierProfileConfiguration : IEntityTypeConfiguration<SupplierProfile>
{
    public void Configure(EntityTypeBuilder<SupplierProfile> builder)
    {
        builder.ToTable("SupplierProfiles");

        builder.HasKey(supplier => supplier.Id);

        builder.Property(supplier => supplier.CompanyName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(supplier => supplier.CompanyDescription)
            .HasMaxLength(2000);

        builder.Property(supplier => supplier.CommercialRegistrationNumber)
            .HasMaxLength(100);

        builder.Property(supplier => supplier.TaxNumber)
            .HasMaxLength(100);

        builder.Property(supplier => supplier.Website)
            .HasMaxLength(300);

        builder.Property(supplier => supplier.LogoUrl)
            .HasMaxLength(500);

        builder.Property(supplier => supplier.ContactPersonName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(supplier => supplier.ContactPhone)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(supplier => supplier.ApprovalStatus)
            .HasConversion<byte>()
            .HasColumnType("tinyint");

        builder.Property(supplier => supplier.RejectionReason)
            .HasMaxLength(500);

        builder.OwnsOne(supplier => supplier.Address, address =>
        {
            address.Property(value => value.Governorate)
                .HasColumnName("Governorate")
                .HasMaxLength(100)
                .IsRequired();

            address.Property(value => value.City)
                .HasColumnName("City")
                .HasMaxLength(100)
                .IsRequired();

            address.Property(value => value.AddressLine)
                .HasColumnName("AddressLine")
                .HasMaxLength(300)
                .IsRequired();
        });

        builder.Navigation(supplier => supplier.Address)
            .IsRequired();

        builder.HasOne<ApplicationUser>()
            .WithOne(user => user.SupplierProfile)
            .HasForeignKey<SupplierProfile>(supplier => supplier.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(supplier => supplier.ApprovedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(supplier => supplier.UserId)
            .IsUnique();

        builder.HasIndex(supplier => supplier.ApprovalStatus);

        builder.HasIndex(supplier => supplier.CompanyName);

        builder.HasIndex(supplier => supplier.CommercialRegistrationNumber)
            .IsUnique()
            .HasFilter("[CommercialRegistrationNumber] IS NOT NULL");
    }
}
