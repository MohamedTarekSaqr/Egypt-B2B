using EgyptB2B.Domain.Common;

namespace EgyptB2B.Domain.Entities;

public sealed class InquiryMessage : BaseEntity
{
    private InquiryMessage()
    {
    }

    public InquiryMessage(Guid inquiryId, Guid senderUserId, string message)
    {
        if (inquiryId == Guid.Empty)
        {
            throw new ArgumentException("Inquiry id is required.", nameof(inquiryId));
        }

        if (senderUserId == Guid.Empty)
        {
            throw new ArgumentException("Sender user id is required.", nameof(senderUserId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        InquiryId = inquiryId;
        SenderUserId = senderUserId;
        Message = message;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid InquiryId { get; private set; }

    public Inquiry? Inquiry { get; private set; }

    public Guid SenderUserId { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public bool IsRead { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public void MarkAsRead() => IsRead = true;
}
