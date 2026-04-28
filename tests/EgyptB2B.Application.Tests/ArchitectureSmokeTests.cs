using EgyptB2B.Application.Common.Models;

namespace EgyptB2B.Application.Tests;

public sealed class ArchitectureSmokeTests
{
    [Fact]
    public void ResultSuccessCreatesSuccessfulResult()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }
}
