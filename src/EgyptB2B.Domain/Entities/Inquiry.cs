using EgyptB2B.Domain.Common;
using EgyptB2B.Domain.Enums;

namespace EgyptB2B.Domain.Entities;

public sealed class Inquiry : AuditableEntity
{
    private Inquiry()
    {
    }

    public Inquiry(
        Guid buyerUserId,
        Guid supplierProfileId,
        Guid? productId,
        string subject,
        string message,
        decimal? quantity,
        string? unit)
    {
        if (buyerUserId == Guid.Empty)
        {
            throw new ArgumentException("Buyer user id is required.", nameof(buyerUserId));
        }

        if (supplierProfileId == Guid.Empty)
        {
            throw new ArgumentException("Supplier profile id is required.", nameof(supplierProfileId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(subject);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        BuyerUserId = buyerUserId;
        SupplierProfileId = supplierProfileId;
        ProductId = productId;
        Subject = subject;
        Message = message;
        Quantity = quantity;
        Unit = unit;
        Status = InquiryStatus.New;
    }

    public Guid BuyerUserId { get; private set; }

    public Guid SupplierProfileId { get; private set; }

    public SupplierProfile? SupplierProfile { get; private set; }

    public Guid? ProductId { get; private set; }

    public Product? Product { get; private set; }

    public string Subject { get; private set; } = string.Empty;

    public string Message { get; private set; } = string.Empty;

    public decimal? Quantity { get; private set; }

    public string? Unit { get; private set; }

    public InquiryStatus Status { get; private set; } = InquiryStatus.New;

    public ICollection<InquiryMessage> Messages { get; private set; } = new List<InquiryMessage>();

    public void MarkResponded() => Status = InquiryStatus.Responded;

    public void Close() => Status = InquiryStatus.Closed;

    public void Cancel() => Status = InquiryStatus.Cancelled;
}
