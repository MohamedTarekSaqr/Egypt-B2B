using EgyptB2B.Domain.Common;
using EgyptB2B.Domain.Enums;

namespace EgyptB2B.Domain.Entities;

public sealed class Product : SoftDeletableEntity
{
    private Product()
    {
    }

    public Product(
        Guid supplierProfileId,
        Guid categoryId,
        string name,
        string slug,
        string description,
        string unit,
        decimal minimumOrderQuantity)
    {
        if (supplierProfileId == Guid.Empty)
        {
            throw new ArgumentException("Supplier profile id is required.", nameof(supplierProfileId));
        }

        if (categoryId == Guid.Empty)
        {
            throw new ArgumentException("Category id is required.", nameof(categoryId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(slug);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(unit);

        if (minimumOrderQuantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumOrderQuantity), "Minimum order quantity must be greater than zero.");
        }

        SupplierProfileId = supplierProfileId;
        CategoryId = categoryId;
        Name = name;
        Slug = slug;
        Description = description;
        Unit = unit;
        MinimumOrderQuantity = minimumOrderQuantity;
        Currency = "EGP";
        Status = ProductStatus.Draft;
    }

    public Guid SupplierProfileId { get; private set; }

    public SupplierProfile? SupplierProfile { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category? Category { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public string? SKU { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public string Unit { get; private set; } = string.Empty;

    public decimal MinimumOrderQuantity { get; private set; }

    public decimal? Price { get; private set; }

    public string Currency { get; private set; } = "EGP";

    public bool IsPriceVisible { get; private set; }

    public decimal? StockQuantity { get; private set; }

    public ProductStatus Status { get; private set; } = ProductStatus.Draft;

    public string? RejectionReason { get; private set; }

    public Guid? ApprovedByUserId { get; private set; }

    public DateTime? ApprovedAtUtc { get; private set; }

    public DateTime? PublishedAtUtc { get; private set; }

    public ICollection<ProductImage> Images { get; private set; } = new List<ProductImage>();

    public ICollection<Inquiry> Inquiries { get; private set; } = new List<Inquiry>();

    public void UpdateDetails(
        Guid categoryId,
        string name,
        string slug,
        string? sku,
        string description,
        string unit,
        decimal minimumOrderQuantity,
        decimal? stockQuantity)
    {
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentException("Category id is required.", nameof(categoryId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(slug);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(unit);

        if (minimumOrderQuantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimumOrderQuantity), "Minimum order quantity must be greater than zero.");
        }

        CategoryId = categoryId;
        Name = name;
        Slug = slug;
        SKU = sku;
        Description = description;
        Unit = unit;
        MinimumOrderQuantity = minimumOrderQuantity;
        StockQuantity = stockQuantity;
    }

    public void SetPricing(decimal? price, string currency, bool isPriceVisible)
    {
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero when supplied.");
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(currency);

        Price = price;
        Currency = currency.ToUpperInvariant();
        IsPriceVisible = isPriceVisible;
    }

    public void SubmitForApproval()
    {
        if (Status is not ProductStatus.Draft and not ProductStatus.Rejected)
        {
            throw new InvalidOperationException("Only draft or rejected products can be submitted for approval.");
        }

        Status = ProductStatus.PendingApproval;
        RejectionReason = null;
    }

    public void Approve(Guid adminUserId, DateTime approvedAtUtc)
    {
        EnsureAdminUser(adminUserId);

        Status = ProductStatus.Approved;
        RejectionReason = null;
        ApprovedByUserId = adminUserId;
        ApprovedAtUtc = approvedAtUtc;
        PublishedAtUtc ??= approvedAtUtc;
    }

    public void Reject(Guid adminUserId, string reason)
    {
        EnsureAdminUser(adminUserId);
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        Status = ProductStatus.Rejected;
        RejectionReason = reason;
        ApprovedByUserId = adminUserId;
        ApprovedAtUtc = null;
    }

    public void Archive() => Status = ProductStatus.Archived;

    private static void EnsureAdminUser(Guid adminUserId)
    {
        if (adminUserId == Guid.Empty)
        {
            throw new ArgumentException("Admin user id is required.", nameof(adminUserId));
        }
    }
}
