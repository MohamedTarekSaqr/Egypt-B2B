namespace EgyptB2B.Domain.ValueObjects;

public sealed record Money(decimal Amount, string Currency)
{
    public static Money EgyptianPound(decimal amount) => new(amount, "EGP");
}
