using EgyptB2B.Domain.Common;

namespace EgyptB2B.Domain.Entities;

public sealed class ApprovalAuditLog : BaseEntity
{
    private ApprovalAuditLog()
    {
    }

    public ApprovalAuditLog(Guid adminUserId, string entityType, Guid entityId, string action, string? reason)
    {
        if (adminUserId == Guid.Empty)
        {
            throw new ArgumentException("Admin user id is required.", nameof(adminUserId));
        }

        if (entityId == Guid.Empty)
        {
            throw new ArgumentException("Entity id is required.", nameof(entityId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(entityType);
        ArgumentException.ThrowIfNullOrWhiteSpace(action);

        AdminUserId = adminUserId;
        EntityType = entityType;
        EntityId = entityId;
        Action = action;
        Reason = reason;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid AdminUserId { get; private set; }

    public string EntityType { get; private set; } = string.Empty;

    public Guid EntityId { get; private set; }

    public string Action { get; private set; } = string.Empty;

    public string? Reason { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
}
