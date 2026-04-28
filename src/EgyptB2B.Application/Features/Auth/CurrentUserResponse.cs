namespace EgyptB2B.Application.Features.Auth;

public sealed record CurrentUserResponse(
    Guid UserId,
    string FullName,
    string Email,
    string? PhoneNumber,
    IReadOnlyCollection<string> Roles);
