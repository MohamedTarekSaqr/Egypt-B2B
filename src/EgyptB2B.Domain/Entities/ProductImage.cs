using EgyptB2B.Domain.Common;

namespace EgyptB2B.Domain.Entities;

public sealed class ProductImage : BaseEntity
{
    private ProductImage()
    {
    }

    public ProductImage(Guid productId, string url, bool isPrimary, int sortOrder = 0, string? altText = null)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Product id is required.", nameof(productId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(url);

        ProductId = productId;
        Url = url;
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
        AltText = altText;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid ProductId { get; private set; }

    public Product? Product { get; private set; }

    public string Url { get; private set; } = string.Empty;

    public string? AltText { get; private set; }

    public int SortOrder { get; private set; }

    public bool IsPrimary { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
}
