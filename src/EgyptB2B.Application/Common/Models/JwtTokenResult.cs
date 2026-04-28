namespace EgyptB2B.Application.Common.Models;

public sealed record JwtTokenResult(string AccessToken, DateTime ExpiresAtUtc);
