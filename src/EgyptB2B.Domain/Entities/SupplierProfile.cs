using EgyptB2B.Domain.Common;
using EgyptB2B.Domain.Enums;
using EgyptB2B.Domain.ValueObjects;

namespace EgyptB2B.Domain.Entities;

public sealed class SupplierProfile : AuditableEntity
{
    private SupplierProfile()
    {
    }

    public SupplierProfile(
        Guid userId,
        string companyName,
        Address address,
        string contactPersonName,
        string contactPhone)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(companyName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contactPersonName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contactPhone);

        UserId = userId;
        CompanyName = companyName;
        Address = address;
        ContactPersonName = contactPersonName;
        ContactPhone = contactPhone;
        ApprovalStatus = ApprovalStatus.Pending;
    }

    public Guid UserId { get; private set; }

    public string CompanyName { get; private set; } = string.Empty;

    public string? CompanyDescription { get; private set; }

    public string? CommercialRegistrationNumber { get; private set; }

    public string? TaxNumber { get; private set; }

    public string? Website { get; private set; }

    public string? LogoUrl { get; private set; }

    public Address Address { get; private set; } = Address.Empty;

    public string ContactPersonName { get; private set; } = string.Empty;

    public string ContactPhone { get; private set; } = string.Empty;

    public ApprovalStatus ApprovalStatus { get; private set; } = ApprovalStatus.Pending;

    public string? RejectionReason { get; private set; }

    public Guid? ApprovedByUserId { get; private set; }

    public DateTime? ApprovedAtUtc { get; private set; }

    public ICollection<Product> Products { get; private set; } = new List<Product>();

    public ICollection<Inquiry> Inquiries { get; private set; } = new List<Inquiry>();

    public void UpdateCompanyInfo(
        string companyName,
        string? companyDescription,
        string? commercialRegistrationNumber,
        string? taxNumber,
        string? website,
        string? logoUrl,
        Address address,
        string contactPersonName,
        string contactPhone)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(companyName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contactPersonName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contactPhone);

        CompanyName = companyName;
        CompanyDescription = companyDescription;
        CommercialRegistrationNumber = commercialRegistrationNumber;
        TaxNumber = taxNumber;
        Website = website;
        LogoUrl = logoUrl;
        Address = address;
        ContactPersonName = contactPersonName;
        ContactPhone = contactPhone;
    }

    public void Approve(Guid adminUserId, DateTime approvedAtUtc)
    {
        EnsureAdminUser(adminUserId);

        ApprovalStatus = ApprovalStatus.Approved;
        RejectionReason = null;
        ApprovedByUserId = adminUserId;
        ApprovedAtUtc = approvedAtUtc;
    }

    public void Reject(Guid adminUserId, string reason)
    {
        EnsureAdminUser(adminUserId);
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        ApprovalStatus = ApprovalStatus.Rejected;
        RejectionReason = reason;
        ApprovedByUserId = adminUserId;
        ApprovedAtUtc = null;
    }

    public void Suspend(Guid adminUserId, string reason)
    {
        EnsureAdminUser(adminUserId);
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        ApprovalStatus = ApprovalStatus.Suspended;
        RejectionReason = reason;
        ApprovedByUserId = adminUserId;
        ApprovedAtUtc = null;
    }

    private static void EnsureAdminUser(Guid adminUserId)
    {
        if (adminUserId == Guid.Empty)
        {
            throw new ArgumentException("Admin user id is required.", nameof(adminUserId));
        }
    }
}
