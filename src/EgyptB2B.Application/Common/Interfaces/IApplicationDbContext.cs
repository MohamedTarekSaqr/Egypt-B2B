using EgyptB2B.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EgyptB2B.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<SupplierProfile> SupplierProfiles { get; }

    DbSet<Category> Categories { get; }

    DbSet<Product> Products { get; }

    DbSet<ProductImage> ProductImages { get; }

    DbSet<Inquiry> Inquiries { get; }

    DbSet<InquiryMessage> InquiryMessages { get; }

    DbSet<ApprovalAuditLog> ApprovalAuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
