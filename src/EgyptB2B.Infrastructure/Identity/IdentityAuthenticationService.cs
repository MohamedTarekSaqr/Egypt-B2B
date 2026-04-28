using EgyptB2B.Application.Common.Interfaces;
using EgyptB2B.Application.Common.Models;
using EgyptB2B.Application.Common.Security;
using EgyptB2B.Application.Features.Auth;
using Microsoft.AspNetCore.Identity;

namespace EgyptB2B.Infrastructure.Identity;

public sealed class IdentityAuthenticationService : IAuthenticationService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityAuthenticationService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IDateTimeProvider dateTimeProvider)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<AuthResponse>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = NormalizeRegistrationRole(request.Role);
        if (role is null)
        {
            return Result<AuthResponse>.Failure(AuthErrors.RoleNotAllowed);
        }

        if (!await _roleManager.RoleExistsAsync(role))
        {
            return Result<AuthResponse>.Failure(AuthErrors.RoleNotConfigured);
        }

        var email = request.Email.Trim();
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser is not null)
        {
            return Result<AuthResponse>.Failure(AuthErrors.EmailAlreadyRegistered);
        }

        var user = new ApplicationUser
        {
            FullName = request.FullName.Trim(),
            Email = email,
            UserName = email,
            PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : request.PhoneNumber.Trim(),
            IsActive = true,
            CreatedAtUtc = _dateTimeProvider.UtcNow
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return Result<AuthResponse>.Failure(ToErrors(createResult.Errors));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return Result<AuthResponse>.Failure(ToErrors(roleResult.Errors));
        }

        var roles = (await _userManager.GetRolesAsync(user)).ToArray();

        return Result<AuthResponse>.Success(CreateAuthResponse(user, roles));
    }

    public async Task<Result<AuthResponse>> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var email = request.Email.Trim();
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result<AuthResponse>.Failure(AuthErrors.InvalidCredentials);
        }

        if (!user.IsActive)
        {
            return Result<AuthResponse>.Failure(AuthErrors.AccountInactive);
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
        {
            return Result<AuthResponse>.Failure(AuthErrors.InvalidCredentials);
        }

        user.LastLoginAtUtc = _dateTimeProvider.UtcNow;
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result<AuthResponse>.Failure(ToErrors(updateResult.Errors));
        }

        var roles = (await _userManager.GetRolesAsync(user)).ToArray();

        return Result<AuthResponse>.Success(CreateAuthResponse(user, roles));
    }

    public async Task<Result<CurrentUserResponse>> GetCurrentUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (userId == Guid.Empty)
        {
            return Result<CurrentUserResponse>.Failure(AuthErrors.UserNotFound);
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result<CurrentUserResponse>.Failure(AuthErrors.UserNotFound);
        }

        if (!user.IsActive)
        {
            return Result<CurrentUserResponse>.Failure(AuthErrors.AccountInactive);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return Result<CurrentUserResponse>.Success(new CurrentUserResponse(
            user.Id,
            user.FullName,
            user.Email ?? string.Empty,
            user.PhoneNumber,
            roles.ToArray()));
    }

    private AuthResponse CreateAuthResponse(ApplicationUser user, IReadOnlyCollection<string> roles)
    {
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email ?? string.Empty, roles);

        return new AuthResponse(
            user.Id,
            user.FullName,
            user.Email ?? string.Empty,
            user.PhoneNumber,
            roles.ToArray(),
            token.AccessToken,
            token.ExpiresAtUtc);
    }

    private static string? NormalizeRegistrationRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            return null;
        }

        role = role.Trim();

        if (role.Equals(AppRoles.Supplier, StringComparison.OrdinalIgnoreCase))
        {
            return AppRoles.Supplier;
        }

        if (role.Equals(AppRoles.Buyer, StringComparison.OrdinalIgnoreCase))
        {
            return AppRoles.Buyer;
        }

        return null;
    }

    private static Error[] ToErrors(IEnumerable<IdentityError> identityErrors)
    {
        return identityErrors
            .Select(error => AuthErrors.IdentityFailure(error.Code, error.Description))
            .ToArray();
    }
}
