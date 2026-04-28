using EgyptB2B.Domain.Common;

namespace EgyptB2B.Domain.Entities;

public sealed class Category : AuditableEntity
{
    private Category()
    {
    }

    public Category(string name, string slug, Guid? parentCategoryId = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(slug);

        Name = name;
        Slug = slug;
        ParentCategoryId = parentCategoryId;
        IsActive = true;
    }

    public Guid? ParentCategoryId { get; private set; }

    public Category? ParentCategory { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? NameAr { get; private set; }

    public string Slug { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; } = true;

    public int SortOrder { get; private set; }

    public ICollection<Category> Children { get; private set; } = new List<Category>();

    public ICollection<Product> Products { get; private set; } = new List<Product>();

    public void UpdateDetails(string name, string slug, string? nameAr, string? description, int sortOrder)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(slug);

        Name = name;
        Slug = slug;
        NameAr = nameAr;
        Description = description;
        SortOrder = sortOrder;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
