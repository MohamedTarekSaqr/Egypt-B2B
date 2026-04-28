namespace EgyptB2B.Domain.Common;

public abstract class SoftDeletableEntity : AuditableEntity
{
    public bool IsDeleted { get; private set; }

    public DateTime? DeletedAtUtc { get; private set; }

    public void MarkAsDeleted(DateTime deletedAtUtc)
    {
        IsDeleted = true;
        DeletedAtUtc = deletedAtUtc;
        SetUpdatedAt(deletedAtUtc);
    }

    public void Restore(DateTime restoredAtUtc)
    {
        IsDeleted = false;
        DeletedAtUtc = null;
        SetUpdatedAt(restoredAtUtc);
    }
}
