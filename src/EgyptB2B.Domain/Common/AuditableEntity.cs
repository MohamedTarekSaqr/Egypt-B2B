namespace EgyptB2B.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    public void SetCreatedAt(DateTime createdAtUtc)
    {
        CreatedAtUtc = createdAtUtc;
    }

    public void SetUpdatedAt(DateTime updatedAtUtc)
    {
        UpdatedAtUtc = updatedAtUtc;
    }
}
