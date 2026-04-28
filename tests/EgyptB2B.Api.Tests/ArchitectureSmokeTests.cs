namespace EgyptB2B.Api.Tests;

public sealed class ArchitectureSmokeTests
{
    [Fact]
    public void ApiAssemblyLoads()
    {
        Assert.Equal("EgyptB2B.Api", typeof(Program).Assembly.GetName().Name);
    }
}
