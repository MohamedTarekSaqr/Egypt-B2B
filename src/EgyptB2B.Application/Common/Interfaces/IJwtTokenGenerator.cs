using EgyptB2B.Application.Common.Models;

namespace EgyptB2B.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(Guid userId, string email, IReadOnlyCollection<string> roles);
}
