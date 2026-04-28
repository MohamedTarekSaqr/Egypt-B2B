namespace EgyptB2B.Application.Common.Security;

public static class AppRoles
{
    public const string Admin = nameof(Admin);
    public const string Supplier = nameof(Supplier);
    public const string Buyer = nameof(Buyer);

    public static readonly IReadOnlyCollection<string> All = new[]
    {
        Admin,
        Supplier,
        Buyer
    };
}
