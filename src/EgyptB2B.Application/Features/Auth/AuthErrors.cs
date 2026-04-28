using EgyptB2B.Application.Common.Models;

namespace EgyptB2B.Application.Features.Auth;

public static class AuthErrors
{
    public static readonly Error EmailAlreadyRegistered = new(
        "Auth.EmailAlreadyRegistered",
        "A user with this email address already exists.");

    public static readonly Error InvalidCredentials = new(
        "Auth.InvalidCredentials",
        "The email or password is incorrect.");

    public static readonly Error AccountInactive = new(
        "Auth.AccountInactive",
        "This account is inactive.");

    public static readonly Error RoleNotAllowed = new(
        "Auth.RoleNotAllowed",
        "Only Supplier and Buyer roles can be used for self-registration.");

    public static readonly Error RoleNotConfigured = new(
        "Auth.RoleNotConfigured",
        "The requested role is not configured.");

    public static readonly Error UserNotFound = new(
        "Auth.UserNotFound",
        "The requested user was not found.");

    public static Error IdentityFailure(string code, string message)
    {
        return new Error($"Identity.{code}", message);
    }
}
