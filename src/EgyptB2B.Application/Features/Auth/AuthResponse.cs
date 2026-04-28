namespace EgyptB2B.Application.Features.Auth;

public sealed record AuthResponse(
    Guid UserId,
    string FullName,
    string Email,
    string? PhoneNumber,
    IReadOnlyCollection<string> Roles,
    string AccessToken,
    DateTime ExpiresAtUtc);
