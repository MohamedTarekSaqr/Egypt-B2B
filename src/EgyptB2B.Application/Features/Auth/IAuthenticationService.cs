using EgyptB2B.Application.Common.Models;

namespace EgyptB2B.Application.Features.Auth;

public interface IAuthenticationService
{
    Task<Result<AuthResponse>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<AuthResponse>> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<CurrentUserResponse>> GetCurrentUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
