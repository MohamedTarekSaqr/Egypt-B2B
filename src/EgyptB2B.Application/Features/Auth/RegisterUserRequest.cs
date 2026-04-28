using System.ComponentModel.DataAnnotations;

namespace EgyptB2B.Application.Features.Auth;

public sealed class RegisterUserRequest
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string FullName { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Phone]
    [StringLength(50)]
    public string? PhoneNumber { get; init; }

    [Required]
    [MinLength(8)]
    public string Password { get; init; } = string.Empty;

    [Required]
    public string Role { get; init; } = string.Empty;
}
