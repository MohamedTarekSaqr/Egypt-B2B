using EgyptB2B.Application.Common.Interfaces;
using EgyptB2B.Application.Common.Models;
using EgyptB2B.Application.Features.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgyptB2B.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ICurrentUserService _currentUserService;

    public AuthController(
        IAuthenticationService authenticationService,
        ICurrentUserService currentUserService)
    {
        _authenticationService = authenticationService;
        _currentUserService = currentUserService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authenticationService.RegisterAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            return ToProblemResult(result.Errors);
        }

        return CreatedAtAction(nameof(Me), result.Value);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authenticationService.LoginAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            return ToProblemResult(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(CurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is not { } userId)
        {
            return Unauthorized();
        }

        var result = await _authenticationService.GetCurrentUserAsync(userId, cancellationToken);

        if (result.IsFailure)
        {
            return ToProblemResult(result.Errors);
        }

        return Ok(result.Value);
    }

    private ObjectResult ToProblemResult(IReadOnlyCollection<Error> errors)
    {
        var statusCode = GetStatusCode(errors);
        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = "Authentication request failed.",
            Detail = errors.FirstOrDefault()?.Message,
            Instance = HttpContext.Request.Path
        };

        problem.Extensions["errors"] = errors.Select(error => new
        {
            error.Code,
            error.Message
        });

        return StatusCode(statusCode, problem);
    }

    private static int GetStatusCode(IReadOnlyCollection<Error> errors)
    {
        if (errors.Any(error => error.Code == AuthErrors.EmailAlreadyRegistered.Code))
        {
            return StatusCodes.Status409Conflict;
        }

        if (errors.Any(error => error.Code == AuthErrors.InvalidCredentials.Code))
        {
            return StatusCodes.Status401Unauthorized;
        }

        if (errors.Any(error => error.Code == AuthErrors.AccountInactive.Code))
        {
            return StatusCodes.Status403Forbidden;
        }

        if (errors.Any(error => error.Code == AuthErrors.UserNotFound.Code))
        {
            return StatusCodes.Status404NotFound;
        }

        return StatusCodes.Status400BadRequest;
    }
}
