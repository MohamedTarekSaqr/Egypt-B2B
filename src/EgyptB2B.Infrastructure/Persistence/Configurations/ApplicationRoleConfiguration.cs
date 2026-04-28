using EgyptB2B.Application.Common.Security;
using EgyptB2B.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EgyptB2B.Infrastructure.Persistence.Configurations;

public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            CreateRole(new Guid("11111111-1111-1111-1111-111111111111"), AppRoles.Admin),
            CreateRole(new Guid("22222222-2222-2222-2222-222222222222"), AppRoles.Supplier),
            CreateRole(new Guid("33333333-3333-3333-3333-333333333333"), AppRoles.Buyer));
    }

    private static ApplicationRole CreateRole(Guid id, string name)
    {
        return new ApplicationRole
        {
            Id = id,
            Name = name,
            NormalizedName = name.ToUpperInvariant(),
            ConcurrencyStamp = id.ToString()
        };
    }
}
