using EgyptB2B.Application.Common.Interfaces;

namespace EgyptB2B.Infrastructure.Services;

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
