namespace EgyptB2B.Domain.ValueObjects;

public sealed record Address(string Governorate, string City, string AddressLine)
{
    public static Address Empty => new(string.Empty, string.Empty, string.Empty);
}
