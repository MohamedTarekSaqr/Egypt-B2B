namespace EgyptB2B.Domain.Enums;

public enum ProductStatus : byte
{
    Draft = 1,
    PendingApproval = 2,
    Approved = 3,
    Rejected = 4,
    Archived = 5
}
